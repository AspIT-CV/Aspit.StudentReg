using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aspit.StudentReg.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspit.StudentReg.DataAccess.Tests
{
    [TestClass()]
    public class AttendanceRegistrationsRepositoryTests
    {
        [TestMethod]
        public void Initialization()
        {
            AttendanceRegistrationsRepository repository = new AttendanceRegistrationsRepository(RepositoryBase.RetrieveConnectionString());
        }
    }
}