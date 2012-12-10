using System;

namespace UnitTestDrilled
{
	public class SampleWorker4
	{
		public SampleWorker4 ()
		{
		}

		public void Work (DateTime date1, DateTime date2, DateTime date3, DateTime date4)
		{

			GoWeekend(date1);

			GoRest(date2);

			GoLunch(date3);

			GoToWork(date4);
		}

		internal bool GoWeekend (DateTime date)
		{
			if (IsWeekend (date)) {
				Rest ();
				return true;
			} else {
				return false;
			}
		}

		internal bool GoRest (DateTime date)
		{
			if (ShouldRest (date)) {
				Rest ();
				return true;
			} else {
				return false;
			}

		}

		internal bool GoLunch (DateTime date)
		{
			if (ShouldLunch (date)) {
				Lunch ();
				return true;
			} else {
				return false;
			}
		}

		internal bool GoToWork(DateTime date)
		{
			if (ShouldNotWork(date)) {
				Rest ();
				return false;
			} else {
				DoWork();
				return true;
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










3 + 3 + 3 + 2 + 2 + 2 + 2 + 2 + 1 = 20
