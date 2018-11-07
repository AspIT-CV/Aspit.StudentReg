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
    public class StudentsTests
    {
        readonly int id = 1;
        readonly string name = " per Bosen ";
        readonly string expectedName = "Per Bosen";
        readonly string uniLogin = "perx234k";

        public Student GetBasicStudent()
        {
            return new Student(id, name, uniLogin);
        }

        [TestMethod]
        public void Initialization()
        {
            Assert.AreNotEqual(null, GetBasicStudent());
        }

        [TestMethod]
        public void StudentValueTest()
        {
            Student student = GetBasicStudent();

            Assert.AreEqual(id, student.Id);
            Assert.AreEqual(expectedName, student.Name);
            Assert.AreEqual(uniLogin, student.UniLogin);
        }

        [TestMethod]
        public void StudentAttendanceRegistrationsAssignment()
        {
            AttendanceRegistration registration = new AttendanceRegistration
            {
                Id = 1,
                LeavingTime = new DateTime(2018, 6, 11, 23, 59, 59),
                MeetingTime = new DateTime(2018, 6, 11, 9, 0, 0)
            };
            Student student = GetBasicStudent();

            student.AttendanceRegistrations = registration;
            Assert.AreEqual(registration, student.AttendanceRegistrations);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "Unilogin is invalid")]
        public void StudentUniLoginError()
        {
            Student student = new Student(id, name, "PerBo");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "Name is invalid")]
        public void StudentNameError()
        {
            Student student = new Student(id, "!Per 0Bo", uniLogin);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
        "Id cannot be less than 0")]
        public void StudentIdError()
        {
            Student student = new Student(-1, name, uniLogin);
        }
    }
}