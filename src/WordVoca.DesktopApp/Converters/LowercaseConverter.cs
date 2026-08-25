using System;
using System.Globalization;

using Avalonia.Data;
using Avalonia.Data.Converters;

namespace WordVoca.DesktopApp.Converters;

public class LowercaseConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is string text ? text.ToLowerInvariant() : value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return BindingOperations.DoNothing;
    }
}
