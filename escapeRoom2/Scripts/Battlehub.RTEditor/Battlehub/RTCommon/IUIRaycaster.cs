using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battlehub.RTCommon
{
	public interface IUIRaycaster
	{
		Camera eventCamera { get; }

		void Raycast(List<RaycastResult> results);

		void Raycast(PointerEventData eventData, List<RaycastResult> results);

		void AddRaycaster(BaseRaycaster raycaster);

		void RemoveRaycaster(BaseRaycaster raycaster);
	}
}
