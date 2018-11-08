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

namespace Aspit.StudentReg.Gui.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window
    {
        /// <summary>
        /// Repository for <see cref="Student"/>s
        /// </summary>
        private StudentsRepository studentsRepository;

        /// <summary>
        /// Repository for <see cref="AttendanceRegistration"/>s
        /// </summary>
        private AttendanceRegistrationsRepository registrationsRepository;

        /// <summary>
        /// Intializes a new <see cref="MainWindow"/>
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invoked when MainWindow has loaded
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                studentsRepository = new StudentsRepository(RepositoryBase.RetrieveConnectionString());
                registrationsRepository = new AttendanceRegistrationsRepository(RepositoryBase.RetrieveConnectionString());
                StudentViewerControl.Intialize(studentsRepository);
            }
            catch
            {
                if(MessageBox.Show("Kunne ikke forbinde til databasen.\n\nVil du prøve at forbinde igen?", "Ingen forbindelse", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Window_Loaded(null, null);
                    return;
                }
                else
                {
                    Close();
                    return;
                }
            }
        }
    }
}
