using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Demo.Models {

public class CheckList : BindableObject {

    public static readonly BindableProperty Serial_No_Property = BindableProperty.Create("Serial_No", typeof(string), typeof(CheckList));
    public static readonly BindableProperty Check_Name_Property = BindableProperty.Create("Check_Name", typeof(string), typeof(CheckList));
    public static readonly BindableProperty CheckVersion_Property = BindableProperty.Create("CheckVersion", typeof(string), typeof(CheckList));
    public static readonly BindableProperty MCheckList_Property = BindableProperty.Create("MCheckList", typeof(List<CheckItem>), typeof(CheckList));
    public static readonly BindableProperty Select_Item_Property = BindableProperty.Create("Select_Item", typeof(CheckItem), typeof(CheckList));


    public string Serial_No {
        get {
            return (string)GetValue(Serial_No_Property);
        }
        set {
            SetValue(Serial_No_Property, value);
        }
    }

    public string Check_Name {
        get {
            return (string)GetValue(Check_Name_Property);
        }
        set {
            SetValue(Check_Name_Property, value);
        }
    }
    public string CheckVersion {
        get {
            return (string)GetValue(CheckVersion_Property);
        }
        set {
            SetValue(CheckVersion_Property, value);
        }
    }

    public CheckItem Select_Item {

        get {
            return (CheckItem)GetValue(Select_Item_Property);
        }
        set {
            SetValue(Select_Item_Property, value);
        }
    }

    public  List<CheckItem> MCheckList {
        get {
            return (List<CheckItem>)GetValue(MCheckList_Property);
        }
        set {
            SetValue(MCheckList_Property, value);
        }
    }



}

}