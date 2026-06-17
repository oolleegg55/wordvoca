using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WordVoca.App.Pages.WordLists;
using WordVoca.Core.Models;
using WordVoca.Core.Storages;

namespace WordVoca.App.Pages.Main;

public partial class MainPageVm : ObservableObject
{
    private readonly IWordListStorage _wordListStorage;

    public ObservableCollection<WordList> WordLists { get; } = [];

    public MainPageVm(IWordListStorage wordListStorage)
    {
        _wordListStorage = wordListStorage;
    }

    [RelayCommand]
    private async Task CreateWordListAsync()
    {
        await Shell.Current.GoToAsync(nameof(CreationPage));
    }

    [RelayCommand]
    private async Task GoToWordList(Guid id)
    {
        await Shell.Current.GoToAsync($"{nameof(WordListPage)}?WordListId={id:D}");
    }

    public async void LoadWordListsAsync()
    {
        var list = await _wordListStorage.GetAll();
        foreach (WordList wordList in list)
        {
            if (WordLists.Any(x => x.Id == wordList.Id))
            {
                continue;
            }

            WordLists.Add(wordList);
        }
    }
}
