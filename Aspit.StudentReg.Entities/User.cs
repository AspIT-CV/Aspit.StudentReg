using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Aspit.StudentReg.Entities
{
    /// <summary>
    /// Represents a user of the system.
    /// </summary>
    public class User
    {
        int id;
        string name;
        string username;
        int attendanceRegistrationsKey;

        public User(int id, string name, string username, int AttendanceRegistrationsKey)
        {
            Id = id;
            Name = name;
            Username = username;
            AttendanceRegistrationsKey = attendanceRegistrationsKey;
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                if(value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                //Check if value is null
                if(value is null)
                {
                    throw new ArgumentNullException();
                }

                //Trim whitespace
                value = value.Trim();

                //Check if value only consits of letters and whitespace
                var reg = new Regex(@"[\p{L} ]+$");
                if (!reg.IsMatch(value))
                {
                    throw new ArgumentException();
                }
                //Make the first letter of each word uppercase
                name = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
            }
        }

        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                //Check if value is null
                if(value is null)
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
                username = value;
            }
        }

        public int AttendanceRegistrationsKey
        {
            get
            {
                return attendanceRegistrationsKey;
            }

            set
            {
                //TODO add checker ting ting
                attendanceRegistrationsKey = value;
            }
        }
    }
}
