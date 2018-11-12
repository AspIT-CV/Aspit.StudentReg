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
        /// <summary>
        /// The user's id
        /// </summary>
        int id;

        /// <summary>
        /// The user's name
        /// </summary>
        string name;

        /// <summary>
        /// Intializes a new <see cref="User"/> with the given id and name
        /// </summary>
        /// <param name="id">The user's id</param>
        /// <param name="name">The user's full name</param>
        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Gets or sets the user's id
        /// </summary>
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

        /// <summary>
        /// Gets or sets the user's name
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                ValidateName(value);
                value = value.Trim();
                name = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
            }
        }

        public static bool ValidateName(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Name cannot be null");
            }

            //Trim whitespace
            name = name.Trim();

            //Check if value only consits of letters and whitespace
            Regex reg = new Regex(@"^([\p{L} ]+)$");
            if(!reg.IsMatch(name))
            {
                throw new ArgumentException("Name is invalid");
            }

            return true;
        }
    }
}
