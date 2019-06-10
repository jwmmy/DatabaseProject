using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Demo.Models;
using MvvmHelpers;
using Demo.Service;
using System.Diagnostics;
using System.Windows.Input;
using Prism.Commands;

namespace Demo.ViewModels {
public class CheckItemListViewModel : MvvmHelpers.BaseViewModel, INotifyPropertyChanged {

    public event PropertyChangedEventHandler PropertyChanged;

    public ClientUser m_ClientUser;
    private ToDoListCellViewModel m_toDoListCellViewModel;
    public CheckItemListViewModel (ClientUser clientUser, CheckList checkList, ToDoListCellViewModel toDoListCellViewModel) {
        m_ClientUser = clientUser;

        MysqlService mysqlService = new MysqlService(m_ClientUser);
        mysqlService.GetUserCheckItem();

        checkItemsViewModels = new ObservableRangeCollection<CheckItemCellViewModel>();
        foreach (var tmp in m_ClientUser.Selected_CheckList.MCheckList) {
            checkItemsViewModels.Add(new CheckItemCellViewModel(m_ClientUser, tmp, this));
        }

        FinishCommand = new DelegateCommand(m_FinishCommandValue);
        m_toDoListCellViewModel = toDoListCellViewModel;
    }


    public string TitleNamePro {
        get {
            return m_ClientUser.Selected_CheckList.Serial_No.Substring(7) + "(" + m_ClientUser.Selected_CheckList.MCheckList.Count() + ")";
        }
        set {
        }
    }

    ObservableRangeCollection<CheckItemCellViewModel> checkItemsViewModels;
    public IList<CheckItemCellViewModel> CheckItemsVMPro {
        get {
            return this.checkItemsViewModels;
        }
    }


    public bool EnableFinishValue {
        get {
            foreach (CheckItemCellViewModel vm in CheckItemsVMPro) {
                if (vm.CheckResultDescPros == null) {
                    return false;
                }
            }
            return true;
        }
        private set {
        }
    }

    public void refresh() {
        if (PropertyChanged != null) {
            PropertyChanged(this, new PropertyChangedEventArgs("EnableFinishValue"));
        }
    }

    public DelegateCommand FinishCommand {
        get;
        set;
    }

    async void m_FinishCommandValue() {
        MysqlService mysqlService = new MysqlService(m_ClientUser);
        bool status;
        status = mysqlService.Update_Doc_Header();

        if (status) {

            await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
            m_toDoListCellViewModel.refreshUI();
            // m_checkItemCellViewModel.RefreshResult(m_ClientUser.Selected_CheckItem);

        } else {
            await Application.Current.MainPage.DisplayAlert("Error", "Finish Error,plz Click again", "OK");
        }

    }
}
}