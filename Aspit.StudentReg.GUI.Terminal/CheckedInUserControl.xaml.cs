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
        public CheckedInUserControl(Student student, MainWindow parentArg)
        {
            parent = parentArg;
            InitializeComponent();
            mainTextBlock.Text = "Du blev checket ind " + student.Name;
            StudentsRepository studentsRepository = new StudentsRepository(RepositoryBase.RetrieveConnectionString());
            studentsRepository.CheckIn(student, DateTime.Now);
            goBackToStart(); 
        }

        private async Task goBackToStart()
        {
            await Task.Delay(3000);
            parent.Content = new StartScreenUserControl(parent);
        }
    }
}
