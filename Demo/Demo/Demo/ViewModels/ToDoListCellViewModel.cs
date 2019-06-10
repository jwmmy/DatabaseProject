using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Demo.Models;
using System.ComponentModel;
using System.Diagnostics;
using Demo.Views;
using MvvmHelpers;
using Demo.Service;

namespace Demo.ViewModels {
public class ToDoListCellViewModel :  MvvmHelpers.BaseViewModel {

    public event PropertyChangedEventHandler PropertyChanged;
    private bool enableSerialNum = true;
    public ToDoListCellViewModel (ClientUser clientUser, CheckList checkList) {
        CheckListValue = checkList;
        ClientUserValue = clientUser;
        CheckListItemTappedCommandValue = new Command(m_TabeCellCommand);
        EnableCheckList = true;
        //   NavigationCommand = new Command(ChangeView);
    }

    public CheckList CheckListValue {
        get;
        set;
    }
    public ClientUser ClientUserValue {
        get;
        set;
    }

    public ICommand CheckListItemTappedCommandValue {
        get;
        private set;
    }
    private void m_TabeCellCommand(object obj) {

        CheckList ckList = obj as CheckList;
        ckList.MCheckList = new List<CheckItem>();
        ClientUserValue.Selected_CheckList = ckList;


        Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new CheckItemListPage(new CheckItemListViewModel(ClientUserValue, ckList, this)));
        Debug.WriteLine("m_TabeCellCommand Click => " + ckList.Serial_No);
    }

    public void refreshUI() {
        CheckListItemTappedCommandValue = null;
        //  EnableCheckList = false;
        CheckListValue.Serial_No = CheckListValue.Serial_No + " (Finish)";
        if (PropertyChanged != null) {
            PropertyChanged(this, new PropertyChangedEventArgs("EnableCheckList"));
            PropertyChanged(this, new PropertyChangedEventArgs("CheckListValue"));
        }
    }

    public bool EnableCheckList {
        get {
            return enableSerialNum;
        }
        set {
            enableSerialNum = value;
        }
    }
}
}