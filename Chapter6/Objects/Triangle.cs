using System;

namespace CleanCode.Chapter6.Objects
{
	public class Triangle : Shape
	{
		int _x;
		int _y;

		public Triangle (int x, int y)
		{
			_x = x;
			_y = y;
		}

		public override double GetArea ()
		{
			return _x * _y / 2.0;
		}
	}
}

