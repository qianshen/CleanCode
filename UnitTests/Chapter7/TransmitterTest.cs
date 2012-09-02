using System;
using NUnit.Framework;
using CleanCode.Chapter7;

namespace CleanCode.Chapter7.Tests
{
	[TestFixture()]
	public class TransmitterTest
	{
		[Test()]
		public void Send ()
		{
			var transmitter = new Transmitter (
				3,
				new Channel ("cpa", 40, 70),
				new Channel ("cea", 30, 50),
				new Channel ("cna", 35, 60)
			);

			for (int i = 0; i < 10; i++) {
				Console.WriteLine("----------Sending {0}th message----------", i + 1);
				transmitter.Send ();
			}
		}
	}
}

