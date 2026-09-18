using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonFeedbackController : MonoBehaviour
{
	public bool DisableController;

	private EventTrigger eventTrigger;

	private Button button;

	[SerializeField]
	private AudioClipTypes clickAudioClip = AudioClipTypes.UI_Click;

	[SerializeField]
	private AudioClipTypes hoverAudioClip = AudioClipTypes.UI_Hover;

	[SerializeField]
	private TweenDuration popTweenDuration = TweenDuration.None;

	[SerializeField]
	private GameObject selectionObject;

	[SerializeField]
	private bool onlyShowSelectedObjectOnController;

	private float startScale = 1f;

	private void Awake()
	{
		button = GetComponent<Button>();
		SetUpEventTriggers();
		if (selectionObject != null)
		{
			selectionObject.SetActive(value: false);
		}
	}

	private void SetUpEventTriggers()
	{
		eventTrigger = base.gameObject.AddComponent<EventTrigger>();
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerClick;
		entry.callback.AddListener(delegate
		{
			OnPointerClick();
		});
		eventTrigger.triggers.Add(entry);
		entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerExit;
		entry.callback.AddListener(delegate
		{
			OnPointerExit();
		});
		eventTrigger.triggers.Add(entry);
		entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerEnter;
		entry.callback.AddListener(delegate
		{
			OnPointerEnter();
		});
		eventTrigger.triggers.Add(entry);
		entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.Select;
		entry.callback.AddListener(delegate
		{
			OnPointerEnter();
		});
		eventTrigger.triggers.Add(entry);
		entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.Submit;
		entry.callback.AddListener(delegate
		{
			OnPointerClick();
		});
		eventTrigger.triggers.Add(entry);
		entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.Deselect;
		entry.callback.AddListener(delegate
		{
			OnPointerExit();
		});
		eventTrigger.triggers.Add(entry);
	}

	public void OnPointerClick()
	{
		if (selectionObject != null)
		{
			selectionObject.SetActive(value: false);
		}
		if (button.interactable && !DisableController)
		{
			Singleton<AudioManager>.Instance.PlayClip(clickAudioClip);
			if (popTweenDuration != TweenDuration.None)
			{
				TweenController.KillTweens(base.gameObject);
				TweenController.PunchScale(base.gameObject, 0.05f, popTweenDuration, null, ignoreTimeScale: true);
			}
		}
	}

	public void OnPointerEnter()
	{
		if (!button.interactable || DisableController)
		{
			return;
		}
		Singleton<CurserManager>.Instance.SetCurser(CurserTypes.Hand);
		Singleton<AudioManager>.Instance.PlayClip(hoverAudioClip);
		if (popTweenDuration != TweenDuration.None)
		{
			TweenController.KillTweens(base.gameObject);
			TweenController.Scale(base.gameObject, startScale, startScale + 0.05f, popTweenDuration, null, ignoreTimeScale: true);
		}
		if (!(selectionObject == null))
		{
			if (onlyShowSelectedObjectOnController)
			{
				selectionObject.SetActive(Singleton<InputManager>.Instance.IsUsingGamepad);
			}
			else
			{
				selectionObject.SetActive(value: true);
			}
		}
	}

	public void OnPointerExit()
	{
		if (button.interactable && !DisableController)
		{
			Singleton<CurserManager>.Instance.ReturnToNormalCurser();
			if (popTweenDuration != TweenDuration.None)
			{
				TweenController.KillTweens(base.gameObject);
				TweenController.Scale(base.gameObject, startScale + 0.05f, startScale, popTweenDuration, null, ignoreTimeScale: true);
			}
			if (selectionObject != null)
			{
				selectionObject.SetActive(value: false);
			}
		}
	}
}
