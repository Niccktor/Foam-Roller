using Android.Widget;
using Android.Bluetooth;
using System.Threading.Tasks;
using System.Text;
using System;
using System.Threading;



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
                            
                            mainActivity.RunOnUiThread(() =>
                            {
                                test.Text += "Socket non connecté.";
                            });
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

                        mainActivity.RunOnUiThread(() =>
                        {
                            test.Text += "Impossible de sur le socket.";
                        });
                        break ;
                    }
                }
            });
        }

        public async Task send(TextView test, EditText editText, Foam_Roller.MainActivity mainActivity)
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

        public async Task conect(TextView text, Foam_Roller.MainActivity mainActivity)
        {
            await Task.Run(() =>
            {
                BluetoothSocket socket = null;
                if (this != null)
                {
                    //listenThread.Start();

                    this.getAdapter();
                    this.thisAdapter.StartDiscovery();

                    try
                    {
                        this.getDevice(); // Recherge le Device HC-05 dans la liste des device bounded
                        if (this.thisDevice == null)
                        {
                            mainActivity.RunOnUiThread(() =>
                            {
                                text.Text = "Le périphérique HC-05 n'est pas appairé, veuillez aller dans les paramètres de votre téléphone et associer le périphérique.";
                            });
                        }
                        this.thisDevice.SetPairingConfirmation(false);
                        this.thisDevice.SetPairingConfirmation(true);
                        this.thisDevice.CreateBond();
                    }
                    catch (Exception e)
                    {
                    }
                    this.thisAdapter.CancelDiscovery();
                    socket = this.thisDevice.CreateInsecureRfcommSocketToServiceRecord(Java.Util.UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));
                    this.thisSocket = socket;
                }
                try
                {
                    while (!this.thisSocket.IsConnected) // Boucle infinie si le périphérique est éteint
                    { 
                        this.thisSocket.ConnectAsync();
                        mainActivity.RunOnUiThread(() =>
                        {
                            text.Text = "Attente de connection";
                        });

                    }
                    if (this.thisSocket.IsConnected)
                    {
                        mainActivity.RunOnUiThread(() =>
                        {
                            text.Text = "Connecter";

                        });
                    }
                }
                catch (Exception e)
                {
                    mainActivity.RunOnUiThread(() =>
                    {
                        text.Text = "Pas de conection";
                    });
                }
            });
        }

        public BluetoothAdapter thisAdapter { get; set; }
        public BluetoothDevice thisDevice { get; set; }
        public BluetoothSocket thisSocket { get; set; }



    }
}