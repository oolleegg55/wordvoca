using System;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WordVoca.Core.Models;
using WordVoca.Core.Storages;

namespace WordVoca.DesktopApp.ViewModels.Dialogs;

public partial class CreationWordListViewModel : DialogViewModel
{
    private readonly IWordListRepository _wordListStorage;

    public CreationWordListViewModel(IWordListRepository wordListStorage)
    {
        _wordListStorage = wordListStorage;
    }

    public async Task InitializeAsync()
    {
        // TODO: replace with _wordListStorage
        DefaultWordListTitle = $"Word List #{(await _wordListStorage.GetAllAsync()).Count + 1}";
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

        WordList wordList = new WordList([])
        {
            Id = Guid.NewGuid(),
            Name = WordListTitle,

            SourceLang = SourceLanguage,
            TargetLang = TargetLanguage,

            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };

        await _wordListStorage.SaveAsync(wordList);

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
