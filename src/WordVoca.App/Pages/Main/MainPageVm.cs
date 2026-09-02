using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WordVoca.App.Pages.WordLists;
using WordVoca.Core.Models;
using WordVoca.Core.Repositories;

namespace WordVoca.App.Pages.Main;

public partial class MainPageVm : ObservableObject
{
    private readonly IWordListRepository _wordListRepository;

    public ObservableCollection<WordList> WordLists { get; } = [];

    public MainPageVm(IWordListRepository wordListRepository)
    {
        _wordListRepository = wordListRepository;
    }

    public async Task InitializeAsync()
    {
        var list = await _wordListRepository.GetAllAsync();
        foreach (WordList wordList in list)
        {
            if (WordLists.Any(x => x.Id == wordList.Id))
            {
                continue;
            }

            WordLists.Add(wordList);
        }
    }

    [RelayCommand]
    private async Task CreateWordListAsync()
    {
        await Shell.Current.GoToAsync(nameof(CreationPage));
    }

    [RelayCommand]
    private async Task GoToWordList(string wordListName)
    {
        await Shell.Current.GoToAsync($"{nameof(WordListPage)}?WordListId={Uri.EscapeDataString(wordListName)}");
    }
}
