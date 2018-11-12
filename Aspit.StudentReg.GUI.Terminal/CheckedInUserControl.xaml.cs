using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Aspit.StudentReg.Entities;
using Aspit.StudentReg.DataAccess;

namespace Aspit.StudentReg.GUI.Terminal
{
    /// <summary>
    /// Interaction logic for CheckedInUserControl.xaml
    /// </summary>
    public partial class CheckedInUserControl : UserControl
    {
        private MainWindow parent;

        public CheckedInUserControl(Student student, MainWindow parentArg, bool checkIn)
        {
            parent = parentArg;
            InitializeComponent();
            //Check if the action was check in
            if (checkIn)
            {
                //Display check in text
                mainTextBlock.Text = "Du blev checket ind " + student.Name;

                //Change Backgroundcolor to green
                Background = new BrushConverter().ConvertFromString("#27ae60") as SolidColorBrush;
            } else
            {
                //Display check out text
                mainTextBlock.Text = "Du blev checket ud " + student.Name;

                //Change Backgroundcolor to red
                Background = new BrushConverter().ConvertFromString("#c0392b") as SolidColorBrush;
            }           
            GoBackToStart();
        }

        /// <summary>
        /// Makes usercontrol go back to <see cref="StartScreenUserControl"/> after 3 seconds
        /// </summary>
        private async void GoBackToStart()
        {
            //Wait 3 seconds
            await Task.Delay(3000);

            //Redirect to StartScreenUserControl
            parent.Content = new StartScreenUserControl(parent);
        }
    }
}
