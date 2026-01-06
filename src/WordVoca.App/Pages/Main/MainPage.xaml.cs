namespace WordVoca.App.Pages.Main;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageVm vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
