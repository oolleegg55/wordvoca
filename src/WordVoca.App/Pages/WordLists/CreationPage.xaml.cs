using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordVoca.App.Pages.WordLists;

public partial class CreationPage : ContentPage
{
    public CreationPage(CreationPageVm vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}

