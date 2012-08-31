using System;

namespace CleanCode.Chapter6.LoD
{
	public class Mechanician : ISkill
	{
		public Mechanician ()
		{
		}

		public void RepairCar (Car car)
		{
			if (car.IsBroken) {
				RepairEngine(car);
				RepairTyre(car);
			}
		}

		private void RepairEngine (Car car)
		{
			if (car.IsEngineBroken) {
				var engine = car.UnloadEngine();

				engine.Reset();
				car.LoadEngine(engine);
			}
		}

		private void RepairTyre (Car car)
		{
			if (car.IsTyreBroken) {
				car.UnloadBrokenTyres();
				for(int i = car.TyreCount; i < Car.StandardTyreCount; i++)
				{
					var tyre = new Tyre();

					car.AddTyre(tyre);
				}
			}
		}
	}
}

