using System;

namespace NaughtyAttributes
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class HideIfAttribute : ShowIfAttribute
	{
		public HideIfAttribute(string condition)
			: base(condition)
		{
			base.Reversed = true;
		}

		public HideIfAttribute(ConditionOperator conditionOperator, params string[] conditions)
			: base(conditionOperator, conditions)
		{
			base.Reversed = true;
		}
	}
}
