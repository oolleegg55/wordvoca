using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WordVoca.Core.Models;
using WordVoca.Core.Repositories;

namespace WordVoca.App.Pages.WordLists;

public partial class CreationPageVm : ObservableValidator
{
    public IEnumerable<Langs> AllLangs { get; } = Enum.GetValues(typeof(Langs)).Cast<Langs>();

    private readonly IWordListRepository _wordListRepository;

    public CreationPageVm(IWordListRepository wordListRepository)
    {
        _wordListRepository = wordListRepository;
    }

    public async Task InitializeAsync()
    {
        WordListDefaultName = await _wordListRepository.GetNextWordListNameAsync();
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
        WordList wordList = _wordListRepository.BuildWordList(wordListName, SourceLang, TargetLang);

        await _wordListRepository.SaveAsync(wordList);
        await Shell.Current.GoToAsync("..");
    }
}
