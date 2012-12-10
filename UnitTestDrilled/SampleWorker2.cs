using System;

namespace UnitTestDrilled
{
	public class SampleWorker2
	{
		public SampleWorker2 ()
		{
		}

		public void Work (DateTime date)
		{

			if (IsWeekend(date)) {
				Rest();
			}

			if (ShouldRest(date)) {
				Rest ();
			}

			if (ShouldLunch(date)) {
				Lunch ();
			}

			if (ShouldNotWork(date)) {
				Rest ();
			} else {
				DoWork();
			}
		}

		internal virtual bool IsWeekend (DateTime date)
		{
			return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
		}

		internal virtual bool ShouldRest(DateTime date)
		{
			return date.Hour < 9 || date.Hour > 17;
		}

		internal virtual bool ShouldLunch (DateTime date)
		{
			return date.Hour >= 12 && date.Hour < 14;
		}

		internal virtual bool ShouldNotWork (DateTime date)
		{
			return date.Minute % 3 == 0;
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









		// 3 + 3 + 3 + 2 + 2 * 2 * 2 * 2 = 27


