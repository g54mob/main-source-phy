using Landfall.TABS.Workshop;
using TFBGames;
using UnityEngine;

public class BattleDraggableUIElement : DraggableUIElement
{
	private DraggableDropZone battleListDropZone;

	private DraggableDropZone libraryListDropZone;

	private BattleCreatorCampaignCreatorUI campaignCreatorUi;

	private const float INPUT_DELAY = 0.3f;

	private const float ANALOGUE_BUTTON_PRESS_VALUE = 0.9f;

	private float inputTimer;

	private UIScreenRect battleListScreenRect;

	private UIScreenRect libraryListScreenRect;

	public void Init(BattleCreatorCampaignCreatorUI campaignCreatorUi, DraggableDropZone battleListDropZone, DraggableDropZone libraryListDropZone)
	{
		this.battleListDropZone = battleListDropZone;
		this.libraryListDropZone = libraryListDropZone;
		this.campaignCreatorUi = campaignCreatorUi;
		battleListScreenRect = battleListDropZone.GetComponent<UIScreenRect>();
		libraryListScreenRect = libraryListDropZone.GetComponent<UIScreenRect>();
	}

	protected override void DragWithController()
	{
		if (inputTimer > 0.3f)
		{
			if (m_playerActions.m_uiNavigationVertical.Value > 0.9f)
			{
				inputTimer = 0f;
				Transform transform = hoveredZone.MoveSpacerUp();
				base.transform.position = transform.position;
			}
			else if (m_playerActions.m_uiNavigationVertical.Value < -0.9f)
			{
				inputTimer = 0f;
				Transform transform2 = hoveredZone.MoveSpacerDown();
				base.transform.position = transform2.position;
			}
			else if (Mathf.Abs(m_playerActions.m_uiNavigationHorizontal.Value) > 0.9f)
			{
				inputTimer = 0f;
				hoveredZone.EndHover();
				hoveredZone = ((hoveredZone == battleListDropZone) ? libraryListDropZone : battleListDropZone);
				Transform spacer = hoveredZone.GetSpacer();
				base.transform.position = spacer.position;
			}
		}
		inputTimer += Time.deltaTime;
		HoveringOverDropZone(hoveredZone);
	}

	public void OnNavigatedAway()
	{
		if (m_draggingWithController && hoveredZone != null)
		{
			Object.DestroyImmediate(base.gameObject);
		}
	}

	protected override DraggableDropZone FindDraggableDropZone(Vector3 position)
	{
		DraggableDropZone draggableDropZone = base.FindDraggableDropZone(position);
		if (draggableDropZone != null)
		{
			return draggableDropZone;
		}
		Rect screenRect = battleListScreenRect.ScreenRect;
		if (position.x >= screenRect.xMin && position.x <= screenRect.xMax)
		{
			return battleListDropZone;
		}
		screenRect = libraryListScreenRect.ScreenRect;
		if (!(position.x >= screenRect.xMin) || !(position.x <= screenRect.xMax))
		{
			return null;
		}
		return libraryListDropZone;
	}
}
