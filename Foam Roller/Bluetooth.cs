using System.Text;
using System;
using Android.Widget;
using Android.Bluetooth;

namespace Core_Bluetooth
{
    public static class Bluetooth
    {

        // isBluetoothSupported Permets de vérifier si l'appareil a un périphérique Bluetooth 
        public static bool isBluetoothSupported(BluetoothAdapter Bluetooth, TextView BluetoothStatusText)
        {
            bool result = false;
            if (Bluetooth == null)
            {
                BluetoothStatusText.Visibility = Android.Views.ViewStates.Visible;
                BluetoothStatusText.Text = "Device doesn't support Bluetooth.";
                result = false;
            }
            else
            {
                BluetoothStatusText.Visibility = Android.Views.ViewStates.Visible;
                BluetoothStatusText.Text = "Device support Bluetooth.";
                result = true;
            }
            return (result);
        }
    }
}