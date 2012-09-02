using System;
using NUnit.Framework;
using System.Collections;
using CleanCode.Chapter6.LoD;
using System.Threading;

namespace CleanCode.Chapter6.LoD.Tests
{
	[TestFixture()]
	public class LoDTest
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

		[Test()]
		public void RoundRobinScenario ()
		{
			var tom = new Employee (
				"tom",
				new Driver ()
			);

			var jerry = new Employee (
				"jerry",
				new Driver (),
				new Mechanician ()
			);

			var jack = new Employee (
				"jack",
				new Driver ()
			);

			var engine = CreateEngine ();
			var tyres = CreateTyres (Car.StandardTyreCount);

			var car = new Car (engine, tyres);

			RoundRobinScenario scene = new RoundRobinScenario (car);

			scene.AddEmployee (tom);
			scene.AddEmployee (jerry);
			scene.AddEmployee (jack);

			for (int i = 0; i < 30; i++) {
				scene.Work ();
			}
		}
	}
}

