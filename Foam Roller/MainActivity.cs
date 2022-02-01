using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using Android.Bluetooth;

namespace Foam_Roller
{
    [Activity(Label = "@string/app_name", 
        Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource

            SetContentView(Resource.Layout.activity_main);
            EditText UserNameText = FindViewById<EditText>(Resource.Id.UserNameText);
            TextView ResponseText = FindViewById<TextView>(Resource.Id.ResponseText);
            Button BtValide = FindViewById<Button>(Resource.Id.BtValide);

            // Initialisation Bluetooth
            TextView BluetoothStatusText = FindViewById<TextView>(Resource.Id.BluetoothStatus);
            BluetoothAdapter Bluetooth = BluetoothAdapter.DefaultAdapter;
            Core_Bluetooth.Bluetooth.isBluetoothSupported(Bluetooth, BluetoothStatusText);

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

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}