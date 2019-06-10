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
public partial class ViewChekListPage : ContentPage {



    public ViewChekListPage(ViewCheckListModel viewCheckListModel) {

        InitializeComponent();
        BindingContext = viewCheckListModel;

        int toDoListCnt = viewCheckListModel.ClientUserPro.User_ToDo_List.Count;
        Title = viewCheckListModel.ClientUserPro.Login_ID + "(" + toDoListCnt + ")";

    }




}
}