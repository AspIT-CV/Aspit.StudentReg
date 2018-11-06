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

        public User(int id, string name)
        {
            Id = id;
            Name = name;
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
                    throw new ArgumentOutOfRangeException("Id cannot be less than 0");
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
                    throw new ArgumentNullException("Name cannot be null");
                }

                //Trim whitespace
                value = value.Trim();

                //Check if value only consits of letters and whitespace
                Regex reg = new Regex(@"^([a-zA-Z ]+)$");
                if (!reg.IsMatch(value))
                {
                    throw new ArgumentException("Name is invalid");
                }
                //Make the first letter of each word uppercase
                name = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
            }
        }
    }
}
