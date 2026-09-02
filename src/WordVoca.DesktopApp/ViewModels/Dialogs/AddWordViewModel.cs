using System;
using System.Collections.ObjectModel;
using System.Linq;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using WordVoca.Core.Models;

namespace WordVoca.DesktopApp.ViewModels.Dialogs;

public partial class AddWordViewModel : DialogViewModel
{
    [ObservableProperty]
    private string _value = string.Empty;

    [ObservableProperty]
    private string _translation = string.Empty;

    [ObservableProperty]
    private string _note = string.Empty;

    public ObservableCollection<Word> Words { get; } = [];

    public bool HasWords => Words.Any();

    [RelayCommand]
    public void Cancel()
    {
        OnCloseCallback();
    }

    [RelayCommand]
    public void AddWord()
    {
        Word word = new Word()
        {
            Id = Guid.NewGuid(),
            Value = Value,
            Note = Note,
            Translation = Translation
        };

        Words.Add(word);

        ResetValues();
    }

    private void ResetValues()
    {
        Value = string.Empty;
        Translation = string.Empty;
        Note = string.Empty;
    }
}
