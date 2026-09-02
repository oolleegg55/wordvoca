using System;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WordVoca.Core.Models;
using WordVoca.Core.Repositories;

namespace WordVoca.DesktopApp.ViewModels.Dialogs;

public partial class CreationWordListViewModel : DialogViewModel
{
    private readonly IWordListRepository _wordListRepository;

    public CreationWordListViewModel(IWordListRepository wordListRepository)
    {
        _wordListRepository = wordListRepository;
    }

    public async Task InitializeAsync()
    {
        DefaultWordListTitle = await _wordListRepository.GetNextWordListNameAsync();
    }

    public Langs[] Languages { get; } = Enum.GetValues<Langs>();

    [ObservableProperty]
    private string _wordListTitle = string.Empty;

    [ObservableProperty]
    private Langs _sourceLanguage = Langs.En;

    [ObservableProperty]
    private Langs _targetLanguage = Langs.Ru;

    [ObservableProperty]
    private string _defaultWordListTitle = string.Empty;

    [RelayCommand]
    private void Cancel()
    {
        Reset();
        OnCloseCallback();
    }

    [RelayCommand]
    private async Task Create()
    {
        if (string.IsNullOrWhiteSpace(WordListTitle))
        {
            WordListTitle = DefaultWordListTitle;
        }

        WordList wordList = _wordListRepository.BuildWordList(WordListTitle, SourceLanguage, TargetLanguage);

        await _wordListRepository.SaveAsync(wordList);

        Reset();
        OnCloseCallback();
    }

    private void Reset()
    {
        WordListTitle = string.Empty;
        SourceLanguage = Langs.En;
        TargetLanguage = Langs.Ru;
    }
}
