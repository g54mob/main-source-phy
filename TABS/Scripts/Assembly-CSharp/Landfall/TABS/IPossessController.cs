using System;

namespace Landfall.TABS
{
	public interface IPossessController
	{
		bool IsPossessing { get; }

		event Action<bool> OnPossessUpdate;
	}
}
