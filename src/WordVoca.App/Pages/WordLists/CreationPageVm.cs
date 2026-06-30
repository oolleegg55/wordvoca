using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WordVoca.Core.Models;
using WordVoca.Core.Storages;

namespace WordVoca.App.Pages.WordLists;

public partial class CreationPageVm : ObservableValidator
{
    public IEnumerable<Langs> AllLangs { get; } = Enum.GetValues(typeof(Langs)).Cast<Langs>();

    private readonly IWordListStorage _wordListStorage;

    public CreationPageVm(IWordListStorage wordListStorage)
    {
        _wordListStorage = wordListStorage;

        Task.Run(async () =>
        {
            WordListDefaultName = $"Word List #{(await _wordListStorage.GetAll()).Count + 1}";
        });
    }

    [ObservableProperty]
    private string _wordListName = string.Empty;

    [ObservableProperty]
    private string _wordListDefaultName = string.Empty;

    [ObservableProperty]
    private Langs _sourceLang = Langs.En;

    [ObservableProperty]
    private Langs _targetLang = Langs.Es;

    [RelayCommand]
    private async Task Create()
    {
        string wordListName = string.IsNullOrEmpty(WordListName) ? WordListDefaultName : WordListName;
        WordList wordList = new()
        {
            Id = Guid.NewGuid(),
            Name = wordListName,
            SourceLang = SourceLang,
            TargetLang = TargetLang
        };

        await _wordListStorage.Save(wordList);
        await Shell.Current.GoToAsync("..");
    }
}
