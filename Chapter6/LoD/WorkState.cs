using System;

namespace CleanCode.Chapter6.LoD
{

	public class WorkState<T>
	{
		public WorkState (
			Car car,
			Type taskType
			)
		{
			Car = car;
			TaskType = taskType;
		}

		public Car Car
		{
			get;
			private set;
		}

		public Type TaskType
		{
			get;
			private set;
		}

		public T StateObject
		{
			get;
			set;
		}
	}
}

