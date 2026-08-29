using System.Collections.Generic;
using UnityEngine;

namespace Battlehub.RTCommon
{
	public interface IRuntimeObjects
	{
		event ObjectEvent Awaked;

		event ObjectEvent Started;

		event ObjectEvent Enabled;

		event ObjectEvent Disabled;

		event ObjectEvent Destroying;

		event ObjectEvent Destroyed;

		event ObjectEvent MarkAsDestroyedChanging;

		event ObjectEvent MarkAsDestroyedChanged;

		event ObjectEvent TransformChanged;

		event ObjectEvent NameChanged;

		event ObjectParentChangedEvent ParentChanged;

		event ObjectEvent<Component> ComponentAdded;

		event ObjectEvent<Component> ComponentDestroyed;

		event ObjectEvent<Component, bool> ReloadComponentEditor;

		IEnumerable<ExposeToEditor> Get(bool rootsOnly, bool useCache = true);
	}
}
