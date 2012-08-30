using System;

namespace CleanCode.Chapter6.LoD
{
	public class Tyre
	{
		int _maxAge;

		public Tyre ()
		{
			_maxAge = (new Random()).Next(1, 10);
		}

		public void Run ()
		{
			if (IsBroken) {
				throw new InvalidOperationException();
			}
			_maxAge --;
		}

		public bool IsBroken {
			get {
				return _maxAge <= 0;
			}
		}
	}
}

