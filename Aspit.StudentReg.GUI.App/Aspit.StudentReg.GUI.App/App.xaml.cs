using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using Aspit.StudentReg.DataAccess;
using Aspit.StudentReg.Entities;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Aspit.StudentReg.GUI.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            try
            {

               
                //StudentsRepository repo = new StudentsRepository("Data Source=cvdb3,1444;Initial Catalog=Aspit.StudentRegDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            }
            catch (Exception)
            {

                
            }

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
