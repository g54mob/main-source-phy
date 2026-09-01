using System.Collections.Generic;

namespace mattmc3.dotmore.Collections.Generic
{
	public class EnumerableEqualityComparer<T> : EqualityComparer<IEnumerable<T>>
	{
		private IEqualityComparer<T> _cmp;

		public EnumerableEqualityComparer()
		{
		}

		public EnumerableEqualityComparer(IEqualityComparer<T> cmp)
		{
			_cmp = cmp;
		}

		public override bool Equals(IEnumerable<T> x, IEnumerable<T> y)
		{
			IEnumerator<T> enumerator = x.GetEnumerator();
			IEnumerator<T> enumerator2 = y.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator2.MoveNext())
				{
					return false;
				}
				if (_cmp != null)
				{
					if (!_cmp.Equals(enumerator.Current, enumerator2.Current))
					{
						return false;
					}
				}
				else if (!enumerator.Current.Equals(enumerator2.Current))
				{
					return false;
				}
			}
			return !enumerator2.MoveNext();
		}

		public override int GetHashCode(IEnumerable<T> obj)
		{
			int num = 0;
			IEnumerator<T> enumerator = obj.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (_cmp != null)
				{
					int num2 = ((enumerator.Current != null) ? _cmp.GetHashCode(enumerator.Current) : 0);
					num ^= num2;
				}
				else
				{
					num ^= enumerator.Current.GetHashCode();
				}
			}
			return num;
		}
	}
}
