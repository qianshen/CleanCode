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
			LoadEngine (engine);
			if (tyres != null) {
				foreach (var item in tyres) {
					AddTyre (item);
				}
			}
		}

		public void AddTyre (Tyre tyre)
		{
			if (TyreCount >= StandardTyreCount) {
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

		public Engine UnloadEngine ()
		{
			var engine = _engine;

			_engine = null;
			return engine;
		}

		public void LoadEngine (Engine engine)
		{
			if (_engine != null) {
				throw new InvalidOperationException();
			}
			_engine = engine;
		}

		public void Start ()
		{
			_engine.Start ();
			if (_tyres.Count < StandardTyreCount) {
				throw new InvalidOperationException();
			}
			foreach (var item in _tyres) {
				item.Run();
			}
		}

		private bool CheckIsTyreBroken ()
		{
			bool isBroken = false;

			foreach (var item in _tyres) {
				isBroken |= item.IsBroken;
				if (isBroken) {
					break;
				}
			}
			return isBroken;
		}

		private bool CheckIsEngineBroken ()
		{
			return _engine.IsBroken;
		}

		public bool IsTyreBroken {
			get {
				return CheckIsTyreBroken();
			}
		}

		public bool IsEngineBroken {
			get {
				return CheckIsEngineBroken();
			}
		}

		public bool IsBroken {
			get {
				return IsEngineBroken && IsTyreBroken;
			}
		}

		public int TyreCount {
			get {
				return _tyres.Count;
			}
		}

		public static int StandardTyreCount {
			get {
				return 4;
			}
		}
	}
}

