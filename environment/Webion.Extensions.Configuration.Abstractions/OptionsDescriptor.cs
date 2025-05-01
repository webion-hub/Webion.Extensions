using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Schema;

namespace Webion.Extensions.Configuration.Abstractions;

public static class OptionsDescriptor
{
    /// <summary>
    /// Generates a JSON schema representation of the specified application settings type.
    /// </summary>
    /// <typeparam name="TAppsettings">The type representing application settings to generate the schema for.</typeparam>
    /// <returns>
    /// A string containing the JSON schema for the specified application settings type.
    /// </returns>
    public static string GetOptionsSchema<TAppsettings>()
    {
        var schema = JsonSerializerOptions.Default.GetJsonSchemaAsNode(typeof(TAppsettings));
        return schema.ToString();
    }
    
    /// <summary>
    /// Generates a dictionary describing the properties of a specified class type,
    /// represented as key-value pairs, where keys include an optional prefix.
    /// </summary>
    /// <typeparam name="T">The class type to describe. Must implement ISetting and have a parameterless constructor.</typeparam>
    /// <param name="prefix">An optional prefix that is added to property names, typically derived from a configuration section.</param>
    /// <returns>
    /// A dictionary where each key represents a property's name, potentially prefixed with its section,
    /// and each value contains the default property's value or null if unavailable.
    /// </returns>
    public static Dictionary<string, string?> Describe<T>(string? prefix = null)
        where T : class, new()
    {
        return Describe(typeof(T), prefix);
    }


    /// <summary>
    /// Generates a dictionary describing the properties of the specified type, represented as key-value pairs,
    /// where keys include an optional prefix derived from the section of the associated configuration setting.
    /// </summary>
    /// <param name="type">The type to describe. Must implement ISetting and contain a parameterless constructor.</param>
    /// <param name="prefix">An optional prefix that is added to property names, typically derived from a configuration section.</param>
    /// <returns>
    /// A dictionary where each key represents a property's name, potentially prefixed with its section,
    /// and each value contains the property's value or null if unavailable.
    /// </returns>
    public static Dictionary<string, string?> Describe(Type type, string? prefix = null)
    {
        var result = new Dictionary<string, string?>();

        AddProperties(type, prefix);
        return result;

        void AddProperties(Type currentType, string? parentPrefix)
        {
            foreach (var property in currentType.GetProperties())
            {
                var key = string.IsNullOrEmpty(parentPrefix)
                    ? property.Name
                    : $"{parentPrefix}:{property.Name}";

                if (property.PropertyType.IsClass && 
                    property.PropertyType != typeof(string) &&
                    property.PropertyType.GetInterfaces().All(x => x != typeof(ISpanFormattable)))
                {
                    AddProperties(property.PropertyType, key);   
                }
                else
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
                    
                    result[key] = instance is not null
                        ? property.GetValue(instance)?.ToString()
                        : null;       
                }
            }
        }
    }
}