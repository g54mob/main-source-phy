using UnityEngine;
using UnityEngine.EventSystems;

public class UITriggersController : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, ISelectHandler, IDeselectHandler
{
	[SerializeField]
	private AudioClipTypes downClickAudioClip = AudioClipTypes.UI_Click;

	[SerializeField]
	private AudioClipTypes clickAudioClip = AudioClipTypes.UI_Click;

	[SerializeField]
	private AudioClipTypes hoverAudioClip = AudioClipTypes.UI_Hover;

	public void OnPointerEnter(PointerEventData eventData)
	{
		Singleton<CurserManager>.Instance.SetCurser(CurserTypes.Hand);
		Singleton<AudioManager>.Instance.PlayClip(hoverAudioClip);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Singleton<CurserManager>.Instance.SetCurser(CurserTypes.Normal);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Singleton<AudioManager>.Instance.PlayClip(clickAudioClip);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Singleton<AudioManager>.Instance.PlayClip(downClickAudioClip);
	}

	public void OnSelect(BaseEventData eventData)
	{
		OnPointerEnter(null);
	}

	public void OnDeselect(BaseEventData eventData)
	{
		OnPointerExit(null);
	}
}
