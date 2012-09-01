using System;
using NUnit.Framework;

namespace CleanCode.Chapter6.LoD.Tests
{
	[TestFixture()]
	public class EmpolyeeTest
	{
		[Test()]
		public void NullSkill()
		{
			var emp = new Empolyee();

            var driver = emp.GetSkill<Driver>();

            Assert.IsNull(driver);
		}

		[Test]
		public void DriverSkill()
		{
            var emp = new Empolyee(new Driver());

            var driver = emp.GetSkill<Driver>();

            Assert.IsNotNull(driver);

            var mechanician = emp.GetSkill<Mechanician>();

            Assert.IsNull(mechanician);
		}

        [Test]
        public void TwoSkills()
        {
            var emp = new Empolyee(new Driver(), new Mechanician());

            var driver = emp.GetSkill<Driver>();

            Assert.IsNotNull(driver);

            var mechanician = emp.GetSkill<Mechanician>();

            Assert.IsNotNull(mechanician);
        }
	}
}

