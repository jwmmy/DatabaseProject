using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Demo.Models;
using System.ComponentModel;
using System.Windows.Input;
using System.Diagnostics;
using Demo.Service;
using Acr.UserDialogs;
using System.Threading.Tasks;
using Mvvm.Commands;
using Demo.Views;

namespace Demo.ViewModels {
public class CheckItemCellViewModel : MvvmHelpers.BaseViewModel, INotifyPropertyChanged {


    public CheckItemCellViewModel (ClientUser clientUser, CheckItem checkItem, CheckItemListViewModel checkItemListViewModel) {
        m_CheckItem = checkItem;
        ClientUserValue = clientUser;
        CheckItemTappedCommandValue = new Command(m_TabeCellCommand);
        m_checkItemListViewModel = checkItemListViewModel;
    }
    public event PropertyChangedEventHandler PropertyChanged;
    private CheckItemListViewModel m_checkItemListViewModel;
    public ClientUser m_ClientUser;
    public CheckItem m_CheckItem;


    public CheckItem CheckItemValue {
        get {
            return m_CheckItem;
        }
        set {
            if (m_CheckItem != value) {
                m_CheckItem = value;


            }
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs("CheckItemValue"));
                PropertyChanged(this, new PropertyChangedEventArgs("CheckResultDescPros"));
            }
        }
    }
    public ClientUser ClientUserValue {
        get {
            return m_ClientUser;
        }
        set {
            if (m_ClientUser != value) {
                m_ClientUser = value;

                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs("ClientUserValue"));
                }
            }
        }
    }
    public string CheckResultDescPros {
        get {
            return CheckItemValue.Check_Result_Desc;
        }
        set {
            if (CheckItemValue.Check_Result_Desc != value) {
                CheckItemValue.Check_Result_Desc = value;

                if (PropertyChanged != null) {
                    PropertyChanged(this, new PropertyChangedEventArgs("CheckResultDescPros"));
                }
            }
        }
    }

    public ICommand CheckItemTappedCommandValue {
        get;
        private set;
    }



    async void m_TabeCellCommand(object obj) {

        CheckItem ckitem = obj as CheckItem;
        //   ckList.MCheckList = new List<CheckItem>();
        //   ClientUserPros.Selected_CheckList = ckList;

        if (ckitem.Check_Point != null && ckitem.Check_Point != "") {//&& false



            var scanner = new ZXing.Mobile.MobileBarcodeScanner();

            var fooQRCode = await scanner.Scan();

            if (fooQRCode != null)
                Console.WriteLine("Scanned Barcode: " + fooQRCode.Text);






            //Debug.WriteLine("m_TabeCellCommand Click => " + ckitem.Doc_Item + " need check point is " + ckitem.Check_Point);
            //var fooQRCode = await DependencyService.Get<IQrCodeScanningService>().ScanAsync();
            //Console.WriteLine("QR Result = " + fooQRCode);



            //Device.BeginInvokeOnMainThread(() =>
            //                               UserDialogs.Instance.Alert(title: "123", message: "456", okText: "OK"));

            if (fooQRCode != null && fooQRCode.ToString() == ckitem.Check_Point ) {
                ClientUserValue.Selected_CheckItem = CheckItemValue;
                // score page
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new ScorePage(new ScoreViewModel(m_ClientUser, this)));
            } else {
                await Application.Current.MainPage.DisplayAlert("Error", "This checkpoint is wrong", "OK");
            }
        }

        else {
            Debug.WriteLine("m_TabeCellCommand Click => " + ckitem.Doc_Item + " no check point");
            ClientUserValue.Selected_CheckItem = CheckItemValue;
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new ScorePage(new ScoreViewModel(m_ClientUser, this)));
        }
        //   Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new CheckItemListPage(new CheckItemListViewModel(ClientUserPros, ckList)));

    }

    public void RefreshResult(CheckItem checkItem) {
        CheckItemValue = checkItem;
        m_checkItemListViewModel.refresh();
    }
}
}