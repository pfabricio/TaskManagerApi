using System.ComponentModel;

namespace TaskManager.Application.Common.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        if (field == null) return value.ToString();
        var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
        return attribute?.Description ?? value.ToString();
    }

    public static string GetDescriptionFromValue<TEnum>(this int value) where TEnum : Enum
    {
        var enumValue = (TEnum)Enum.ToObject(typeof(TEnum), value);
        var field = enumValue.GetType().GetField(enumValue.ToString());
        var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return attribute == null ? enumValue.ToString() : attribute.Description;
    }

    public static int ToInt<TEnum>(this TEnum enumValue) where TEnum : Enum
    {
        return Convert.ToInt32(enumValue);
    }
}