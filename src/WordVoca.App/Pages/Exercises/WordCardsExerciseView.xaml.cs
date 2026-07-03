namespace WordVoca.App.Pages.Exercises;

public partial class WordCardsExerciseView : ContentPage
{
	public WordCardsExerciseView(WordCardsExerciseVm wordCardsExerciseVm)
	{
		InitializeComponent();
        BindingContext = wordCardsExerciseVm;
	}
}
