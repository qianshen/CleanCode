using System;
using NUnit.Framework;
using CleanCode.Chapter6.LoD;

namespace CleanCode.Chapter6.LoD.Tests
{
	[TestFixture()]
	public class CarTest
	{
		Engine CreateEngine()
		{
			return new Engine();
		}

		Tyre[] CreateTyres (int number)
		{
			var tyres = new Tyre[number];

			for (int i = 0; i < tyres.Length; i++) {
				tyres[i] = new Tyre();
			}
			return tyres;
		}

		[Test]
		public void BuildCar()
		{
			var engine = CreateEngine();
			var tyres = CreateTyres(Car.StandardTyreCount);

			var car = new Car(engine, tyres);
			Assert.Pass("Build Car succ");
		}

		[ExpectedException(typeof(InvalidOperationException))]
		[Test]
		public void BuildCarTooMuchTyres()
		{
			var engine = CreateEngine();
			var tyres = CreateTyres(Car.StandardTyreCount + 1);

			var car = new Car(engine, tyres);
			Assert.Fail();
		}

		[Test]
		public void AddTyre()
		{
			var engine = CreateEngine();

			var car = new Car(engine, null);
			Assert.AreEqual(0, car.TyreCount);

			car.AddTyre(new Tyre());
			Assert.AreEqual(1, car.TyreCount);
		}

		[ExpectedException(typeof(InvalidOperationException))]
		[Test()]
		public void AddTyreTooMuch()
		{
			var engine = CreateEngine();
			var tyres = CreateTyres(Car.StandardTyreCount);

			var car = new Car(engine, tyres);

			car.AddTyre(new Tyre());
			Assert.Fail();
		}
	}
}

