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
        public async Task Recive(TextView ECG, TextView EMG, Foam_Roller.MainActivity mainActivity)
        {
            await Task.Run(() =>
            {
                string str = "";
                string nb = "";
                string type = "";
                string res = "";
                bool first = false;
                bool iscmd = false;
                int i = 0;
                while (true)
                {
                    byte[] read = new byte[5];
                    try
                    {
                        if (!this.thisSocket.IsConnected)
                        {
                            break;
                        }

                        this.thisSocket.InputStream.Read(read, 0, 5);

                        // Permet de parser le résultats 
                        if (read.Length > 0)    
                        { 
                            str += Encoding.Default.GetString(read);
                            i = 0;
                            first = false;
                            iscmd = false;
                            nb = "";
                            type = "";
                            while (str.Length > i)
                            {
                                if (str[i] == '{')
                                    first = true;
                                if (str[i] >= 'A' && str[i] <= 'Z')
                                    type += str[i];
                                if (str[i] >= '0' && str[i] <= '9')
                                    nb += str[i];
                                if (str[i] == '}' && first != false)
                                    iscmd = true;
                                i++;
                            }
                            if (iscmd != false)
                            {
                                res = nb;
                                if (type == "FC")
                                    ECG.Text = "Fréauence cardique : " + res;
                                else if (type == "EMG")
                                    EMG.Text = "EMG : " + res;

                                nb = "";
                                str = "";
                                type = "";
                            }

                        }
                    }
                    catch
                    {
                        break ;
                    }
                }
            });
        }

        public async Task send(string text, Foam_Roller.MainActivity mainActivity)
        {
            await Task.Run(() =>
            {
                try
                {
                    if (this.thisSocket.IsConnected)
                    {
                        this.thisSocket.OutputStream.Write(Encoding.ASCII.GetBytes(text + "\n"), 0, text.Length + 1);
                        this.thisSocket.OutputStream.Flush();
                    }
                }
                catch (Exception e)
                {
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
                    if (!this.thisSocket.IsConnected)
                        this.thisSocket.ConnectAsync();
                    while (!this.thisSocket.IsConnected) // Boucle infinie si le périphérique est éteint
                    { 
                        //this.thisSocket.ConnectAsync();
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