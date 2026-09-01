using System.Text;

namespace ModIO
{
	public abstract class ArrayFieldFilterBase<T> : AFieldFilterBase<T[]>
	{
		public T[] filterArray
		{
			get
			{
				return filterValue;
			}
			set
			{
				base.filterValue = value;
			}
		}

		public ArrayFieldFilterBase(FieldFilterMethod filterMethod, string apiStringOperator)
			: base(filterMethod, apiStringOperator)
		{
		}

		public override string GenerateFilterString(string fieldName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (filterArray.Length > 0)
			{
				T[] array = filterArray;
				for (int i = 0; i < array.Length; i++)
				{
					T val = array[i];
					if (val != null)
					{
						stringBuilder.Append(val.ToString() + ",");
					}
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Length--;
				}
			}
			return fieldName + apiStringOperator + stringBuilder.ToString();
		}
	}
}
