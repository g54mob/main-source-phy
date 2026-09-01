using UnityEngine;

public class LogicEventDisplay : MonoBehaviour
{
	public Transform backgroundTransform;

	public Transform line;

	public UIButton nextButton;

	public UIButton prevButton;

	public GameObject infoIcon;

	protected EntityEvent currentEvent;

	protected LogicEventWidget eventWidget;

	protected EntityLogic currentLogic;

	protected float defaultHeight = 1f;

	protected bool isEditing;

	protected virtual void Awake()
	{
		prevButton.Click += OnPrev;
		nextButton.Click += OnNext;
	}

	public virtual void Init(LogicEventWidget parentWidget, EntityLogic inLogic, EntityEvent inEvent)
	{
		currentEvent = inEvent;
		currentLogic = inLogic;
		eventWidget = parentWidget;
		isEditing = true;
	}

	public virtual void UpdateVisual()
	{
	}

	public virtual void ToggleHover(bool toggle)
	{
		nextButton.gameObject.SetActive(toggle);
		prevButton.gameObject.SetActive(toggle);
		infoIcon.SetActive(toggle);
	}

	public virtual void ResetToPool()
	{
		isEditing = false;
	}

	protected virtual void UpdateBackground()
	{
		backgroundTransform.localScale = new Vector3(backgroundTransform.localScale.x, defaultHeight, backgroundTransform.localScale.z);
		UpdateBottomLine();
	}

	protected void UpdateBottomLine()
	{
		if (eventWidget.Logic.events.Count < 2 || eventWidget.Logic.events[eventWidget.Logic.events.Count - 1] == currentEvent)
		{
			if (line.gameObject.activeSelf)
			{
				line.gameObject.SetActive(false);
			}
			return;
		}
		if (!line.gameObject.activeSelf)
		{
			line.gameObject.SetActive(true);
		}
		line.localPosition = new Vector3(0f, 0f - backgroundTransform.localScale.y, 0f);
	}

	private void OnNext()
	{
		eventWidget.OnNext();
	}

	private void OnPrev()
	{
		eventWidget.OnPrev();
	}
}
