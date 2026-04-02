using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
        await Shell.Current.GoToAsync("WordListCreation");
    }

    [RelayCommand]
    private async Task GoToWordList(string name)
    {
        await Shell.Current.GoToAsync($"WordList?WordListName={Uri.EscapeDataString(name)}");
    }

    public async void LoadWordListsAsync()
    {
        foreach (WordList wordList in _wordListStorage.GetAll())
        {
            if (WordLists.Any(x => x.Id == wordList.Id))
            {
                continue;
            }

            WordLists.Add(wordList);
        }
    }
}
