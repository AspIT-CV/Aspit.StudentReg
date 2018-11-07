using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aspit.StudentReg.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspit.StudentReg.Entities;

namespace Aspit.StudentReg.Tests
{
    [TestClass()]
    public class AttendanceRegistrationsRepositoryTests
    {
        /// <summary>
        /// Creates a repository
        /// </summary>
        /// <returns>Returns a new <see cref="AttendanceRegistrationsRepository"/></returns>
        public static AttendanceRegistrationsRepository CreateRepository()
        {
            return new AttendanceRegistrationsRepository(RepositoryBase.RetrieveConnectionString());
        }

        [TestMethod]
        public void Initialization()
        {
            //Tests if a connection with the connection string can be made
            CreateRepository();
        }

        [TestMethod()]
        public void GetAllTest()
        {
            //Tests if GetAll can get any AttendanceRegistration from database
            AttendanceRegistrationsRepository repository = CreateRepository();
            List<AttendanceRegistration> list = repository.GetAll();

            Assert.AreNotEqual(0, list.Count);
        }

        [TestMethod()]
        public void CreateRegistrationTest()
        {
            //Tests if newly created AttendanceRegistration gets an id on creation
            AttendanceRegistrationsRepository repository = CreateRepository();
            Student student = new Student(1,"bla","blax2345",new AttendanceRegistration {MeetingTime = DateTime.Now.AddMilliseconds(-1), LeavingTime = DateTime.Now });

            repository.CreateRegistration(student);

            Assert.AreNotEqual(0,student.AttendanceRegistrations.Id);

            //Tests if a student without an AttendanceRegistration throws an error
            student = new Student(1, "bla", "blax2345");
            Assert.ThrowsException<ArgumentException>(() => repository.CreateRegistration(student));

            //Tests if a student which is null throws an error
            student = null;
            Assert.ThrowsException<ArgumentNullException>(() => repository.CreateRegistration(student));
        }

        [TestMethod()]
        public void UpdateTest()
        {
            //Tests if update doesnt throw any error
            AttendanceRegistrationsRepository repository = CreateRepository();
            Student student = new Student(1, "bla", "blax2345", new AttendanceRegistration { MeetingTime = DateTime.Now.AddMilliseconds(-1), LeavingTime = DateTime.Now });

            repository.CreateRegistration(student);
            repository.Update(student.AttendanceRegistrations);

            //Tests if update throws an error if AttendanceRegistration is default value
            Assert.ThrowsException<ArgumentException>(() => repository.Update(default));
        }

        [TestMethod()]
        public void GetFromIdTest()
        {
            //Tests if GetFromId works
            AttendanceRegistrationsRepository repository = CreateRepository();
            Student student = new Student(1, "bla", "blax2345", new AttendanceRegistration { MeetingTime = new DateTime(2018, 5, 2, 8, 10, 5), LeavingTime = new DateTime(2018, 5, 2, 8, 10, 6) });

            repository.CreateRegistration(student);

            AttendanceRegistration registration = repository.GetFromId(student.AttendanceRegistrations.Id);
            Assert.AreEqual(new DateTime(2018, 5, 2, 8, 10, 5), registration.MeetingTime);
            Assert.AreEqual(new DateTime(2018, 5, 2, 8, 10, 6), registration.LeavingTime);
        }

        [TestMethod()]
        public void GetUsersRegistrationsTest()
        {
            //Tests if GetUsersRegistrations output the correct amount of AttendanceRegistrations
            AttendanceRegistrationsRepository repository = CreateRepository();
            Student student = new Student(1, "bla", "blax2345", new AttendanceRegistration { MeetingTime = DateTime.Now.AddMilliseconds(-1), LeavingTime = DateTime.Now });

            repository.CreateRegistration(student);
            student.AttendanceRegistrations = new AttendanceRegistration { MeetingTime = new DateTime(2018, 5, 2, 8, 10, 5), LeavingTime = new DateTime(2018, 5, 2, 8, 10, 6) };
            repository.CreateRegistration(student);

            List<AttendanceRegistration> registrations = repository.GetUsersRegistrations(student);
            Assert.IsTrue(registrations.Count >= 2);

            //Tests if its throws an error if Student is null
            Assert.ThrowsException<ArgumentNullException>(() => repository.GetUsersRegistrations(null));
        }
    }
}