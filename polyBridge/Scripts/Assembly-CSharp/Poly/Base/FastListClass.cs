namespace Poly.Base
{
	public class FastListClass<T> : FastList<T> where T : new()
	{
		public new ref T ExpandOne()
		{
			ref T reference = ref base.ExpandOne();
			if (reference == null)
			{
				reference = new T();
			}
			return ref reference;
		}

		public FastListClass()
			: base((short)16)
		{
		}
	}
}
