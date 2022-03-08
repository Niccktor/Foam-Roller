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

            #region Déclaration des variables
            // Déclaration Bluetooth;
            BluetoothConnection btConnection = null;
            if (btConnection == null)
                btConnection = new BluetoothConnection();

            // Connection bluetooth;
            TextView status = FindViewById<TextView>(Resource.Id.status);
            Button Connexion = FindViewById<Button>(Resource.Id.Connexion);

            // Saisie du kinésithérapeute;
            EditText LastName = FindViewById<EditText>(Resource.Id.LastName);
            EditText FirstName = FindViewById<EditText>(Resource.Id.FirstName);
            EditText seuil = FindViewById<EditText>(Resource.Id.seuil);
            LastName.SetWidth(display.WidthPixels / 3);
            FirstName.SetWidth(display.WidthPixels / 3);
            seuil.SetWidth(display.WidthPixels / 3);
            Button BtValide = FindViewById<Button>(Resource.Id.BtValide);

            // Information ECG / EMG;
            TextView ECG = FindViewById<TextView>(Resource.Id.ECG);
            TextView EMG = FindViewById<TextView>(Resource.Id.EMG);
            // Mise en page ECG/EMG;
            ECG.SetWidth(display.WidthPixels / 2);
            EMG.SetWidth(display.WidthPixels / 2);
            Button getECG = FindViewById<Button>(Resource.Id.getECG);
            Button getEMG = FindViewById<Button>(Resource.Id.getEMG);
            //Mise en page ECG/EMG
            getECG.SetWidth(display.WidthPixels / 2);
            getEMG.SetWidth(display.WidthPixels / 2);

            #endregion

            // Initie la connection avec l'arduino via le bluetooth
            Connexion.Click += async (sender, args) =>
            {
                Connexion.Enabled = false;
                await btConnection.conect(status, this);
                if (btConnection.thisSocket.IsConnected == true)
                {
                    // Active/desactive les bouton;
                    Connexion.Enabled = false;
                    BtValide.Enabled = true;
                    getECG.Enabled = true;
                    getEMG.Enabled = true;

                    // Permet de lire les information recus en continue dans un thread distinct;
                    await btConnection.Recive(ECG, EMG, this);
                }
            };

            // Bouton validation des information du patients;
            BtValide.Click += delegate
            {
                LastName.Enabled = false;
                FirstName.Enabled = false;
                seuil.Enabled = false;
                BtValide.Enabled = false;
            };

            // Envoie la demande de la fréquence cardiaque vers l'arduino via Bluetooth;
            getECG.Click += async (sender, args) =>
            {
                await btConnection.send("{FC}", this);
            };

            // Envoie la demande de l'EMG vers l'arduino via Bluetooth;
            getEMG.Click += async (sender, args) =>
            {
                await btConnection.send("{EMG}", this);
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
       
}