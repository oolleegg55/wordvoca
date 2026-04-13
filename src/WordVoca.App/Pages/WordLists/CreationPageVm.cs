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
    private Langs _sourceLang;

    [ObservableProperty]
    private Langs _targetLang;

    public CreationPageVm(IWordListStorage wordListStorage)
    {
        _wordListStorage = wordListStorage;
    }

    [RelayCommand]
    private async Task Create()
    {
        WordList wordList = new()
        {
            Id = Guid.NewGuid(), Name = WordListName, SourceLang = SourceLang, TargetLang = TargetLang
        };

        _wordListStorage.Save(wordList);
        await Shell.Current.GoToAsync("..");
    }
}
