using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Demo.ViewModels;
namespace Demo.Views {
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class CheckItemListPage : ContentPage {


    private CheckItemListViewModel m_checkItemListViewModel;

    public CheckItemListPage(CheckItemListViewModel checkItemListViewModel) {
        InitializeComponent();
        m_checkItemListViewModel = checkItemListViewModel;
        BindingContext = m_checkItemListViewModel;
    }
}
}