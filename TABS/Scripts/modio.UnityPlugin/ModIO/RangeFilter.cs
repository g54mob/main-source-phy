using System;

namespace ModIO
{
	[Obsolete("Combine a MinimumFilter and MaximumFilter instead.")]
	public class RangeFilter<T> : IRequestFieldFilter<T>, IRequestFieldFilter where T : IComparable<T>
	{
		public T min;

		public bool isMinInclusive;

		public T max;

		public bool isMaxInclusive;

		public FieldFilterMethod filterMethod
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		object IRequestFieldFilter.filterValue
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		T IRequestFieldFilter<T>.filterValue
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public string GenerateFilterString(string fieldName)
		{
			return string.Concat(fieldName, isMinInclusive ? "-min=" : "-gt=", min, "&", fieldName, isMaxInclusive ? "-max=" : "-st=", max);
		}
	}
}
