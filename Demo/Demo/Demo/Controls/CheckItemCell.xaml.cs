using System;
using System.Collections.Generic;
using System.Windows.Input;

using Xamarin.Forms;
using Demo.Models;
using Demo.ViewModels;

using Xamarin.Forms.Xaml;

namespace Demo.Controls {
[XamlCompilation(XamlCompilationOptions.Compile)]

public partial class CheckItemCell : ViewCell {

    public CheckItemCell() {
        InitializeComponent();
    }


    public static readonly BindableProperty CheckItemProsProperty = BindableProperty.Create("CheckItemPros", typeof(CheckItem), typeof(CheckItemCell), null);


    public CheckItem CheckItemPros {
        get {
            return (CheckItem)GetValue(CheckItemProsProperty);
        }
        set {
            SetValue(CheckItemProsProperty, value);
        }
    }

    protected override void OnBindingContextChanged() {
        base.OnBindingContextChanged();

        if (BindingContext != null) {
            labelTitle.Text = CheckItemPros.Doc_Item + ". " + CheckItemPros.Ichk_Group_Desc;
            labelCheckItemDesc.Text = "    " + (CheckItemPros.IChk_Item_Desc.Length > 7 ? CheckItemPros.IChk_Item_Desc.Substring(0, 7) : CheckItemPros.IChk_Item_Desc) + "...";
            //  labelCheckResultDesc.Text = CheckItemPros.Check_Result_Desc;
        }
    }

    public static readonly BindableProperty CheckItemTappedCommandProsProperty = BindableProperty.Create("CheckItemTappedCommandPros", typeof(ICommand), typeof(CheckItemCell), null);
    public ICommand CheckItemTappedCommandPros {
        get {
            return (ICommand)GetValue(CheckItemTappedCommandProsProperty);
        }
        set {
            SetValue(CheckItemTappedCommandProsProperty, value);
        }
    }
    void CellTapped(object sender, System.EventArgs e) {
        if (CheckItemTappedCommandPros != null) {
            CheckItemTappedCommandPros.Execute(CheckItemPros);
        }
    }

}
}