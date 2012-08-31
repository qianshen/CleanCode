using System;
using NUnit.Framework;
using Objects = CleanCode.Chapter6.Objects;
using DataStructures = CleanCode.Chapter6.DataStructures;

namespace UnitTests
{
	[TestFixture()]
	public class ObjectsTest
	{
		[Test()]
		public void ObjectsGetArea ()
		{
			Objects.Rectangle rect = new Objects.Rectangle(3, 4);

			Assert.AreEqual(12, rect.GetArea());

			Objects.Square sqr = new Objects.Square(5);

			Assert.AreEqual(25, sqr.GetArea());

			Objects.Triangle tri = new Objects.Triangle(3, 4);

			Assert.AreEqual(6, tri.GetArea());
		}

		[Test()]
		public void DataStructuresGetArea()
		{
			DataStructures.Rectangle rect = new DataStructures.Rectangle
			{
				X = 3,
				Y = 4
			};

			Assert.AreEqual(12, DataStructures.ShapeHelper.GetArea(rect));

			DataStructures.Square sqr = new DataStructures.Square
			{
				Width = 5
			};

			Assert.AreEqual(25, DataStructures.ShapeHelper.GetArea(sqr));

			DataStructures.Triangle tri = new DataStructures.Triangle
			{
				X = 3,
				Y = 4
			};

			Assert.AreEqual(6, DataStructures.ShapeHelper.GetArea(tri));
		}

	}
}

