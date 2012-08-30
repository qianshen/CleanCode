using System;

namespace CleanCode.Chapter6.LoD
{
	public class Engine
	{
		bool _isBorken = false;

		public Engine ()
		{
		}

		public void Start ()
		{
			_isBorken = (new Random()).Next(1, 100) >= 70;
			if (IsBroken) {
				throw new InvalidOperationException();
			}
		}

		internal void Reset()
		{
			_isBorken = false;
		}

		public bool IsBroken {
			get {
				return _isBorken;
			}
		}
	}
}

