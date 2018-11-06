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
        }

        [TestMethod()]
        public void UpdateTest()
        {
            AttendanceRegistrationsRepository repository = CreateRepository();
        }
    }
}