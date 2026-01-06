using System.Collections.ObjectModel;

using MauiIcons.Core;

using WordVoca.App.ViewModels;

namespace WordVoca.App;

public partial class MainPage : ContentPage
{
    public MainPage(WordListViewModel vm)
    {
        InitializeComponent();
        _ = new MauiIcon();

        BindingContext = vm;
    }
}
