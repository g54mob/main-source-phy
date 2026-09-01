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

		public FieldFilterMethod filterMethod
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public string GenerateFilterString(string fieldName)
		{
			return string.Concat(fieldName, (!isMinInclusive) ? "-gt=" : "-min=", min, "&", fieldName, (!isMaxInclusive) ? "-st=" : "-max=", max);
		}
	}
}
