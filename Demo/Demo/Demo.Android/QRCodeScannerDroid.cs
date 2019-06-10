﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


using System.Threading.Tasks;
using Demo.Service;
using Demo.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(QrCodeScanningService))]

namespace Demo.Droid {
public class QrCodeScanningService : IQrCodeScanningService {

    public async Task<string> ScanAsync() {

        var scanner = new ZXing.Mobile.MobileBarcodeScanner();
        var options = new ZXing.Mobile.MobileBarcodeScanningOptions();

        options.PossibleFormats = new List<ZXing.BarcodeFormat>() {
            ZXing.BarcodeFormat.QR_CODE,
        };


        var scanResults = await scanner.Scan(options);

        if (scanner != null) {
            return scanResults.Text;
        } else {
            return null;
        }
    }
}
}