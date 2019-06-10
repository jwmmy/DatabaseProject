using System;
using System.Collections.Generic;
using System.Windows.Input;

using Xamarin.Forms;
using Demo.Models;
using Demo.ViewModels;

using Xamarin.Forms.Xaml;

namespace Demo.Controls {
[XamlCompilation(XamlCompilationOptions.Compile)]

public partial class ToDoListCell : ViewCell {

    public ToDoListCell() {
        InitializeComponent();
    }


    public static readonly BindableProperty CheckListProsProperty = BindableProperty.Create("CheckListPros", typeof(CheckList), typeof(ToDoListCell), null);


    public CheckList CheckListPros {
        get {
            return (CheckList)GetValue(CheckListProsProperty);
        }
        set {
            SetValue(CheckListProsProperty, value);
        }
    }

    protected override void OnBindingContextChanged() {
        base.OnBindingContextChanged();

        if (BindingContext != null) {
            labelName.Text = CheckListPros.Check_Name;
            // labelSerialNo.Text = CheckListPros.Serial_No;
            labelVersion.Text = CheckListPros.CheckVersion;
        }
    }

    public static readonly BindableProperty CheckListItemTappedCommandProsProperty = BindableProperty.Create("CheckListItemTappedCommandPros", typeof(ICommand), typeof(ToDoListCell), null);
    public ICommand CheckListItemTappedCommandPros {
        get {
            return (ICommand)GetValue(CheckListItemTappedCommandProsProperty);
        }
        set {
            SetValue(CheckListItemTappedCommandProsProperty, value);
        }
    }
    void CellTapped(object sender, System.EventArgs e) {
        if (CheckListItemTappedCommandPros != null) {
            CheckListItemTappedCommandPros.Execute(CheckListPros);
        }
    }

}
}