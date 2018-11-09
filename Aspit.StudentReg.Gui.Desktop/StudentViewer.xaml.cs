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
using Aspit.StudentReg.DataAccess;
using Aspit.StudentReg.Entities;

namespace Aspit.StudentReg.Gui.Desktop
{
    /// <summary>
    /// Interaction logic for StudentViewer.xaml
    /// </summary>
    public partial class StudentViewer : UserControl
    {
        /// <summary>
        /// Repository for students
        /// </summary>
        private StudentsRepository studentsRepository;

        /// <summary>
        /// Repository for <see cref="AttendanceRegistration"/>s
        /// </summary>
        AttendanceRegistrationsRepository registrationsRepository;

        /// <summary>
        /// A list of all students
        /// </summary>
        private List<Student> students;

        /// <summary>
        /// Intializes a new <see cref="StudentViewer"/>
        /// </summary>
        public StudentViewer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Intializes this StudentViewer's items
        /// </summary>
        public StudentViewer Intialize(StudentsRepository repository, AttendanceRegistrationsRepository registrationRepository)
        {
            registrationsRepository = registrationRepository;
            studentsRepository = repository;
            UpdateStudentList();
            EnableEditing(false);

            return this;
        }

        /// <summary>
        /// Updates the list of students
        /// </summary>
        private void UpdateStudentList()
        {
            students = studentsRepository.GetAll();
            StudentDataGrid.ItemsSource = (from student in students
                                           let Navn = student.Name
                                           let UniLogin = student.UniLogin
                                           let Status = (student.AttendanceRegistrations.IsDefault()) ? "Væk" : "Her"
                                           select new
                                           {
                                               Status,
                                               Navn,
                                               UniLogin
                                           });
        }

        /// <summary>
        /// Invoked when StudentDataGrid selection has changed
        /// </summary>
        private void StudentDateGrid_Changed(object sender, SelectionChangedEventArgs e)
        {
            if(StudentDataGrid.SelectedIndex != -1)
            {
                EnableEditing(true);
                Student selectedStudent = students[StudentDataGrid.SelectedIndex];
                if(selectedStudent.AttendanceRegistrations.IsDefault())
                {
                    RegistrationInformationViewer.AttendanceRegistration = default;
                }
                else
                {
                    RegistrationInformationViewer.AttendanceRegistration = registrationsRepository.GetFromId(selectedStudent.AttendanceRegistrations.Id);
                }
                NameTextBox.Text = selectedStudent.Name;
                UniLoginTextBox.Text = selectedStudent.UniLogin;
            }
            else
            {
                EnableEditing(false);
            }
        }

        /// <summary>
        /// Enables all text boxes and such so you can change information for the student
        /// </summary>
        /// <param name="enable">If the UI should be enabled or disabled</param>
        /// <param name="enableForNew">If the UI for time registrations should be changed too</param>
        private void EnableEditing(bool enable, bool enableForNew = false)
        {
            NameTextBox.IsEnabled = enable;
            UniLoginTextBox.IsEnabled = enable;
            SaveButton.IsEnabled = false;

            if(!enableForNew || !enable)
            {
                ShowRegistrationsButton.IsEnabled = enable;
                RegistrationInformationViewer.IsEnabled = enable;
            }
        }
    }
}
