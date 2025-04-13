namespace Webion.Extensions.Configuration.Abstractions;

public static class OptionsDescriptor
{
    /// <summary>
    /// Scans the assembly containing the specified type <typeparamref name="T"/>,
    /// identifies all types implementing the ISetting interface, and generates
    /// a combined dictionary describing the properties of each of these types.
    /// </summary>
    /// <typeparam name="T">
    /// A type whose assembly will be scanned. The type itself is not directly described,
    /// but any type within the assembly that implements ISetting will be processed.
    /// </typeparam>
    /// <returns>
    /// A dictionary where each key represents a property's name (potentially prefixed
    /// by its associated section) and each value corresponds to the property's default
    /// value or null if unavailable.
    /// </returns>
    public static Dictionary<string, string?> DescribeAssemblyContaining<T>()
    {
        var assembly = typeof(T).Assembly;
        return assembly.GetTypes()
            .Where(x => x.GetInterfaces().Contains(typeof(ISetting)))
            .Select(Describe)
            .SelectMany(x => x)
            .ToDictionary(x => x.Key, x => x.Value);
    }
    
    
    /// <summary>
    /// Generates a dictionary describing the properties of a specified class type,
    /// represented as key-value pairs, where keys include an optional prefix.
    /// </summary>
    /// <typeparam name="T">The class type to describe. Must implement ISetting and have a parameterless constructor.</typeparam>
    /// <returns>
    /// A dictionary where each key represents a property's name, potentially prefixed with its section,
    /// and each value contains the default property's value or null if unavailable.
    /// </returns>
    public static Dictionary<string, string?> Describe<T>()
        where T : ISetting, new()
    {
        return Describe(typeof(T));
    }


    /// <summary>
    /// Generates a dictionary describing the properties of the specified type, represented as key-value pairs,
    /// where keys include an optional prefix derived from the section of the associated configuration setting.
    /// </summary>
    /// <param name="type">The type to describe. Must implement <see cref="ISetting"/> and contain a parameterless constructor.</param>
    /// <returns>
    /// A dictionary where each key represents a property's name, potentially prefixed with its section,
    /// and each value contains the property's value or null if unavailable.
    /// </returns>
    public static Dictionary<string, string?> Describe(Type type)
    {
        var result = new Dictionary<string, string?>();
        var setting = Activator.CreateInstance(type) as ISetting;

        AddProperties(type, setting?.Section);
        return result;

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
    }
}