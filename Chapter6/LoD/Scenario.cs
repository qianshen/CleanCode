using System;
using System.Collections.Generic;
using System.Text;

namespace CleanCode.Chapter6.LoD
{
	public abstract class Scenario<State> where State : class, new()
    {
		protected Dictionary<Employee, Employee> _emps = new Dictionary<Employee, Employee>();

		public Scenario()
		{
		}

		public virtual void Work ()
		{
			Car car = GetCar ();

			var taskTypes = GetTaskTypes (car);

			State stateObject = new State();

			foreach (var taskType in taskTypes) {

				WorkState<State> state = new WorkState<State>(car, taskType);

				state.StateObject = stateObject;

				DoTask(state);

				stateObject = state.StateObject;
			}

		}

		protected abstract Car GetCar ();

		protected abstract Type[] GetTaskTypes (Car car);

		protected abstract void DoTask (WorkState<State> state);

		public void AddEmployee (Employee emp)
		{
			if (!_emps.ContainsKey(emp)) {
				_emps.Add (emp, emp);
				WaitTask(emp);
			}
		}

		public void RemoveEmployee (Employee emp)
		{
			if (_emps.ContainsKey (emp)) {
				_emps.Remove (emp);
			}
		}

		protected abstract void WaitTask (Employee emp);

		protected abstract Employee GetFreeEmployee ();
    }
}
