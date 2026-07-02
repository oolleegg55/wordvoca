namespace WordVoca.App.Pages.Main;

public partial class MainPage : ContentPage
{
    private readonly MainPageVm _mainPageVm;

    public MainPage(MainPageVm vm)
    {
        InitializeComponent();

        BindingContext = vm;
        _mainPageVm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _mainPageVm.InitializeAsync();
    }
}
