using System.Collections;
using System.Collections.Generic;

namespace SRDebugger
{
	public interface IReadOnlyList<T> : IEnumerable, IEnumerable<T>
	{
		int Count { get; }

		T this[int index] { get; }
	}
}
