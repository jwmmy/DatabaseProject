using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ZXing.Net.Mobile.Forms.Android;
using Acr.UserDialogs;

namespace Demo.Droid {
[Activity(Label = "Demo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {
    protected override void OnCreate(Bundle savedInstanceState) {
        //SetIoc();
        TabLayoutResource = Resource.Layout.Tabbar;
        ToolbarResource = Resource.Layout.Toolbar;

        base.OnCreate(savedInstanceState);
        Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
        UserDialogs.Init(this);




        global::ZXing.Net.Mobile.Forms.Android.Platform.Init();
        global::ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);


        TabLayoutResource = Resource.Layout.Tabbar;
        ToolbarResource = Resource.Layout.Toolbar;




        global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
        LoadApplication(new App());
    }

    //private void SetIoc() {
    //    var resolverContainer = new global::XLabs.Ioc.SimpleContainer();
    //    resolverContainer.Register<IMediaPicker, MediaPicker>();
    //    XLabs.Ioc.Resolver.SetResolver(resolverContainer.GetResolver());
    //}

    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults) {

        Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
    }

}
}