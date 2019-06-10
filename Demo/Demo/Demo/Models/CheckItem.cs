

using System;
using Xamarin.Forms;

namespace Demo.Models {
public class CheckItem : BindableObject {


    public static readonly BindableProperty Doc_Item_Property = BindableProperty.Create("Doc_Item", typeof(string), typeof(CheckItem));
    public static readonly BindableProperty Check_Status_Desc_Property = BindableProperty.Create("Check_Status_Desc", typeof(string), typeof(CheckItem));
    public static readonly BindableProperty IChk_Item_Desc_Property = BindableProperty.Create("IChk_Item_Desc", typeof(string), typeof(CheckItem));
    public static readonly BindableProperty Check_Point_Property = BindableProperty.Create("Check_Point", typeof(string), typeof(CheckItem));
    // public static readonly BindableProperty Check_Point_Property = BindableProperty.Create("Check_Point", typeof(string), typeof(CheckItem));
    public static readonly BindableProperty Check_Result_Desc_Property = BindableProperty.Create("Check_Result_Desc", typeof(string), typeof(CheckItem));
    public static readonly BindableProperty Check_Comment_Property = BindableProperty.Create("Check_Comment", typeof(string), typeof(CheckItem));
    public static readonly BindableProperty Photo_Path_Property = BindableProperty.Create("Photo_Path", typeof(string), typeof(CheckItem));
    public static readonly BindableProperty Ichk_Group_Desc_Property = BindableProperty.Create("Ichk_Group_Desc", typeof(string), typeof(CheckItem));
    //public static readonly BindableProperty UpdateTime_Property = BindableProperty.Create("UpdateTime", typeof(string), typeof(CheckItem));
    //public static readonly BindableProperty Last_Update_Time_Property = BindableProperty.Create("Last_Update_Time", typeof(string), typeof(CheckItem));

    public string Doc_Item {
        get {
            return (string)GetValue(Doc_Item_Property);
        }
        set {
            SetValue(Doc_Item_Property, value);
        }
    }
    public string Check_Status_Desc {
        get {
            return (string)GetValue(Check_Status_Desc_Property);
        }
        set {
            SetValue(Check_Status_Desc_Property, value);
        }
    }


    public string IChk_Item_Desc {
        get {
            return (string)GetValue(IChk_Item_Desc_Property);
        }
        set {
            SetValue(IChk_Item_Desc_Property, value);
        }
    }

    public string Check_Point {
        get {
            return (string)GetValue(Check_Point_Property);
        }
        set {
            SetValue(Check_Point_Property, value);
        }
    }

    public string Check_Result_Desc {
        get {
            return (string)GetValue(Check_Result_Desc_Property);
        }
        set {
            SetValue(Check_Result_Desc_Property, value);
        }
    }

    public string Check_Comment {
        get {
            return (string)GetValue(Check_Comment_Property);
        }
        set {
            SetValue(Check_Comment_Property, value);
        }
    }

    public string Photo_Path {
        get {
            return (string)GetValue(Photo_Path_Property);
        }
        set {
            SetValue(Photo_Path_Property, value);
        }
    }


    public string Ichk_Group_Desc {
        get {
            return (string)GetValue(Ichk_Group_Desc_Property);
        }
        set {
            SetValue(Ichk_Group_Desc_Property, value);
        }
    }

    //public string Check_Result {
    //    get {
    //        return (string)GetValue(Check_Result_Property);
    //    }
    //    set {
    //        SetValue(Check_Result_Property, value);
    //    }
    //}

    //public string Check_Comment {
    //    get {
    //        return (string)GetValue(Check_Comment_Property);
    //    }
    //    set {
    //        SetValue(Check_Comment_Property, value);
    //    }
    //}


    //public string CreateTime {
    //    get {
    //        return (string)GetValue(CreateTime_Property);
    //    }
    //    set {
    //        SetValue(CreateTime_Property, value);
    //    }
    //}
    //public string UpdateTime {
    //    get {
    //        return (string)GetValue(UpdateTime_Property);
    //    }
    //    set {
    //        SetValue(UpdateTime_Property, value);
    //    }
    //}
    //public string Last_Update_Time {
    //    get {
    //        return (string)GetValue(Last_Update_Time_Property);
    //    }
    //    set {
    //        SetValue(Last_Update_Time_Property, value);
    //    }
    //}
}
}