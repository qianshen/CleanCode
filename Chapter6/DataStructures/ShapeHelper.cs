using System;

namespace CleanCode.Chapter6.DataStructures
{
	public static class ShapeHelper
	{
		public static double GetArea (object shape)
		{
			if (shape.GetType () == typeof(Rectangle)) {
				return GetRectangleArea((Rectangle)shape);
			} else if (shape.GetType () == typeof(Square)) {
				return GetSquareArea((Square)shape);
			} else if (shape.GetType () == typeof(Triangle)) {
				return GetTriangleArea((Triangle)shape);
			}
		}

		private static double GetRectangleArea(Rectangle rect)
		{
			return rect.X * rect.Y;
		}

		private static double GetSquareArea(Square sqr)
		{
			return sqr.Width * sqr.Width;
		}

		private static double GetTriangleArea(Triangle tri)
		{
			return tri.X * tri.Y / 2.0;
		}
	}
}

