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
        /// The student the user is editing
        /// </summary>
        private Student selectedStudent;

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

                selectedStudent = students[StudentDataGrid.SelectedIndex];
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
            SaveButton.IsEnabled = false;
            NameTextBox.Text = "";
            UniLoginTextBox.Text = "";

            NameTextBox.IsEnabled = enable;
            UniLoginTextBox.IsEnabled = enable;

            if(!enableForNew || !enable)
            {
                ShowRegistrationsButton.IsEnabled = enable;
                RegistrationInformationViewer.IsEnabled = enable;
            }

            ValidateInformation();
            if(!enable)
            {
                ErrorLabel.Content = "";
            }
        }

        /// <summary>
        /// Invoked when any one of the information text boxes
        /// </summary>
        private void StudentInformation_Changed(object sender, TextChangedEventArgs e)
        {
            ValidateInformation();
        }

        /// <summary>
        /// Validates the information in the text boxes
        /// </summary>
        private void ValidateInformation()
        {
            try
            {
                Student.ValidateName(NameTextBox.Text);
            }
            catch(ArgumentNullException)
            {
                ErrorLabel.Content = "Navnet må ikke være ingenting.";
                SaveButton.IsEnabled = false;
                return;
            }
            catch(ArgumentException)
            {
                ErrorLabel.Content = "Navnet kan kun indeholde bogstaver.";
                SaveButton.IsEnabled = false;
                return;
            }

            try
            {
                Student.ValidateUniLogin(UniLoginTextBox.Text);
            }
            catch(ArgumentNullException)
            {
                ErrorLabel.Content = "Unilogin må ikke være ingenting.";
                SaveButton.IsEnabled = false;
                return;
            }
            catch(ArgumentException)
            {
                ErrorLabel.Content = "Unilogin er ugyldigt.";
                SaveButton.IsEnabled = false;
                return;
            }

            ErrorLabel.Content = "";
            SaveButton.IsEnabled = true;
        }

        /// <summary>
        /// Invoked when the save button has been clicked - Saves/Creates the student
        /// </summary>
        private void SaveButton_Clicked(object sender, RoutedEventArgs e)
        {
            bool isNewStudent = false;
            if(selectedStudent == null)
            {
                selectedStudent = new Student(0,"newStudent","news1234");
                isNewStudent = true;
            }

            selectedStudent.Name = NameTextBox.Text;
            selectedStudent.UniLogin = UniLoginTextBox.Text;

            if(isNewStudent)
            {
                studentsRepository.CreateStudent(selectedStudent);
            }
            else
            {
                studentsRepository.UpdateStudent(selectedStudent);
            }

            UpdateStudentList();
            EnableEditing(false);
        }

        /// <summary>
        /// Invoked when the create new student button has been clicked
        /// </summary>
        private void CreateNewButton_Clicked(object sender, RoutedEventArgs e)
        {
            selectedStudent = null;
            StudentDataGrid.SelectedIndex = -1;
            EnableEditing(true,true);
        }
    }
}
