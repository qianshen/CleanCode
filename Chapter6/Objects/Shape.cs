using System;

namespace CleanCode.Chapter6.Objects
{
	public class Shape
	{
		public Shape ()
		{
		}

		public virtual double GetArea()
		{
			throw new NotImplementedException();
		}
	}
}

