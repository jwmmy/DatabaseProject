using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Demo.Models {
public class ClientUser : BindableObject {

    public static readonly BindableProperty Login_ID_Property = BindableProperty.Create("Login_ID", typeof(string), typeof(ClientUser));
    public static readonly BindableProperty Login_Password_Property = BindableProperty.Create("Login_Password", typeof(string), typeof(ClientUser));
    public static readonly BindableProperty Verification_Property = BindableProperty.Create("Verification", typeof(bool), typeof(ClientUser));
    public static readonly BindableProperty User_ToDo_List_Property = BindableProperty.Create("User_ToDo_List", typeof(List<CheckList>), typeof(ClientUser));
    public static readonly BindableProperty Selected_CheckList_Property = BindableProperty.Create("Selected_CheckList", typeof(CheckList), typeof(ClientUser));
    public static readonly BindableProperty Selected_CheckItem_Property = BindableProperty.Create("Selected_CheckItem", typeof(CheckItem), typeof(ClientUser));

    public string Login_ID {
        get {
            return (string)GetValue(Login_ID_Property);
        }
        set {
            SetValue(Login_ID_Property, value);
        }
    }
    public string Login_Password {
        get {
            return (string)GetValue(Login_Password_Property);
        }
        set {
            SetValue(Login_Password_Property, value);
        }
    }

    public bool Verification {
        get {
            return (bool)GetValue(Verification_Property);
        }
        set {
            SetValue(Verification_Property, value);
        }
    }

    public List<CheckList> User_ToDo_List {
        get {
            return (List<CheckList>)GetValue(User_ToDo_List_Property);
        }
        set {
            SetValue(User_ToDo_List_Property, value);
        }
    }

    public CheckList Selected_CheckList {
        get {
            return (CheckList)GetValue(Selected_CheckList_Property);
        }
        set {
            SetValue(Selected_CheckList_Property, value);
        }
    }

    public CheckItem Selected_CheckItem {
        get {
            return (CheckItem)GetValue(Selected_CheckItem_Property);
        }
        set {
            SetValue(Selected_CheckItem_Property, value);
        }
    }
}
}
