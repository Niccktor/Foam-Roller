using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using Android.Bluetooth;



using System.Collections;



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
        public static void showPairedDevices(BluetoothAdapter Bluetooth, TextView test)
        {

            int i = 0;
             
            System.Collections.Generic.ICollection<BluetoothDevice> bondedDevices = Bluetooth.BondedDevices;
            if (bondedDevices.Count > 0)
            {
                foreach (BluetoothDevice device in bondedDevices)
                {
                    test.Text = i.ToString() + " Address : " + device.Address + " Name : " + device.Name;
                    i++;
                }
            }
            else
            {
                test.Text = "Rien";
            }
        }
    }
}