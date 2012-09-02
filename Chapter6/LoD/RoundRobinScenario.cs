using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CleanCode.Chapter6.LoD
{
	public class RoundRobinScenario : Scenario<object>
	{
		Queue<Employee> _taskforce = new Queue<Employee>();
		Car _workingCar;

		public RoundRobinScenario (Car workingCar)
		{
			_workingCar = workingCar;
		}

		protected override void DoTask (WorkState<object> state)
		{
			Employee emp = null;

			emp = GetFreeEmployee ();

			if (emp == null) {
				throw new InvalidOperationException();
			}

			Console.WriteLine(string.Format("{0} is selected out for task type {1}", emp.Name, state.TaskType.Name));

			var skill = emp.GetSkill (state.TaskType);

			if (skill != null) {
				Console.WriteLine(string.Format("{0} do task of {1}", emp.Name, skill.GetType().Name));
				DoSkill(skill, state.Car);
			}
			WaitTask(emp);
		}

		private void DoSkill (ISkill skill, Car car)
		{
			try {
				if (skill.GetType ().IsAssignableFrom (typeof(Driver))) {
					((Driver)skill).DriveCar (car);
				} else if (skill.GetType ().IsAssignableFrom (typeof(Mechanician))) {
					((Mechanician)skill).RepairCar (car);
				}
			} catch (Exception ex) {
				Console.WriteLine(string.Format("ex {0} of task {1}", ex.Message, skill.GetType().Name));
			}
		}

		protected override Car GetCar ()
		{
			return _workingCar;
		}

		protected override Type[] GetTaskTypes (Car car)
		{
			if (car.IsBroken) {
				return new Type[]{typeof(Mechanician)};
			} else {
				return new Type[]{typeof(Driver)};
			}
		}

		protected override void WaitTask (Employee emp)
		{
			if (_emps.ContainsKey (emp)) {
				_taskforce.Enqueue (emp);
			}
		}

		protected override Employee GetFreeEmployee ()
		{
			if (_taskforce.Count > 0) {
				var emp = _taskforce.Dequeue ();

				if (_emps.ContainsKey (emp)) {
					return emp;
				} else {
					return GetFreeEmployee ();
				}
			} else {
				return null;
			}
		}
	}
}

