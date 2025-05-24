using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MyFirstWebApp.Services
{
    public static class EnumService
    {
        public static string GetDisplayName<TEnum>(TEnum value) where TEnum : Enum
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttribute<DisplayAttribute>();
            return attribute?.Name ?? value.ToString();
        }

        public static Dictionary<int, string> GetEnumDisplayNames<TEnum>() where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .ToDictionary(
                    e => Convert.ToInt32(e),
                    e => GetDisplayName(e)
                );
        }

        public static TEnum ParseEnum<TEnum>(string value) where TEnum : Enum
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }

        public static bool TryParseEnum<TEnum>(string value, out TEnum result) where TEnum : struct, Enum
        {
            return Enum.TryParse(value, out result);
        }
    }
}
