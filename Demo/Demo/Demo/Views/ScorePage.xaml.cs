using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Demo.ViewModels;
using System.ComponentModel;

namespace Demo.Views {
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ScorePage : ContentPage, INotifyPropertyChanged {

    public ScorePage (ScoreViewModel scoreViewModel) {
        InitializeComponent ();
        BindingContext = scoreViewModel;

    }



}
}