Change tracking extensions for Entity Framework Core db contexts.

# Usage
Implement the IChangeTrackingInfoDbo interface on the table that will hold the tracking information.
```csharp
public sealed class ChangeTrackingInfoDbo : IChangeTrackingInfoDbo, IEntityTypeConfiguration<ChangeTrackingInfoDbo>
{
    public Guid Id { get; set; }
    public string? TargetId { get; set; }
    public string? EntityName { get; set; }
    public string? CreatedByName { get; set; }
    
    public decimal Version { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    
    // Note that you can add addional fields and relations.
    public Guid? CreatedByUserId { get; set; }
    
    public JsonDocument? Metadata { get; set; }
    public ChangeTrackingOperation Operation { get; set; }
    public UserDbo? User { get; set; }
    
    public void Configure(EntityTypeBuilder<ChangeTrackingInfoDbo> builder)
    {
        builder.ToTable("change_tracking_info", Schemas.Common);
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Version).IsRequired().HasDefaultValue(0m);
        builder.Property(x => x.Timestamp).IsRequired().HasDefaultValueSql("now()");
        builder.Property(x => x.CreatedByUserId).IsRequired(false);
        builder.Property(x => x.CreatedByName).IsRequired(false).HasMaxLength(128);
        builder.Property(x => x.Metadata).IsRequired(false);
        builder.Property(x => x.Operation).IsRequired();
        builder.Property(x => x.EntityName).IsRequired();
        builder.Property(x => x.TargetId).IsRequired();

        // Addional relation with an ipotetic user table
        builder
            .HasOne(x => x.User)
            .WithMany()
            .HasPrincipalKey(x => x.Id)
            .HasForeignKey(x => x.CreatedByUserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
```

Implement the db context interface.
```csharp
public sealed class MyDbContext : DbContext, IChangeTrackingDbContext<ChangeTrackingInfoDbo>
{
    private readonly IEnumerable<IInterceptor> _interceptors;
    
    // This property is needed only for this example, as we will be tracking
    // the user that makes the queries
    public ClaimsPrincipal? Principal { get; set; }
    
    public EtePgsqlDbContext(DbContextOptions<EtePgsqlDbContext> options, IEnumerable<IInterceptor> interceptors) : base(options)
    {
        _interceptors = interceptors;
    }
    
   
    public DbSet<ChangeTrackingInfoDbo> TrackingInfo { get; set; }
   
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(MyDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        
        // This line is required
        // The library relies on ef core interceptors.
        builder.AddInterceptors(_interceptors);
    }
}
```

Create any custom interceptor to enrich the data.
For example, in this case we will track our custom `User` property for each Api request.

```csharp
// Custom aspnet core middleware that enriches the db context with 
// the current user that is making the api request.
public sealed class ChangeTrackingMiddleware
{
    private readonly RequestDelegate _next;

    public ChangeTrackingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext httpContext, EtePgsqlDbContext dbContext)
    {
        dbContext.Principal = httpContext.User;
        await _next(httpContext);
    }
}

// Custom interceptor that enriches the new row with information retrieved
// from the db context.
public sealed class ChangeTrackingInfoDboDecorator : IChangeTrackingInfoDecorator<ChangeTrackingInfoDbo>
{
    public void Decorate(ChangeTrackingContext context, ChangeTrackingInfoDbo dbo)
    {
        if (context.DbContext is not MyDbContext db)
            return;

        var rawUserId = db.Principal?.FindFirstValue(ClaimTypes.NameIdentifier);
        var userEmail = db.Principal?.FindFirstValue(ClaimTypes.Email);
        if (Guid.TryParse(rawUserId, out var userId))
            dbo.CreatedByUserId = userId;

        dbo.CreatedByName = userEmail;
    }
}
```

Register the services.
```csharp
builder.Services.AddTransient<IChangeTrackingInfoDecorator<ChangeTrackingInfoDbo>, ChangeTrackingInfoDboDecorator>();
builder.Services.AddChangeTracking<MyDbContext, ChangeTrackingInfoDbo>();
```

Once the services are registered, each Delete, Insert and Update operation will be tracked automatically, 
with new rows being created in the configured table.