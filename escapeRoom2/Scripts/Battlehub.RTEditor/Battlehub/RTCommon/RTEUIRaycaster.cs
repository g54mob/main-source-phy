using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battlehub.RTCommon
{
	public class RTEUIRaycaster : MonoBehaviour, IUIRaycaster
	{
		[SerializeField]
		private BaseRaycaster[] m_raycasters;

		private IInput m_input;

		private IRTE m_editor;

		public Camera eventCamera => m_raycasters[0].eventCamera;

		private void Awake()
		{
			m_editor = IOC.Resolve<IRTE>();
			m_input = m_editor.Input;
			if (m_raycasters == null || m_raycasters.Length == 0 || m_raycasters[0] == null)
			{
				BaseRaycaster baseRaycaster = base.gameObject.GetComponent<BaseRaycaster>();
				if (baseRaycaster == null)
				{
					GraphicRaycaster graphicRaycaster = base.gameObject.AddComponent<GraphicRaycaster>();
					graphicRaycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;
					baseRaycaster = graphicRaycaster;
				}
				m_raycasters = new BaseRaycaster[1] { baseRaycaster };
			}
		}

		public void Raycast(List<RaycastResult> results)
		{
			if (m_editor.EventSystem == null)
			{
				return;
			}
			PointerEventData pointerEventData = new PointerEventData(m_editor.EventSystem);
			pointerEventData.position = m_input.GetPointerXY(0);
			for (int i = 0; i < m_raycasters.Length; i++)
			{
				BaseRaycaster baseRaycaster = m_raycasters[i];
				if (baseRaycaster != null)
				{
					baseRaycaster.Raycast(pointerEventData, results);
				}
			}
		}

		public void Raycast(PointerEventData eventData, List<RaycastResult> results)
		{
			eventData.position = m_input.GetPointerXY(0);
			for (int i = 0; i < m_raycasters.Length; i++)
			{
				BaseRaycaster baseRaycaster = m_raycasters[i];
				if (baseRaycaster != null)
				{
					baseRaycaster.Raycast(eventData, results);
				}
			}
		}

		public void AddRaycaster(BaseRaycaster raycaster)
		{
			List<BaseRaycaster> list = m_raycasters.ToList();
			list.Add(raycaster);
			m_raycasters = list.ToArray();
		}

		public void RemoveRaycaster(BaseRaycaster raycaster)
		{
			List<BaseRaycaster> list = m_raycasters.ToList();
			list.Remove(raycaster);
			m_raycasters = list.ToArray();
		}
	}
}
