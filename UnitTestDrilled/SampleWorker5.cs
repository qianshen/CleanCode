using System;
using System.Collections.Generic;

namespace UnitTestDrilled
{
	public class SampleWorker5
	{
		Dictionary<Type, ILife> _lifeStyles = new Dictionary<Type, ILife>();

		internal SampleWorker5 (
			params ILife[] lifeStyles
		)
		{
			foreach (var lifeStyle in lifeStyles) {
				_lifeStyles.Add(lifeStyle.GetType(), lifeStyle);
			}
		}

		public void Work (DateTime date)
		{
			foreach (var lifeStyle in _lifeStyles.Values) {
				lifeStyle.GoLife(date);
			}
		}

	}

	interface ILife
	{
		bool GoLife(DateTime date);
	}

	static class DateTimeExtension
	{
		public static bool IsWeekend (this DateTime date)
		{
			return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
		}

		public static bool ShouldRest(this DateTime date)
		{
			return date.Hour < 9 || date.Hour > 17;
		}

		public static bool ShouldLunch (this DateTime date)
		{
			return date.Hour >= 12 && date.Hour < 14;
		}

		public static bool ShouldNotWork (this DateTime date)
		{
			return date.Minute % 3 == 0;
		}
	}

	class WeekendLife : ILife
	{
		public bool GoLife (DateTime date)
		{
			if (date.IsWeekend()) {
				Rest ();
				return true;
			} else {
				return false;
			}
		}

		internal void Rest()
		{
		}
	}

	class ResetLife : ILife
	{
		public bool GoLife (DateTime date)
		{
			if (date.ShouldRest ()) {
				Rest ();
				return true;
			} else {
				return false;
			}
		}

		internal void Rest()
		{
		}
	}

	class LunchLife : ILife
	{
		public bool GoLife (DateTime date)
		{
			if (date.ShouldLunch()) {
				Lunch ();
				return true;
			} else {
				return false;
			}
		}

		internal void Lunch()
		{
		}
	}

	class WorkLife : ILife
	{
		public bool GoLife (DateTime date)
		{
			if (date.ShouldNotWork()) {
				Rest ();
				return false;
			} else {
				DoWork();
				return true;
			}
		}

		internal void Rest()
		{
		}

		internal void DoWork()
		{
		}
	}
}










//3 + 3 + 3 + 2 + 2 + 2 + 2 + 2 + 1 = 20
