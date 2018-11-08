using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Aspit.StudentReg.DataAccess;
using Aspit.StudentReg.Entities;

namespace Aspit.StudentReg.GUI.AndroidApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            try
            {
                SetContentView(Resource.Layout.activity_main);
                StudentsRepository StudentsRepositorys = new StudentsRepository("Server=cvdb3,1444;Database=Aspit.StudentRegDB;User Id=SRSAPPUSER;Password=IsVerySecret;");
            }
            catch (System.Exception)
            {
                throw;
            }

        }
    }
}