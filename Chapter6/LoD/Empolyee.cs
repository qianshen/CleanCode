using System;
using System.Collections.Generic;

namespace CleanCode.Chapter6.LoD
{
	public class Empolyee
	{
		Dictionary<Type, ISkill> _skillSet = new Dictionary<Type, ISkill>();

		public Empolyee (
			params ISkill[] skills
		)
		{
			if (skills != null) {
				foreach(var skill in skills)
				{
					_skillSet[skill.GetType()] = skill;
				}
			}
		}

		public T GetSkill<T>() where T : class, ISkill
		{
            ISkill skill;

            if (!_skillSet.TryGetValue(typeof(T), out skill))
            {
                skill = null;
            }
            return skill as T;
		}
	}
}

