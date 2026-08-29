using System;
using UnityEngine.Events;

namespace RuntimeSceneGizmo
{
	[Serializable]
	public class ComponentClickedEvent : UnityEvent<GizmoComponent>
	{
	}
}
