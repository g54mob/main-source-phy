using System.Collections;
using UnityEngine;

namespace Battlehub.RTCommon
{
	public interface IRuntimeSelectionInternal : IRuntimeSelection, IEnumerable
	{
		Object INTERNAL_activeObjectProperty { get; set; }

		Object[] INTERNAL_objectsProperty { get; set; }
	}
}
