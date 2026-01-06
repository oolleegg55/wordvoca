using CommunityToolkit.Mvvm.ComponentModel;

namespace WordVoca.App.ViewModels;

public partial class WordListViewModel : ObservableObject
{
    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private int _wordCount;

    [ObservableProperty]
    private string _sourceLang = "en";

    [ObservableProperty]
    private string _targetLang;
}
