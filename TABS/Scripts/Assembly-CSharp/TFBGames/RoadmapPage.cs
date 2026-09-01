using System.Collections.Generic;
using Landfall;
using Landfall.TABS;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TFBGames
{
	public class RoadmapPage : MonoBehaviour
	{
		[SerializeField]
		protected MainMenuUIHandler mainMenuUIHandler;

		[SerializeField]
		[Tooltip("The Roadmap parent.")]
		protected RoadmapHandler roadmapHandler;

		[SerializeField]
		[Tooltip("The text images on this page.")]
		protected RoadmapTextImage[] textImages;

		[SerializeField]
		[Tooltip("URL to open, usually located at the bottom of the page.")]
		protected OpenURL openUrl;

		private PlayerActions playerActions;

		private RectTransform rectTransform;

		private PointerEventData pointerEventData;

		private int selectedTextImage;

		private List<EventTrigger.Entry> selectedEventTriggers;

		private void Start()
		{
			playerActions = PlayerActions.Instance;
			pointerEventData = new PointerEventData(EventSystem.current);
			rectTransform = base.transform as RectTransform;
			DeselectAllTextImages();
		}

		private void OnDisable()
		{
			DeselectAllTextImages();
		}

		private void Update()
		{
			if (mainMenuUIHandler.currentMenuState != MenuState.Roadmap || roadmapHandler.CurrentPageTransform != rectTransform || !playerActions.m_accept.WasPressed || selectedEventTriggers == null)
			{
				return;
			}
			int i = 0;
			for (int count = selectedEventTriggers.Count; i < count; i++)
			{
				EventTrigger.Entry entry = selectedEventTriggers[i];
				if (entry.eventID == EventTriggerType.PointerClick)
				{
					entry.callback?.Invoke(pointerEventData);
				}
			}
		}

		private void DeselectAllTextImages()
		{
			selectedTextImage = -1;
			selectedEventTriggers = null;
			int i = 0;
			for (int num = textImages.Length; i < num; i++)
			{
				RoadmapTextImage roadmapTextImage = textImages[i];
				if (roadmapTextImage != null)
				{
					roadmapTextImage.OnPointerExit(pointerEventData);
				}
			}
		}

		private void SelectTextImage(int direction)
		{
			int num = textImages.Length;
			RoadmapTextImage roadmapTextImage = ((selectedTextImage >= 0 && selectedTextImage < num) ? textImages[selectedTextImage] : null);
			if (roadmapTextImage != null)
			{
				roadmapTextImage.OnPointerExit(pointerEventData);
			}
			selectedEventTriggers = null;
			selectedTextImage += direction;
			if (selectedTextImage < 0)
			{
				if (num == 1 && roadmapTextImage != null)
				{
					selectedTextImage = -1;
				}
				else
				{
					selectedTextImage = num - 1;
				}
			}
			else if (selectedTextImage >= num)
			{
				if (num == 1 && roadmapTextImage != null)
				{
					selectedTextImage = -1;
				}
				else
				{
					selectedTextImage = 0;
				}
			}
			roadmapTextImage = ((selectedTextImage >= 0 && selectedTextImage < num) ? textImages[selectedTextImage] : null);
			if (!(roadmapTextImage == null))
			{
				EventTrigger component = roadmapTextImage.GetComponent<EventTrigger>();
				if (component != null)
				{
					selectedEventTriggers = component.triggers;
				}
				roadmapTextImage.OnPointerEnter(pointerEventData);
			}
		}
	}
}
