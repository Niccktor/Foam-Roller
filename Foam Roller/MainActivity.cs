using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using Android.Util;

using MyBluetooth;

namespace Foam_Roller
{
    [Activity(Label = "@string/app_name",
        Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            DisplayMetrics display = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(display);


            SetContentView(Resource.Layout.activity_main);

            BluetoothConnection btConnection = null;
            if (btConnection == null)
                btConnection = new BluetoothConnection();

            // Connection bluetooth
            TextView status = FindViewById<TextView>(Resource.Id.status);
            Button Connexion = FindViewById<Button>(Resource.Id.Connexion);

            // Saisie du kinésithérapeute
            EditText LastName = FindViewById<EditText>(Resource.Id.LastName);
            EditText FirstName = FindViewById<EditText>(Resource.Id.FirstName);
            EditText seuil = FindViewById<EditText>(Resource.Id.seuil);
            LastName.SetWidth(display.WidthPixels / 3);
            FirstName.SetWidth(display.WidthPixels / 3);
            seuil.SetWidth(display.WidthPixels / 3);
            Button BtValide = FindViewById<Button>(Resource.Id.BtValide);

            // Information ECG / EMG
            TextView ECG = FindViewById<TextView>(Resource.Id.ECG);
            TextView EMG = FindViewById<TextView>(Resource.Id.EMG);
            ECG.SetWidth(display.WidthPixels / 2);
            EMG.SetWidth(display.WidthPixels / 2);
            Button getECG = FindViewById<Button>(Resource.Id.getECG);
            Button getEMG = FindViewById<Button>(Resource.Id.getEMG);
            getECG.SetWidth(display.WidthPixels / 2);
            getEMG.SetWidth(display.WidthPixels / 2);

            BtValide.Click += delegate
            {
                LastName.Enabled = false;
                FirstName.Enabled = false;
                seuil.Enabled = false;
                BtValide.Enabled = false;
            };

           /* test1.Click += async (sender, args) =>
            {
                test1.Visibility = Android.Views.ViewStates.Gone;
                await btConnection.Recive(test, this);*/
            #region Old
            /*
            byte[] read = new byte[20];
            try { 

                if (!btConnection.thisSocket.IsConnected)
                    test.Text += "Je ne suis pas co";

                //if (btConnection.thisSocket.InputStream.Length > 0)
                    btConnection.thisSocket.InputStream.Read(read, 0, 1);
                btConnection.thisSocket.InputStream.Flush();
                //btConnection.thisSocket.InputStream.Close();
                RunOnUiThread(() =>
                {
                    test.Text += Encoding.Default.GetString(read);
                   // test.Text += "Bonjour";
                });
                }
                catch
                {
                    test.Text += "i cant Read";
                }*/
            #endregion
            // };

            /* test2.Click += async (sender, args) =>
             {
                 await btConnection.send(test, LastName, this);*/
            #region Old
            /*string send = "Bonjour";

            try
            {
                if (btConnection.thisSocket.IsConnected)
                {
                    btConnection.thisSocket.OutputStream.Write(Encoding.ASCII.GetBytes(send + "\n") , 0, send.Length + 1);
                    btConnection.thisSocket.OutputStream.Flush();
                    test.Text += send;
                }
            }
            catch (Exception e)
            {
                test.Text += "t";
            }*/
            #endregion
            // };

            Connexion.Click += async (sender, args) =>
            {
                Connexion.Enabled = false;
                await btConnection.conect(status, this);
                if (btConnection.thisSocket.IsConnected == true)
                {
                    Connexion.Visibility = Android.Views.ViewStates.Gone;
                    BtValide.Enabled = true;
                    getECG.Enabled = true;
                    getEMG.Enabled = true;
                    await btConnection.Recive(ECG, EMG, this);
                }
                #region old
                /*
                if (btConnection == null)
                {
                    //listenThread.Start();

                    btConnection = new BluetoothConnection();
                    btConnection.getAdapter();
                    btConnection.thisAdapter.StartDiscovery();

                    try
                    {
                        btConnection.getDevice(); // Recherge le Device HC-05 dans la liste des device bounded
                        btConnection.thisDevice.SetPairingConfirmation(false);
                        btConnection.thisDevice.SetPairingConfirmation(true);
                        btConnection.thisDevice.CreateBond();
                    }
                    catch (Exception e)
                    {
                    }
                    btConnection.thisAdapter.CancelDiscovery();
                    socket = btConnection.thisDevice.CreateInsecureRfcommSocketToServiceRecord(Java.Util.UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));
                    btConnection.thisSocket = socket;
                }
                try
                {

                    if (!btConnection.thisSocket.IsConnected)
                        btConnection.thisSocket.ConnectAsync();
                    while (!btConnection.thisSocket.IsConnected)
                    {
                        test.Text = "Attente de connection";
                    }
                    if (btConnection.thisSocket.IsConnected)
                        test.Text = "Connected";
                }
                catch (Exception e)
                {
                    test.Text = "Pas de conection";
                }*/
                #endregion
            };

            getECG.Click += async (sender, args) =>
            {
                await btConnection.send("{FC}", this);
            };

            getEMG.Click += async (sender, args) =>
            {
                await btConnection.send("{EMG}", this);
            };




            #region old
            // Initialisation Bluetooth
            /*TextView BluetoothStatusText = FindViewById<TextView>(Resource.Id.BluetoothStatusText);
            TextView test = FindViewById<TextView>(Resource.Id.BluetoothStatusText); 
            BluetoothAdapter Bluetooth = BluetoothAdapter.DefaultAdapter;

            if (Bluetooth == null)
            {
                BluetoothStatusText.Visibility = Android.Views.ViewStates.Visible;
                test.Text = "Device doesn't support Bluetooth.";
            }
            else
            {
                BluetoothStatusText.Visibility = Android.Views.ViewStates.Visible;
                test.Text = "Device support Bluetooth.";
                if (!Bluetooth.IsEnabled)
                {
                    Bluetooth.Enable();
                    test.Text += " Bluetooth Was Disable.";
                }
                else
                {
                    test.Text += " Bluetooth already Enable.";
                }
                System.Collections.Generic.ICollection<BluetoothDevice> bondedDevices = Bluetooth.BondedDevices;
                BluetoothDevice capteur = null;
                foreach (BluetoothDevice device in bondedDevices)
                {
                    if (device.Name == "HC-05")
                    {
                        capteur = Bluetooth.GetRemoteDevice(device.Address);
                    }
                }
                if (capteur != null)
                {
                    /* capteur.CreateInsecureRfcommSocketToServiceRecord(
                         Android.Provider.Settings.Secure.GetString(
                             Android.App.Application.Context.ContentResolver, 
                             Android.Provider.Settings.Secure.AndroidId));*/
            /*  Java.Util.UUID MY_UUID = Java.Util.UUID.FromString("0000110E-0000-1000-8000-00805F9B34FB");
              BluetoothSocket socket = capteur.CreateInsecureRfcommSocketToServiceRecord();
              socket.Connect();
              if (socket.IsConnected)
              {
                  test.Text += " Je suis connecte";
              }
              else
              {
                  test.Text += "Je ne suis pas co ..";
              }
          }
      }/*
      Connexion.Click += (sendeer, e) =>
      {
          System.Collections.Generic.ICollection<BluetoothDevice> bondedDevices = Bluetooth.BondedDevices;
          if (bondedDevices.Count > 0)
          {
              foreach (BluetoothDevice device in bondedDevices)
              {
                  if (device.Name == "HC-05")
                  {
                      if (device.CreateBond() == true)
                      {
                          device.CreateInsecureRfcommSocketToServiceRecord(ParcelUuid.FromString(device.UU));
                      }
                  }
              }
          }
          else
          {
              test.Text = "Rien";
          }
      };*/

            #endregion
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
       
}