using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZXing.Mobile;

namespace OctoPrintCore.Services
{
    public class QrCodeReader : IQrCodeReader
    {
        public async Task<string> ScanAsync()
        {
            var optionsDefault = new MobileBarcodeScanningOptions();
            var optionsCustom = new MobileBarcodeScanningOptions();

            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Scan the QR Code",
                BottomText = "Please Wait",
            };

            try
            {
                var scanResult = await scanner.Scan(optionsCustom);
                return scanResult.Text;
            } catch (Exception e)
            {
                return null;
            }
        }
    }
}
