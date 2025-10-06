using System.Reflection;

namespace Axolotl.Utilities.Helpers;

public static class GetEntityProperties
{
    public static Dictionary<string, object> GetPropertiesWithValues<T>(this T entity)
    {
        PropertyInfo[] properties = typeof(T).GetProperties();
        var entityParams = new Dictionary<string, object>();

        foreach (PropertyInfo property in properties)
        {
            object value = property.GetValue(entity)!;

            if (value != null)
            {
                entityParams["P" + property.Name] = value;
            }
        }

        return entityParams;
    }
}