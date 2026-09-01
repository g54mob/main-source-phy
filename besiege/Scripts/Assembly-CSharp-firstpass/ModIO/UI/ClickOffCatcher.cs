using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ModIO.UI
{
	[RequireComponent(typeof(RectTransform))]
	public class ClickOffCatcher : MonoBehaviour
	{
		public bool constructOnEnable = true;

		public UnityEvent clickedOff;

		private RectTransform m_blocker;

		private bool m_hasCanvas;

		private int m_oldCanvasSort = -1;

		private bool m_hasRaycaster;

		private void OnEnable()
		{
			if (constructOnEnable)
			{
				m_blocker = InstantiateBlocker(base.transform as RectTransform);
				m_blocker.GetComponent<Button>().onClick.AddListener(OnButtonClick);
				Canvas canvas = GetComponent<Canvas>();
				m_hasCanvas = canvas != null;
				if (m_hasCanvas)
				{
					m_oldCanvasSort = canvas.sortingOrder;
				}
				else
				{
					canvas = base.gameObject.AddComponent<Canvas>();
					canvas.overridePixelPerfect = false;
					canvas.overrideSorting = true;
				}
				canvas.sortingOrder = 30000;
				GraphicRaycaster component = GetComponent<GraphicRaycaster>();
				m_hasRaycaster = component != null;
				if (!m_hasRaycaster)
				{
					component = base.gameObject.AddComponent<GraphicRaycaster>();
					component.ignoreReversedGraphics = true;
					component.blockingObjects = GraphicRaycaster.BlockingObjects.None;
				}
			}
		}

		private void OnDisable()
		{
			if (!constructOnEnable || !(m_blocker != null))
			{
				return;
			}
			Object.Destroy(m_blocker.gameObject);
			if (!m_hasRaycaster)
			{
				GraphicRaycaster component = base.gameObject.GetComponent<GraphicRaycaster>();
				if (component != null)
				{
					Object.Destroy(component);
				}
			}
			Canvas component2 = base.gameObject.GetComponent<Canvas>();
			if (component2 != null)
			{
				if (m_hasCanvas)
				{
					component2.sortingOrder = m_oldCanvasSort;
				}
				else
				{
					Object.Destroy(component2);
				}
			}
		}

		private void OnButtonClick()
		{
			if (clickedOff != null)
			{
				clickedOff.Invoke();
			}
		}

		public static RectTransform InstantiateBlocker(RectTransform creator)
		{
			Canvas canvas = creator.gameObject.GetComponentInParent<Canvas>();
			if (canvas == null)
			{
				Debug.LogWarning("[mod.io] Unable to instantiate as no parent canvas was found for the creator object.");
				return null;
			}
			if (canvas != null)
			{
				canvas = canvas.rootCanvas;
			}
			GameObject gameObject = new GameObject("Blocker", typeof(RectTransform));
			gameObject.hideFlags = HideFlags.DontSave;
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.SetParent(canvas.transform);
			component.localPosition = Vector3.zero;
			component.localRotation = Quaternion.identity;
			component.localScale = Vector3.one;
			component.anchorMin = Vector2.zero;
			component.anchorMax = Vector2.one;
			Vector2 offsetMin = (component.offsetMax = Vector2.zero);
			component.offsetMin = offsetMin;
			Canvas canvas2 = gameObject.AddComponent<Canvas>();
			canvas2.overridePixelPerfect = false;
			canvas2.overrideSorting = true;
			canvas2.sortingOrder = 29999;
			GraphicRaycaster graphicRaycaster = gameObject.AddComponent<GraphicRaycaster>();
			graphicRaycaster.ignoreReversedGraphics = true;
			graphicRaycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;
			gameObject.AddComponent<CanvasRenderer>();
			gameObject.AddComponent<Touchable>();
			Button button = gameObject.AddComponent<Button>();
			button.navigation = new Navigation
			{
				mode = Navigation.Mode.None
			};
			return component;
		}
	}
}
