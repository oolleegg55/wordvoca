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
            new()
            {
                Name = "Word List #1",
                WordCount = 10,
                TargetLang = "ru"
            },
            new()
            {
                Name = "Word List #2",
                WordCount = 100,
                TargetLang = "fr"
            },
            new()
            {
                Name = "Word List #3",
                WordCount = 42,
                TargetLang = "kz"
            },
        };
        BindingContext = this;
    }
}
