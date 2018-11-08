using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Aspit.StudentReg.DataAccess;
using Aspit.StudentReg.Entities;

namespace Aspit.StudentReg.GUI.App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            DisplayAlert("Alert1", "You have been alerted", "OK");

            /*StudentView.ItemsSource = StudentsRepository.GetAll();*/
        }
    }
}
