using System;

namespace UnitTestDrilled
{
	public class SampleWorker
	{
		public SampleWorker ()
		{
		}

		public void Work (DateTime date)
		{

			if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) {
				Rest();
			}
	
			if (date.Hour < 9 || date.Hour > 17) {
				Rest ();
			}

			if (date.Hour >= 12 && date.Hour < 14) {
				Lunch ();
			}
	
			if (date.Minute % 3 == 0) {
				Rest ();
			} else {
				DoWork();
			}

		}

	    void Rest()
		{
		}

		void Lunch()
		{
		}

		void DoWork()
		{
		}
	}
}











		// 3 * 3 * 3 * 2 = 54
