using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Aspit.StudentReg.Entities
{
    public class Student : User
    {

        string uniLogin;
        AttendanceRegistration attendanceRegistrations;

        public Student(int id, string name, string uniLogin, AttendanceRegistration attendanceRegistrations = null):base(id,name)
        {
            Id = id;
            Name = name;
            UniLogin = uniLogin;
            AttendanceRegistrations = attendanceRegistrations;
        }

        public string UniLogin
        {
            get
            {
                return uniLogin;
            }

            set
            {
                //Check if value is null
                if (value is null)
                {
                    throw new ArgumentNullException();
                }

                //Trim whitespace
                value = value.Trim();

                //Check if value is a correct unilogin format
                var reg = new Regex(@"[a-x0-9]{8}");
                if (!reg.IsMatch(value))
                {
                    throw new ArgumentException();
                }
                uniLogin = value;
            }
        }

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

    }
}
