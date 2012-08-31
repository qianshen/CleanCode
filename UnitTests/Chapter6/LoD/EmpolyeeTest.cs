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

//			var driver = emp.GetSkill<Driver>();

//			Assert.IsNull(driver);
		}

		[Test]
		public void DriverSkill()
		{
		}
	}
}

