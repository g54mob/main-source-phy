using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class FactionCreatorFadeBG : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		public Image fade;

		public float targetFade;

		public CustomContentSideBar sidebar;

		private void Update()
		{
			Color color = fade.color;
			color.a = Mathf.Lerp(color.a, targetFade, 10f * Time.deltaTime);
			fade.color = color;
		}

		public void SetOn()
		{
			fade.raycastTarget = true;
			targetFade = 0.85f;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			sidebar.CloseFactionPreview();
		}

		public void SetOff()
		{
			fade.raycastTarget = false;
			targetFade = 0f;
		}
	}
}
