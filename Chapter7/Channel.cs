using System;
using System.Threading;

namespace CleanCode.Chapter7
{
	public class Channel
	{

		public Channel (
			string name,
			int availability, 
			int severity
			)
		{
			Name = name;
			Availability = availability;
			Severity = severity;
		}

		public void Send ()
		{
			var chance = (new Random ()).Next (1, 100);

			Console.WriteLine(string.Format("{0}: 1 -{3}- {1} -{4}- {2} -{5}", chance, Availability, Severity, 
			                                chance > 1 && chance <= Availability ? "@" : string.Empty,
			                                chance > Availability && chance <= Severity ? "@" : string.Empty,
			                                chance > Severity ? "@" : string.Empty
			                                ));
			Thread.Sleep(chance);
			if (chance > Severity) {
				throw new FatalException ();
			} else if (chance > Availability) {
				throw new GeneralException();
			}
		}

		public string Name
		{
			get;
			set;
		}

		public int Availability
		{
			get;
			private set;
		}

		public int Severity
		{
			get;
			private set;
		}
	}
}

