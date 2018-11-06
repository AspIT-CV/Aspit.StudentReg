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
    [TestClass]
    public class StudentsRepositoryTests
    {

        public static StudentsRepository CreateRepository()
        {
            return new StudentsRepository(RepositoryBase.RetrieveConnectionString());
        }

        [TestMethod()]
        public void GetAllTest()
        {
            StudentsRepository StudentsRepositorys = new StudentsRepository(RepositoryBase.RetrieveConnectionString());
            List<Student> list = StudentsRepositorys.GetAll();

            Assert.AreNotEqual(0, list.Count);
        }

        [TestMethod()]
        public void GetFromIdTest()
        {
            StudentsRepository repository = CreateRepository();
            Student student = new Student(0, "bla", "blax2345");

            //repository.CreateRegistration(student);

            //AttendanceRegistration registration = repository.GetFromId(student.AttendanceRegistrations.Id);
            //Assert.AreEqual(new DateTime(2018, 5, 2, 8, 10, 5), registration.MeetingTime);
            //Assert.AreEqual(new DateTime(2018, 5, 2, 8, 10, 6), registration.LeavingTime);
        }
    }
}
