using System;

namespace UnitTestDrilled
{
	public class SampleWorker3
	{
		public SampleWorker3 ()
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

		internal bool IsWeekend (DateTime date)
		{
			return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
		}

		internal bool ShouldRest(DateTime date)
		{
			return date.Hour < 9 || date.Hour > 17;
		}

		internal bool ShouldLunch (DateTime date)
		{
			return date.Hour >= 12 && date.Hour < 14;
		}

		internal bool ShouldNotWork (DateTime date)
		{
			return date.Minute % 3 == 0;
		}

	    internal void Rest()
		{
		}

		internal void Lunch()
		{
		}

		internal void DoWork()
		{
		}
	}
}

