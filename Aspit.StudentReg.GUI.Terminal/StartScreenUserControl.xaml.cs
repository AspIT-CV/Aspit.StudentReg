﻿using System;
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
    /// Interaction logic for StartScreenUserControl.xaml
    /// </summary>
    /// 
    public partial class StartScreenUserControl : UserControl
    {
        private MainWindow parent;
        public StartScreenUserControl(MainWindow parentArg)
        {
            parent = parentArg;
            InitializeComponent();
            StudentsRepository studentsRepository = new StudentsRepository(RepositoryBase.RetrieveConnectionString());

            //Populate StudentListbox with all students
            StudentListbox.DisplayMemberPath = Name;

            StudentListbox.ItemsSource = studentsRepository.GetAll();




        }

        private void StudentListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Set usercontrol to CheckInOrOutPromt with the selcted student 
            Student student = StudentListbox.SelectedItem as Student;
            parent.Content = new CheckInOrOutPromt(student, parent);
        }
    }
}
