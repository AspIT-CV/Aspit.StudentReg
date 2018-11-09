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

            if (checkIn)
            {
                mainTextBlock.Text = "Du blev checket ind " + student.Name;
                this.Background = new BrushConverter().ConvertFromString("#27ae60") as SolidColorBrush;
            } else
            {
                mainTextBlock.Text = "Du blev checket ud " + student.Name;
                this.Background = new BrushConverter().ConvertFromString("#c0392b") as SolidColorBrush;
            }           
            goBackToStart(); 
        }

        private async Task goBackToStart()
        {
            await Task.Delay(3000);
            parent.Content = new StartScreenUserControl(parent);
        }
    }
}
