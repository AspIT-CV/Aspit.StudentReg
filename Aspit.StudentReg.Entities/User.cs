using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if(value is null)
                {
                    throw new ArgumentNullException();
                }
                //TODO add checker ting ting
                name = value;
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
                //TODO add checker ting ting
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
