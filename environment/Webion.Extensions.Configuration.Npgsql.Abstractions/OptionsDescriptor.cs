namespace Webion.Extensions.Configuration.Npgsql.Abstractions;

public static class OptionsDescriptor
{
    public static Dictionary<string, string?> Describe<T>(string? prefix = null)
        where T : class, new()
    {
        var result = new Dictionary<string, string?>();
        var type = typeof(T);
        
        void AddProperties(Type currentType, string? parentPrefix)
        {
            object? instance = null;
            try
            {
                instance = Activator.CreateInstance(currentType);
            }
            catch
            {
                // ignored
            }

            foreach (var property in currentType.GetProperties())
            {
                var key = string.IsNullOrEmpty(parentPrefix)
                    ? property.Name
                    : $"{parentPrefix}:{property.Name}";
    
                if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    AddProperties(property.PropertyType, key);
                }
                else
                {
                    result[key] = property.GetValue(instance)?.ToString();
                }
            }
        }
    
        AddProperties(type, prefix);
    
        return result;
    }
}