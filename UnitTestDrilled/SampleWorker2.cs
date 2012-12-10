using System;

namespace UnitTestDrilled
{
	public class SampleWorker
	{
		public SampleWorker ()
		{
		}

		public void Work (DateTime date1, DateTime date2, DateTime date3, DateTime date4)
		{

			if (date1.DayOfWeek == DayOfWeek.Saturday || date1.DayOfWeek == DayOfWeek.Sunday) {
				return;
			}

			if (date2.Hour < 9 || date2.Hour > 17) {
				Rest ();
				return;
			}

			if (date3.Hour >= 12 && date3.Hour < 14) {
				Lunch ();
				return;
			}

			if (date4.Minute % 3 == 0) {
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

