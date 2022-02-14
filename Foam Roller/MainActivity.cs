using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using Android.Bluetooth;
using Android.Util;
using System;
using System.Text;
using System.Threading.Tasks;

using MyBluetooth;

namespace Foam_Roller
{
    [Activity(Label = "@string/app_name",
        Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        BluetoothConnection btConnection = null;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            DisplayMetrics display = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(display);


            SetContentView(Resource.Layout.activity_main);
            LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            EditText UserNameText = FindViewById<EditText>(Resource.Id.UserNameText);
            TextView ResponseText = FindViewById<TextView>(Resource.Id.ResponseText);
            Button BtValide = FindViewById<Button>(Resource.Id.BtValide);
            Button Connexion = FindViewById<Button>(Resource.Id.Connexion);

            GridLayout grid = FindViewById<GridLayout>(Resource.Id.gridLayout1);
            Button test1 = FindViewById<Button>(Resource.Id.test1);
            Button test2 = FindViewById<Button>(Resource.Id.test2);
            Button test3 = FindViewById<Button>(Resource.Id.test3);

            test1.SetWidth(display.WidthPixels / 3);
            test2.SetWidth(display.WidthPixels / 3);
            test3.SetWidth(display.WidthPixels / 3);

            TextView test = FindViewById<TextView>(Resource.Id.BluetoothStatusText);
            BluetoothSocket socket = null;

            System.Threading.Thread listenThread = new System.Threading.Thread(listener);
            listenThread.Abort();

            test1.Click += async (sender, args) =>
            {
                test1.Visibility = Android.Views.ViewStates.Gone;
                await btConnection.Recive(test, this);
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
            };




            test2.Click += async (sender, args) =>
            {
                await btConnection.send(test, UserNameText);
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
            };

            Connexion.Click += delegate
            {
                if (btConnection == null)
                {
                    listenThread.Start();

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
                }
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

            // Bouton Valide, Vérifie si l'utilisateur est connu.
            BtValide.Click += (sender, e) =>
            {
                string Response = Core.UserName.CheckUser(UserNameText.Text);
                if (string.IsNullOrWhiteSpace(Response))
                {
                    ResponseText.Text = "";
                    ResponseText.Visibility = Android.Views.ViewStates.Gone;
                }
                else
                {
                    ResponseText.Text = Response;
                    ResponseText.Visibility = Android.Views.ViewStates.Visible;
                }
            };

            void listener()
            {

                while (true)
                {
                    //thisTime = DateTime.Now;
                    try
                    {

                        if (btConnection.thisSocket.InputStream.CanRead)
                        {
                            test.Text = "I can read";
                        }
                        //btConnection.thisSocket.InputStream.Close();
                    }
                    catch
                    {
                        test.Text += "i cant Read";
                    }

                }
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
       
}