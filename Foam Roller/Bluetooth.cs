using Android.App;
using Android.Widget;
using Android.Bluetooth;
using System.Threading.Tasks;
using System.Text;
using System;



namespace MyBluetooth
{
    public class BluetoothConnection
    {
        public void getAdapter() { this.thisAdapter = BluetoothAdapter.DefaultAdapter; }
        public void getDevice()
        {
            System.Collections.Generic.ICollection<BluetoothDevice> bondedDevices = this.thisAdapter.BondedDevices;
            foreach (BluetoothDevice device in bondedDevices)
            {
                if (device.Name == "HC-05")
                {
                    this.thisDevice = device;
                }
            }
        }
        public async Task Recive(TextView test, Foam_Roller.MainActivity mainActivity)
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    byte[] read = new byte[20];
                    try
                    {

                        if (!this.thisSocket.IsConnected)
                        {
                            test.Text += "Je ne suis pas co";
                            break;
                        }

                        this.thisSocket.InputStream.Read(read, 0, 20);
                        this.thisSocket.InputStream.Flush();

                        mainActivity.RunOnUiThread(() =>
                        {
                            test.Text = Encoding.Default.GetString(read);
                        });
                    }
                    catch
                    {
                        test.Text += "i cant Read";
                    }
                }
            });
        }

        public async Task send(TextView test, EditText editText)
        {
            await Task.Run(() =>
            {

                try
                {
                    if (this.thisSocket.IsConnected)
                    {
                        this.thisSocket.OutputStream.Write(Encoding.ASCII.GetBytes(editText.Text + "\n"), 0, editText.Text.Length + 1);
                        this.thisSocket.OutputStream.Flush();
                    }
                }
                catch (Exception e)
                {
                    test.Text += "t";
                }
            });
        }
        public BluetoothAdapter thisAdapter { get; set; }
        public BluetoothDevice thisDevice { get; set; }
        public BluetoothSocket thisSocket { get; set; }



    }
}