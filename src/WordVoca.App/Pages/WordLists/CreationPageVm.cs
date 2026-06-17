using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WordVoca.Core.Models;
using WordVoca.Core.Storages;

namespace WordVoca.App.Pages.WordLists;

public partial class CreationPageVm : ObservableValidator
{
    public IEnumerable<Langs> AllLangs { get; } = Enum.GetValues(typeof(Langs)).Cast<Langs>();

    private readonly IWordListStorage _wordListStorage;

    [ObservableProperty]
    private string _wordListName = string.Empty;

    [ObservableProperty]
    private string _wordListDefaultName = string.Empty;

    [ObservableProperty]
    private Langs _sourceLang = Langs.En;

    [ObservableProperty]
    private Langs _targetLang = Langs.Es;

    public CreationPageVm(IWordListStorage wordListStorage)
    {
        _wordListStorage = wordListStorage;

        // TODO: fix default list name
        //WordListDefaultName = $"Word List #{wordListStorage.GetAll().Count + 1}";
    }

    [RelayCommand]
    private async Task Create()
    {
        string wordListName = string.IsNullOrWhiteSpace(WordListName) ? WordListDefaultName : WordListName;
        WordList wordList = new()
        {
            Id = Guid.NewGuid(),
            Name = WordListName,
            SourceLang = SourceLang,
            TargetLang = TargetLang
        };

        await _wordListStorage.Save(wordList);
        await Shell.Current.GoToAsync("..");
    }
}
