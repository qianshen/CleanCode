using System;

namespace UnitTestDrilled
{
	public class SampleWorker3
	{
		public SampleWorker3 ()
		{
		}

		public void Work (DateTime date1, DateTime date2, DateTime date3, DateTime date4)
		{

			if (IsWeekend()) {
				Rest();
			}

			if (ShouldRest()) {
				Rest ();
			}

			if (ShouldLunch()) {
				Lunch ();
			}

			if (ShouldNotWork()) {
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

