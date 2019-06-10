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
public class LoginViewModel : MvvmHelpers.BaseViewModel, INotifyPropertyChanged {

    private enum LoginStatus {
        Login_Error = 0,
        Login_Success = 1,
        Login_Null = 3
    }
    private LoginStatus m_loginStatus = LoginStatus.Login_Null;

    public event PropertyChangedEventHandler PropertyChanged;

    public ICommand LoginCommand {
        get;
        private set;
    }
    public ICommand CancelCommand {
        get;
        private set;
    }
    private ClientUser clientUser = new ClientUser();
    private string loginStatus = "";
    private string cancelBtnText = "Cancel";

    public ClientUser ClientUserPro {
        get {
            return clientUser;
        }
        set {
            if (clientUser != value) {
                clientUser = value;

                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs("ClientUserPro"));
                }
            }
        }
    }


    public string LoginStatusPro {
        get {
            return loginStatus;
        }
        set {
            if (loginStatus != value) {
                loginStatus = value;

                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs("LoginStatusPro"));
                }
            }
        }
    }
    public string CancelBtnTextPro {
        get {
            return cancelBtnText;
        }
        set {
            if (cancelBtnText != value) {
                cancelBtnText = value;

                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs("CancelBtnTextPro"));
                }
            }
        }
    }


    public LoginViewModel () {

        LoginCommand = new Command(m_LoginCommand);
        CancelCommand = new Command(m_CancelCommand);
        cancelBtnText = "Cancel";

        ClientUserPro.Login_ID = "n97071133";
        ClientUserPro.Login_Password = "n97071133";
    }

    private void m_LoginCommand() { //hard code
        LoginStatusPro = "";

        if (m_loginStatus == LoginStatus.Login_Null || m_loginStatus == LoginStatus.Login_Error || m_loginStatus == LoginStatus.Login_Success) {
            MysqlService mysqlService = new MysqlService(ClientUserPro);
            mysqlService.UserVerificationService();
            if (ClientUserPro.Verification == true) {
                m_loginStatus = LoginStatus.Login_Success;
                Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new ViewChekListPage(new ViewCheckListModel(ClientUserPro)));
                // NavigationList.Execute(null);
            } else {
                m_loginStatus = LoginStatus.Login_Error;
            }
        }


        UpdateLoginStatusUI();
        Debug.WriteLine("m_LoginCommand Click");
    }
    private void ResetClientUser() {
        ClientUserPro.Login_ID = "";
        ClientUserPro.Login_Password = "";
        ClientUserPro.Verification = false;
        ClientUserPro.User_ToDo_List.Clear();
    }

    private void UpdateLoginStatusUI() {

        if (m_loginStatus == LoginStatus.Login_Success) {
            CancelBtnTextPro = "Logout";
        } else if (m_loginStatus == LoginStatus.Login_Error) {
            LoginStatusPro = "Wrong Account ID or Password";
            CancelBtnTextPro = "Cancel";
            ClientUserPro.Login_ID = "";
            ClientUserPro.Login_Password = "";
        } else if (m_loginStatus == LoginStatus.Login_Null) {
            LoginStatusPro = "";
            CancelBtnTextPro = "Cancel";
            ClientUserPro.Login_ID = "";
            ClientUserPro.Login_Password = "";
        }
    }


    private void m_CancelCommand() {
        clientUser.Login_ID = "";
        clientUser.Login_Password = "";
        ResetClientUser();
        m_loginStatus = LoginStatus.Login_Null;
        UpdateLoginStatusUI();
        Debug.WriteLine("m_CancelCommand Click");
    }


}
}