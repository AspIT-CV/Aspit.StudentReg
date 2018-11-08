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
        /// Repository for AttendanceRegistrations
        /// </summary>
        private AttendanceRegistrationsRepository registrationsRepository;

        /// <summary>
        /// Repository for students
        /// </summary>
        private StudentsRepository studentsRepository;

        /// <summary>
        /// A list of all students
        /// </summary>
        private List<Student> students;

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
                registrationsRepository = new AttendanceRegistrationsRepository(RepositoryBase.RetrieveConnectionString());
                studentsRepository = new StudentsRepository(RepositoryBase.RetrieveConnectionString());
            }
            catch
            {
                MessageBox.Show("Kunne ikke forbinde til databasen.");
                Close();
                return;
            }
            UpdateStudentList();
        }

        /// <summary>
        /// Updates the list of students
        /// </summary>
        private void UpdateStudentList()
        {
            students = studentsRepository.GetAll();
            StudentDataGrid.ItemsSource = (from student in students
                                           let Id = student.Id
                                           let Navn = student.Name
                                           let UniLogin = student.UniLogin
                                           let Status = (student.AttendanceRegistrations.IsDefault()) ? "Væk" : "Her" 
                                           select new {Id, Status, Navn, UniLogin});
            StudentDataGrid.Columns[0].Visibility = Visibility.Collapsed;
        }
    }
}
