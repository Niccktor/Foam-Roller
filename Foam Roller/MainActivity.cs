using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using Android.Bluetooth;
using Android.Util;

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
            DisplayMetrics display = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(display);
            

            SetContentView(Resource.Layout.activity_main);
            LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            EditText UserNameText = FindViewById<EditText>(Resource.Id.UserNameText);
            TextView ResponseText = FindViewById<TextView>(Resource.Id.ResponseText);
            Button BtValide = FindViewById<Button>(Resource.Id.BtValide);

            GridLayout grid = FindViewById<GridLayout>(Resource.Id.gridLayout1);
            Button test1 = FindViewById<Button>(Resource.Id.test1);
            Button test2 = FindViewById<Button>(Resource.Id.test2);
            Button test3 = FindViewById<Button>(Resource.Id.test3);

            test1.SetWidth(display.WidthPixels / 3);
            test2.SetWidth(display.WidthPixels / 3);
            test3.SetWidth(display.WidthPixels / 3);






            // Initialisation Bluetooth
            TextView BluetoothStatusText = FindViewById<TextView>(Resource.Id.BluetoothStatusText);
            TextView test = FindViewById<TextView>(Resource.Id.BluetoothStatusText); 
            BluetoothAdapter Bluetooth = BluetoothAdapter.DefaultAdapter;
            if (Core_Bluetooth.Bluetooth.isBluetoothSupported(Bluetooth, BluetoothStatusText))
            {
                Core_Bluetooth.Bluetooth.showPairedDevices(Bluetooth, test);
            }


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