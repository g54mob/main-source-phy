using System;

namespace NaughtyAttributes
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class DisableIfAttribute : EnableIfAttribute
	{
		public DisableIfAttribute(string condition)
			: base(condition)
		{
			base.Reversed = true;
		}

		public DisableIfAttribute(ConditionOperator conditionOperator, params string[] conditions)
			: base(conditionOperator, conditions)
		{
			base.Reversed = true;
		}
	}
}
