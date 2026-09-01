using System;
using System.Collections.Generic;

namespace FluffyUnderware.DevTools
{
	internal class LoopState<T>
	{
		public short StartIndex;

		public short EndIndex;

		public List<T> Items;

		public Action<T> Action;
	}
}
