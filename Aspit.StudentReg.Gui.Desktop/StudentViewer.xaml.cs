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
        /// Invoke when page to should change to registrations
        /// </summary>
        public RoutedEventHandler GoToViewScreen { get; set; }

        /// <summary>
        /// Intializes this StudentViewer's items
        /// </summary>
        public StudentViewer Intialize(StudentsRepository repository, AttendanceRegistrationsRepository registrationRepository)
        {
            StudentsListBox.SelectedIndex = -1;
            registrationsRepository = registrationRepository;
            studentsRepository = repository;
            UpdateStudentList();
            EnableEditing(false);

            return this;
        }

        /// <summary>
        /// Creates a dockpanel showing information about a student
        /// </summary>
        /// <param name="student">The student to show information for</param>
        /// <returns>A dockpanel containing information about the student</returns>
        private static DockPanel StudentView(Student student)
        {
            DockPanel studentDockPanel = new DockPanel() {LastChildFill = false };

            Label isHere = new Label() { Width = 20, Height = 20 };
            if(student.AttendanceRegistrations.IsDefault())
            {
                isHere.Background = Brushes.Red;
            }
            else
            {
                isHere.Background = Brushes.Green;
            }
            DockPanel.SetDock(isHere, Dock.Left);
            studentDockPanel.Children.Add(isHere);

            Label name = new Label() {Content = student.Name };
            DockPanel.SetDock(name, Dock.Left);
            studentDockPanel.Children.Add(name);

            Label uniLogin = new Label() { Content = student.UniLogin };
            DockPanel.SetDock(uniLogin, Dock.Right);
            studentDockPanel.Children.Add(uniLogin);

            return studentDockPanel;
        }

        /// <summary>
        /// Updates the list of students
        /// </summary>
        private void UpdateStudentList(string searchString = "")
        {
            searchString = searchString.Trim().ToLower();
            StudentsListBox.SelectedIndex = -1;
            students = studentsRepository.GetAll();

            //remove students who aren't found in the search
            students.RemoveAll((student) => !(student.ToString().ToLower().Contains(searchString) || student.UniLogin.Contains(searchString)));

            students.Sort(Student.Compare);

            StudentsListBox.Items.Clear();
            foreach(Student student in students)
            {
                StudentsListBox.Items.Add(StudentView(student));
            }
        }

        /// <summary>
        /// Invoked when StudentDataGrid selection has changed
        /// Choses which student the controller is changing
        /// </summary>
        private void StudentsListBox_Changed(object sender, SelectionChangedEventArgs e)
        {
            if(StudentsListBox.SelectedIndex != -1)
            {
                EnableEditing(true);

                selectedStudent = students[StudentsListBox.SelectedIndex];
                if(selectedStudent.AttendanceRegistrations.IsDefault())
                {
                    RegistrationInformationViewer.AttendanceRegistration = default;
                }
                else
                {
                    RegistrationInformationViewer.AttendanceRegistration = selectedStudent.AttendanceRegistrations;
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
        /// Invoked when any one of the information text boxes' text changes
        /// Calls <see cref="ValidateInformation"/>
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
        /// Invoked when the save button has been clicked
        /// Saves/Creates the student
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
        /// enables the create new student gui
        /// </summary>
        private void CreateNewButton_Clicked(object sender, RoutedEventArgs e)
        {
            selectedStudent = null;
            StudentsListBox.SelectedIndex = -1;
            EnableEditing(true,true);
        }

        /// <summary>
        /// Invoked when the AttendanceRegistration-usercontrol's button has been clicked
        /// saves the registration in the student
        /// </summary>
        private void RegistrationSaveButton_Clicked(object sender, RoutedEventArgs e)
        {
            selectedStudent.AttendanceRegistrations = RegistrationInformationViewer.AttendanceRegistration;
            if(RegistrationInformationViewer.IsNew)
            {
                registrationsRepository.CreateRegistration(selectedStudent);
                studentsRepository.UpdateStudent(selectedStudent);
            }
            else
            {
                registrationsRepository.Update(selectedStudent.AttendanceRegistrations);
                if(selectedStudent.AttendanceRegistrations.LeavingTime.TimeOfDay != default)
                {
                    selectedStudent.AttendanceRegistrations = default;
                    studentsRepository.UpdateStudent(selectedStudent);
                }
            }

            UpdateStudentList();
            EnableEditing(false);
        }

        /// <summary>
        /// Invoked when the show registrations button has been clicked
        /// Changes the visible usercontrol into a place where you can see all the registrations for the student
        /// </summary>
        private void ShowRegistrationsButton_Clicked(object sender, RoutedEventArgs e)
        {
            GoToViewScreen?.Invoke(selectedStudent, null);
        }

        /// <summary>
        /// Invoked when the refresh button has been clicked
        /// refreshes the student list
        /// </summary>
        private void RefreshButtn_Clicked(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            UpdateStudentList();
        }

        /// <summary>
        /// Invoked when the search button has been clicked
        /// Searches using the searchtextbox string and shows result in the list
        /// </summary>
        private void SearchButton_Clicked(object sender, RoutedEventArgs e)
        {
            UpdateStudentList(SearchTextBox.Text);
        }
    }
}
