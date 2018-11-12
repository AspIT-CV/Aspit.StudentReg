using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Windows.Media;

namespace Aspit.StudentReg.Entities
{
    public class Student : User
    {
        /// <summary>
        /// The student's unilogin
        /// </summary>
        string uniLogin;

        /// <summary>
        /// The student's current <see cref="AttendanceRegistration"/>
        /// </summary>
        AttendanceRegistration attendanceRegistrations;

        SolidColorBrush color;

        string icon;

        /// <summary>
        /// Intializes a new <see cref="Student"/> using the given parameters
        /// </summary>
        /// <param name="id">The student's id</param>
        /// <param name="name">The student's name</param>
        /// <param name="uniLogin">The student's unilogin</param>
        /// <param name="attendanceRegistrations">The student's current <see cref="AttendanceRegistration"/></param>
        public Student(int id, string name, string uniLogin, AttendanceRegistration attendanceRegistrations = default):base(id,name)
        {
            Id = id;
            Name = name;
            UniLogin = uniLogin;
            AttendanceRegistrations = attendanceRegistrations;
            colorSet();
            iconset();
        }

        /// <summary>
        /// Gets or sets the student's unilogin
        /// </summary>
        public string UniLogin
        {
            get
            {
                return uniLogin;
            }

            set
            {
                ValidateUniLogin(value);
                value = value.Trim();
                uniLogin = value;
            }
        }

        /// <summary>
        /// Gets or sets the student's current <see cref="AttendanceRegistration"/>
        /// </summary>
        public AttendanceRegistration AttendanceRegistrations
        {
            get
            {
                return attendanceRegistrations;
            }

            set
            {
                attendanceRegistrations = value;
            }
        }

        public SolidColorBrush Color
        {
            get
            {
                return this.color;
            }

            set
            {
                this.color = value;
            }
        }

        public string Icon
        {
            get
            {
                return this.icon;
            }

            set
            {
                this.icon = value;
            }
        }

        /// <summary>
        /// Tests if a uniLogin string is valid
        /// </summary>
        /// <param name="uniLogin">The uniLogin in test</param>
        /// <returns>true if the uniLogin is valid, throws error if false</returns>
        public static bool ValidateUniLogin(string uniLogin)
        {
            if(string.IsNullOrWhiteSpace(uniLogin))
            {
                throw new ArgumentNullException("Unilogin cannot be null");
            }

            //Trim whitespace
            uniLogin = uniLogin.Trim();

            //Check if value is a correct unilogin format
            Regex reg = new Regex(@"^[a-x]{4}[a-x0-9]{4}$");
            if (!reg.IsMatch(uniLogin))
            {
                throw new ArgumentException("Unilogin is invalid");
            }

            return true;
        }

        public override string ToString()
        {
            if (AttendanceRegistrations.IsDefault())
            {
                return "✗ - " + Name;
            } else
            {
                return "✓ - " + Name;
            }
        }

        public void colorSet()
        {
            if (AttendanceRegistrations.IsDefault())
            {
                Color = new BrushConverter().ConvertFromString("#27ae60") as SolidColorBrush;
            }
            else
            {
                Color = new BrushConverter().ConvertFromString("#c0392b") as SolidColorBrush;
            }
        }

        public void iconset()
        {
            if (AttendanceRegistrations.IsDefault())
            {
                Icon = "✗";
            }
            else
            {
                Icon = "✓";
            }
        }

        /// <summary>
        /// Compares two students and outputs a number based on which student should show first in a list
        /// </summary>
        /// <param name="student1">The first student</param>
        /// <param name="student2">The second student</param>
        /// <returns>returns 1 if the first student should be highest, -1 if lowest and 0 if it doesnt matter</returns>
        public static int Compare(Student student1, Student student2)
        {
            if(student1.AttendanceRegistrations.IsDefault() && !student2.AttendanceRegistrations.IsDefault())
            {
                return 1;
            }
            else if(!student1.AttendanceRegistrations.IsDefault() && student2.AttendanceRegistrations.IsDefault())
            {
                return -1;
            }
            else
            {
                return string.Compare(student1.ToString(), student2.ToString());
            }
        }
    }
}
