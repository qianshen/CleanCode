using System;

namespace CleanCode.Chapter6.LoD
{
	public class Driver : ISkill
	{
		public Driver ()
		{
		}

		public void DriveCar(Car car)
		{
			car.Start();
		}
	}
}

