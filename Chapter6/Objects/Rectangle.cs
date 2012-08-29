using System;

namespace CleanCode.Chapter6.Objects
{
	public class Rectangle : Shape
	{
		int _x;
		int _y;

		public Rectangle (int x, int y)
		{
			_x = x;
			_y = y;
		}

		public override double GetArea ()
		{
			return _x * _y;
		}
	}
}

