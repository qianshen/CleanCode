using System;
using NUnit.Framework;

namespace CleanCode.Chapter6.LoD.Tests
{
	[TestFixture()]
	public class EmployeeTest
	{
		[Test()]
		public void NullSkill()
		{
			var emp = new Employee("");

            var driver = emp.GetSkill<Driver>();

            Assert.IsNull(driver);
		}

		[Test]
		public void DriverSkill()
		{
            var emp = new Employee(
				"",
				new Driver());

            var driver = emp.GetSkill<Driver>();

            Assert.IsNotNull(driver);

            var mechanician = emp.GetSkill<Mechanician>();

            Assert.IsNull(mechanician);
		}

        [Test]
        public void TwoSkills()
        {
            var emp = new Employee(
				"",
				new Driver(), 
				new Mechanician()
				);

            var driver = emp.GetSkill<Driver>();

            Assert.IsNotNull(driver);

            var mechanician = emp.GetSkill<Mechanician>();

            Assert.IsNotNull(mechanician);
        }
	}
}

