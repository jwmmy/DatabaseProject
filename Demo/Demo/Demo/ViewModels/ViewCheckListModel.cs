using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using MvvmHelpers;
using System.Windows.Input;
using System.Diagnostics;
using Demo.Models;
using System.ComponentModel;
using System.Globalization;
using Demo.Views;
using Demo.Service;

namespace Demo.ViewModels {
public class ViewCheckListModel : MvvmHelpers.BaseViewModel, INotifyPropertyChanged {

    public event PropertyChangedEventHandler PropertyChanged;
    private ClientUser m_ClientUser;


    ObservableRangeCollection<ToDoListCellViewModel> checkListViewModels ;

    public IList<ToDoListCellViewModel> CheckListsVMPro {
        get {
            return this.checkListViewModels;
        }
    }


    public ClientUser ClientUserPro {
        get {
            return m_ClientUser;
        }
        set {
            if (m_ClientUser != value) {
                m_ClientUser = value;

                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs("ClientUserPro"));
                }
            }
        }
    }


    public ViewCheckListModel(ClientUser clientUser) {

        ClientUserPro = clientUser;
        MysqlService mysqlService = new MysqlService(ClientUserPro);
        mysqlService.GetUserToDoList();

        checkListViewModels = new ObservableRangeCollection<ToDoListCellViewModel>();

        foreach (var tmp in m_ClientUser.User_ToDo_List) {
            checkListViewModels.Add(new ToDoListCellViewModel(ClientUserPro, tmp));
        }

    }







}
}