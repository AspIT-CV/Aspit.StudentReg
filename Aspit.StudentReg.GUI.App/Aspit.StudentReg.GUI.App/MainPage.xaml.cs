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
            this.Title = "Hej, hvem er du?";

            StudentsRepository StudentsRepository = new StudentsRepository(RepositoryBase.RetrieveConnectionString());
            
            StudentView.ItemsSource = StudentsRepository.GetAll();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new User());
        }
    }
}
