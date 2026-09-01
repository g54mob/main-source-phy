using UnityEngine;
using UnityEngine.UI;

public class EventEditorInsertLine : MonoBehaviour
{
	[Header("Insert Line")]
	public GameObject m_InsertLine;

	public GameObject m_InsertLineOneRow;

	public GameObject m_InsertLineTwoRows;

	private EventStage m_InsertLeftStage;

	private EventStage m_InsertRightStage;

	private Image[] m_InsertLineImages;

	private void Awake()
	{
		m_InsertLine.SetActive(value: false);
		m_InsertLineImages = m_InsertLine.GetComponentsInChildren<Image>(includeInactive: true);
	}

	public void UpdateManual()
	{
		m_InsertLine.SetActive(value: false);
		m_InsertLeftStage = null;
		m_InsertRightStage = null;
		m_InsertLine.transform.SetParent(base.transform);
		SetInsertLineColor(GameUI.m_Instance.m_EventEditor.m_InsertLineColor);
		MaybeShowInsertLine();
	}

	public void SetInsertLineColor(Color c)
	{
		if (m_InsertLineImages != null)
		{
			Image[] insertLineImages = m_InsertLineImages;
			for (int i = 0; i < insertLineImages.Length; i++)
			{
				insertLineImages[i].color = c;
			}
		}
	}

	public bool IsActive()
	{
		return m_InsertLine.activeInHierarchy;
	}

	public EventStage GetInsertLeftStage()
	{
		return m_InsertLeftStage;
	}

	public EventStage GetInsertRightStage()
	{
		return m_InsertRightStage;
	}

	private void MaybeShowInsertLine()
	{
		if ((!EventEditor.IsIconMoving() && !EventEditor.IsStageMoving()) || (bool)EventEditor.GetStageUnderMouse(Vector2.zero))
		{
			return;
		}
		EventUnit movingIcon = EventEditor.GetMovingIcon();
		EventStage movingStage = EventEditor.GetMovingStage();
		Rect rect = RectTransformUtility.PixelAdjustRect(EventTimelines.m_Timelines[0].m_CheckpointTrigger.GetComponent<RectTransform>(), GameUI.m_Instance.GetComponent<Canvas>());
		EventStage stageUnderMouse = EventEditor.GetStageUnderMouse(new Vector2(0f - rect.size.x, 0f));
		EventStage stageUnderMouse2 = EventEditor.GetStageUnderMouse(new Vector2(rect.size.x, 0f));
		if ((!movingIcon || (bool)stageUnderMouse2) && ((bool)stageUnderMouse || (bool)stageUnderMouse2))
		{
			m_InsertLine.SetActive(value: true);
			m_InsertLeftStage = stageUnderMouse;
			m_InsertRightStage = stageUnderMouse2;
			EventTimeline eventTimeline = ((stageUnderMouse != null) ? stageUnderMouse.m_ParentTimeline : stageUnderMouse2.m_ParentTimeline);
			bool flag = true;
			if ((bool)movingIcon && !EventEditor.CanPlaceUnitOnTimeline(movingIcon, eventTimeline))
			{
				flag = false;
			}
			if ((bool)movingStage && !EventEditor.CanPlaceStageOnTimeline(movingStage, eventTimeline))
			{
				flag = false;
			}
			if (flag)
			{
				SetInsertLineColor(GameUI.m_Instance.m_EventEditor.m_InsertLineColor);
			}
			else
			{
				SetInsertLineColor(eventTimeline.m_OutlineErrorColor);
			}
			float num = 0f;
			num = ((!stageUnderMouse) ? (stageUnderMouse2.m_RectTransform.anchoredPosition.x - stageUnderMouse2.m_RectTransform.sizeDelta.x / 2f - eventTimeline.m_HorizontalLayoutGroup.spacing / 2f) : (stageUnderMouse.m_RectTransform.anchoredPosition.x + stageUnderMouse.m_RectTransform.sizeDelta.x / 2f + eventTimeline.m_HorizontalLayoutGroup.spacing / 2f));
			m_InsertLine.SetActive(value: true);
			Transform parent = (stageUnderMouse ? stageUnderMouse.m_ParentTimeline.transform : stageUnderMouse2.m_ParentTimeline.transform);
			m_InsertLine.transform.SetParent(parent);
			m_InsertLine.GetComponent<RectTransform>().anchoredPosition = new Vector2(num, 0f);
			if (((bool)stageUnderMouse && stageUnderMouse.m_IconsParent.childCount > 1) || ((bool)stageUnderMouse2 && stageUnderMouse2.m_IconsParent.childCount > 1))
			{
				m_InsertLineOneRow.SetActive(value: false);
				m_InsertLineTwoRows.SetActive(value: true);
			}
			else
			{
				m_InsertLineOneRow.SetActive(value: true);
				m_InsertLineTwoRows.SetActive(value: false);
			}
		}
	}
}
