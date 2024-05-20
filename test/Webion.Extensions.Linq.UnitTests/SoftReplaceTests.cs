namespace Webion.Extensions.Linq.UnitTests;

public sealed class SoftReplaceTests
{
    [Fact]
    public void Add_should_add_the_items_correctly()
    {
        // Arrange
        List<string> original = ["a", "b", "c"];
        List<string> replacement = ["d", "e", "f"];

        // Act
        var result = original.SoftReplace(
            replacement: replacement,
            match: (o, n) => o == n,
            add: n => n
        );

        // Assert
        Assert.Equal(replacement, result.Added);
        Assert.Equal(6, original.Count);
    }
    
    [Fact]
    public void Update_should_update_the_items_correctly()
    {
        // Arrange
        var user1 = new User(1, "Mario");
        var user2 = new User(1, "Paolo");

        List<User> original = [user1];
        List<User> replacement = [user2];

        // Act
        var result = original.SoftReplace(
            replacement: replacement,
            match: (o, n) => o.Id == n.Id,
            update: (o, n) =>
            {
                o.Name = n.Name;
            }
        );

        // Assert
        Assert.Equal("Paolo", user1.Name);
        Assert.Single(result.Updated);
        Assert.Single(original);
    }
    
    [Fact]
    public void Delete_should_remove_the_items_correctly()
    {
        // Arrange
        var user1 = new User(1, "Mario");

        List<User> original = [user1];
        List<User> replacement = [];

        // Act
        var result = original.SoftReplace(
            replacement: replacement,
            match: (o, n) => o.Id == n.Id,
            delete: o => original.Remove(o)
        );

        // Assert
        Assert.Single(result.Removed);
        Assert.Empty(original);
    }
}

file class User(int id, string name)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
}