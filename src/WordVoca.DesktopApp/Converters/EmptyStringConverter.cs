using System;
using System.Globalization;

using Avalonia.Data;
using Avalonia.Data.Converters;

namespace WordVoca.DesktopApp.Converters;

public sealed class EmptyStringConverter : IValueConverter
{
    private const string NoDataPlaceholder = "---";

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string text && !string.IsNullOrWhiteSpace(text))
        {
            return text;
        }

        return NoDataPlaceholder;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return BindingOperations.DoNothing;
    }
}
