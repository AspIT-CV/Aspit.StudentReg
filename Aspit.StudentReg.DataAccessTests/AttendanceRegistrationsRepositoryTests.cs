using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aspit.StudentReg.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspit.StudentReg.Entities;

namespace Aspit.StudentReg.DataAccess.Tests
{
    [TestClass()]
    public class AttendanceRegistrationsRepositoryTests
    {
        public static AttendanceRegistrationsRepository CreateRepository()
        {
            return new AttendanceRegistrationsRepository(RepositoryBase.RetrieveConnectionString());
        }

        [TestMethod]
        public void Initialization()
        {
            CreateRepository();
        }

        [TestMethod()]
        public void GetAllTest()
        {
            AttendanceRegistrationsRepository repository = CreateRepository();
            List<AttendanceRegistration> list = repository.GetAll();

            Assert.AreNotEqual(0, list.Count);
        }

        [TestMethod()]
        public void CreateRegistrationTest()
        {
            AttendanceRegistrationsRepository repository = CreateRepository();
            Student student = new Student(0,"bla","bla12345",new AttendanceRegistration {MeetingTime = DateTime.Now.AddMilliseconds(-1), LeavingTime = DateTime.Now });

            repository.CreateRegistration(student);

            Assert.AreNotEqual(0,student.AttendanceRegistrations.Id);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            AttendanceRegistrationsRepository repository = CreateRepository();
            Student student = new Student(0, "bla", "bla12345", new AttendanceRegistration { MeetingTime = DateTime.Now.AddMilliseconds(-1), LeavingTime = DateTime.Now });

            repository.CreateRegistration(student);

            repository.Update(student.AttendanceRegistrations);
        }

        [TestMethod()]
        public void GetFromIdTest()
        {
            AttendanceRegistrationsRepository repository = CreateRepository();
            Student student = new Student(0, "bla", "bla12345", new AttendanceRegistration { MeetingTime = new DateTime(2018, 5, 2, 8, 10, 5), LeavingTime = new DateTime(2018, 5, 2, 8, 10, 6) });

            repository.CreateRegistration(student);

            AttendanceRegistration registration = repository.GetFromId(student.AttendanceRegistrations.Id);
            Assert.AreEqual(new DateTime(2018, 5, 2, 8, 10, 5), registration.MeetingTime);
            Assert.AreEqual(new DateTime(2018, 5, 2, 8, 10, 6), registration.LeavingTime);
        }

        [TestMethod()]
        public void GetUsersRegistrationsTest()
        {
            AttendanceRegistrationsRepository repository = CreateRepository();
            Student student = new Student(0, "bla", "bla12345", new AttendanceRegistration { MeetingTime = DateTime.Now.AddMilliseconds(-1), LeavingTime = DateTime.Now });

            repository.CreateRegistration(student);
            student.AttendanceRegistrations = new AttendanceRegistration { MeetingTime = new DateTime(2018, 5, 2, 8, 10, 5), LeavingTime = new DateTime(2018, 5, 2, 8, 10, 6) };
            repository.CreateRegistration(student);

            List<AttendanceRegistration> registrations = repository.GetUsersRegistrations(student);
            Assert.IsTrue(registrations.Count >= 2);
        }
    }
}