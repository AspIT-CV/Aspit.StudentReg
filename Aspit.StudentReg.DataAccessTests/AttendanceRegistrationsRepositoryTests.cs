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
    }
}