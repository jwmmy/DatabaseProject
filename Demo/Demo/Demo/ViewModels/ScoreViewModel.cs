using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Xamarin.Forms;
//using MvvmHelpers;
//using System.Windows.Input;
//using System.Diagnostics;
using Demo.Models;
//using System.ComponentModel;
//using System.Globalization;
//using Demo.Views;
//using Demo.Service;
//using Prism.Commands;
//using Prism.Navigation;

using Demo.Service;

namespace Demo.ViewModels {
using System.ComponentModel;
using System.Diagnostics;
using Plugin.Media.Abstractions;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

public class ScoreViewModel : INotifyPropertyChanged, INavigationAware {

    private ImageSource m_imageSource;
    private MediaFile m_mediaFile = null;
    public ImageSource MyImageSource {
        get {
            return m_imageSource;
        }
        set {
            if (m_imageSource != value) {
                m_imageSource = value;
            }
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs("CheckBox_Ng_Value"));
                PropertyChanged(this, new PropertyChangedEventArgs("CheckBox_Ok_Value"));
                PropertyChanged(this, new PropertyChangedEventArgs("CheckBox_None_Value"));
                PropertyChanged(this, new PropertyChangedEventArgs("EnableCommitValue"));
            }
        }
    }
    // private readonly INavigationService _navigationService;
    // public readonly Prism.Services.IPageDialogService _dialogService;
    private CheckItemCellViewModel m_checkItemCellViewModel;

    public ScoreViewModel (ClientUser clientUser, CheckItemCellViewModel checkItemCellViewModel) {
        ClientUserValue = clientUser;
        CheckItemValue = ClientUserValue.Selected_CheckItem;
        // TakePhotoCommand = new DelegateCommand(m_TakePhotoCommand);
        TakePhotoCommand = new DelegateCommand(m_TakePhotoCommand);
        CommitCommand = new DelegateCommand(m_CommitCommandValue);
        MyImageSource = null;
        m_checkItemCellViewModel = checkItemCellViewModel;
    }
    public ClientUser ClientUserValue {
        get {
            return m_clientUser;
        }
        set {
            if (m_clientUser != value) {
                m_clientUser = value;
            }
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs("ClientUserValue"));

            }
        }
    }
    public DelegateCommand TakePhotoCommand {
        get;
        set;
    }

    async void m_TakePhotoCommand() {
        try {
            // 進行 Plugin.Media 套件的初始化動作
            await Plugin.Media.CrossMedia.Current.Initialize();

            // 確認這個裝置是否具有拍照的功能
            if (!Plugin.Media.CrossMedia.Current.IsCameraAvailable || !Plugin.Media.CrossMedia.Current.IsTakePhotoSupported) {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            // 啟動拍照功能，並且儲存到指定的路徑與檔案中
            m_mediaFile  = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions {
                Directory = "Sample",
                Name = "Sample.jpg"
            });

            if (m_mediaFile == null)
                return;

            // 讀取剛剛拍照的檔案內容，轉換成為 ImageSource，如此，就可以顯示到螢幕上了

            MyImageSource = ImageSource.FromStream(() => {
                var stream = m_mediaFile.GetStream();
                //m_mediaFile.Dispose();
                return stream;
            });
        } catch(Exception ex) {
            m_mediaFile.Dispose();
            Debug.WriteLine(ex);
        }
    }

    public void OnNavigatedFrom(INavigationParameters parameters) {
        throw new NotImplementedException();
    }

    public void OnNavigatedTo(INavigationParameters parameters) {
        throw new NotImplementedException();
    }

    public void OnNavigatingTo(INavigationParameters parameters) {
        throw new NotImplementedException();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public bool CheckBox_Ok_Value {
        get {
            if (CheckItemValue.Check_Result_Desc == "正常") {
                return true;
            } else {
                return false;
            }

        }
        set {
            if (true == value) {
                MyImageSource = null;
                m_mediaFile = null;
                CheckItemValue.Check_Result_Desc = "正常";
            }
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs("CheckBox_Ng_Value"));
                PropertyChanged(this, new PropertyChangedEventArgs("CheckBox_Ok_Value"));
                PropertyChanged(this, new PropertyChangedEventArgs("CheckBox_Soso_Value"));
                PropertyChanged(this, new PropertyChangedEventArgs("EnableCommitValue"));
            }
        }
    }

    public bool CheckBox_Ng_Value {
        get {
            if (CheckItemValue.Check_Result_Desc == "異常") {
                return true;
            } else {
                return false;
            }

        }
        set {
            if (true == value) {
                CheckItemValue.Check_Result_Desc = "異常";
            } else {
                MyImageSource = null;
                m_mediaFile = null;
            }
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs("CheckBox_Ng_Value"));
                PropertyChanged(this, new PropertyChangedEventArgs("CheckBox_Ok_Value"));
                PropertyChanged(this, new PropertyChangedEventArgs("CheckBox_Soso_Value"));
                PropertyChanged(this, new PropertyChangedEventArgs("EnableCommitValue"));
            }
        }
    }

    public bool CheckBox_Soso_Value {
        get {
            if (CheckItemValue.Check_Result_Desc == "堪用") {
                return true;
            } else {
                return false;
            }

        }
        set {
            if (true == value) {
                MyImageSource = null;
                m_mediaFile = null;
                CheckItemValue.Check_Result_Desc = "堪用";
            }
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs("CheckBox_Ng_Value"));
                PropertyChanged(this, new PropertyChangedEventArgs("CheckBox_Ok_Value"));
                PropertyChanged(this, new PropertyChangedEventArgs("CheckBox_Soso_Value"));
                PropertyChanged(this, new PropertyChangedEventArgs("EnableCommitValue"));
            }
        }
    }


    public bool EnableCommitValue {
        get {
            if ((CheckItemValue.Check_Result_Desc == "異常" && MyImageSource == null) || CheckItemValue.Check_Result_Desc == null) {
                return false;
            } else {
                return true;
            }
        }
        private set {
        }
    }

    public CheckItem CheckItemValue {
        get;
        set;
    }

    public ClientUser m_clientUser;

    public DelegateCommand CommitCommand {
        get;
        set;
    }

    async void m_CommitCommandValue() {
        MysqlService mysqlService = new MysqlService(ClientUserValue);
        bool status;
        if (ClientUserValue.Selected_CheckItem.Check_Result_Desc == "異常") {
            status = mysqlService.Update_Doc_Item(m_mediaFile);
        } else {
            status = mysqlService.Update_Doc_Item(null);
        }

        if (status) {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
            m_checkItemCellViewModel.RefreshResult(ClientUserValue.Selected_CheckItem);
            if (m_mediaFile != null) {
                m_mediaFile.Dispose();
            }
        } else {
            await Application.Current.MainPage.DisplayAlert("Error", "Update Error,plz update again", "OK");
        }

    }


}
}