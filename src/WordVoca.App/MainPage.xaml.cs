using System.Collections.ObjectModel;

using WordVoca.App.ViewModels;

namespace WordVoca.App;

public partial class MainPage : ContentPage
{
    public ObservableCollection<WordListViewModel> WordListViewModels { get; set; }

    public MainPage()
    {
        InitializeComponent();

        WordListViewModels = new ObservableCollection<WordListViewModel>
        {
            new(),
            new(),
            new(),
        };
        BindingContext = this;
    }
}
