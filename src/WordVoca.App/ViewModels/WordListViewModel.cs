using CommunityToolkit.Mvvm.ComponentModel;

namespace WordVoca.App.ViewModels;

public partial class WordListViewModel : ObservableObject
{
    [ObservableProperty]
    private string _name = "Word List #1";

    [ObservableProperty]
    private int _wordCount = 99;

    [ObservableProperty]
    private string _sourceLang = "en";

    [ObservableProperty]
    private string _targetLang = "ru";
}
