using System;
using System.Collections.Generic;

namespace CleanCode.Chapter6.LoD
{
	public class Employee
	{
		Dictionary<Type, ISkill> _skillSet = new Dictionary<Type, ISkill>();

		public Employee (
			string name,
			params ISkill[] skills
		)
		{
			Name = name;
			if (skills != null) {
				foreach(var skill in skills)
				{
					_skillSet[skill.GetType()] = skill;
				}
			}
		}

		public T GetSkill<T>() where T : class, ISkill
		{
			var skill = GetSkill(typeof(T));

            return skill as T;
		}

		public ISkill GetSkill(Type type)
		{
			ISkill skill;

			if (!_skillSet.TryGetValue(type, out skill))
            {
                skill = null;
            }
			return skill;
		}

		public string Name
		{
			get;
			private set;
		}
	}
}

