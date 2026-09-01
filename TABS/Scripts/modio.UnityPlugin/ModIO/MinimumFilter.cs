using System;

namespace ModIO
{
	public class MinimumFilter<T> : AFieldFilterBase<T> where T : IComparable<T>
	{
		public bool isInclusive = true;

		public T minimum
		{
			get
			{
				return base.filterValue;
			}
			set
			{
				base.filterValue = value;
			}
		}

		public override FieldFilterMethod filterMethod
		{
			get
			{
				data.filterMethod = (isInclusive ? FieldFilterMethod.Minimum : FieldFilterMethod.GreaterThan);
				return base.filterMethod;
			}
		}

		public MinimumFilter(T filterValue = default(T), bool isInclusive = true)
			: base(FieldFilterMethod.Minimum, "-min=")
		{
			minimum = filterValue;
			this.isInclusive = isInclusive;
		}

		public override string GenerateFilterString(string fieldName)
		{
			apiStringOperator = (isInclusive ? "-min=" : "-gt=");
			return base.GenerateFilterString(fieldName);
		}
	}
}
