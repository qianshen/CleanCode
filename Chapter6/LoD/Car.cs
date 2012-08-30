using System;
using System.Collections.Generic;

namespace CleanCode.Chapter6.LoD
{
	public class Car
	{
		Engine _engine;
		List<Tyre> _tyres = new List<Tyre>();

		public Car (
			Engine engine,
			Tyre[] tyres
		)
		{
			_engine = engine;
			foreach (var item in tyres) {
				AddTyre(item);
			}
		}

		public void AddTyre (Tyre tyre)
		{
			if (_tyres.Count >= 4) {
				throw new InvalidOperationException();
			}
			_tyres.Add(tyre);
		}

		public void UnloadBrokenTyres ()
		{
			for (int i = 0; i < _tyres.Count;) {
				if (_tyres[i].IsBroken) {
					_tyres.RemoveAt (i);
				} else {
					i++;
				}
			}
		}

		public void Start ()
		{
			_engine.Start ();
			foreach (var item in _tyres) {
				item.Run();
			}
		}

		private bool CheckIsBroken ()
		{
			var isBroken = _engine.IsBroken;

			if (!isBroken) {
				foreach (var item in _tyres) {
					isBroken &= item.IsBroken;
					if (isBroken) {
						break;
					}
				}
			}
			return isBroken;
		}

		public bool IsBroken {
			get {
				return CheckIsBroken();
			}
		}
	}
}

