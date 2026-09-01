using System;
using Unity.Mathematics;
using UnityEngine;

namespace Landfall.TABC
{
	public class DragHandler : MonoBehaviour
	{
		public static DragHandler instance;

		public LayerMask mask;

		public Action StartDragAction;

		public Action EndDragAction;

		public bool isDragging;

		[HideInInspector]
		public GameObject draggedObject;

		private UnitDataInstance draggedUnitData;

		private UnitButton fromButton;

		public Action<UnitDataInstance, UnitButton, GameObject> startDragAction;

		public Action<int2, UnitButton> endDragAction;

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			ShopHandler shopHandler = ShopHandler.instance;
			shopHandler.ShopRefreshAction = (Action)Delegate.Combine(shopHandler.ShopRefreshAction, new Action(CancelDragIfDraggingShopUnit));
			RoundHandler roundHandler = RoundHandler.instance;
			roundHandler.EnterBattleAction = (Action)Delegate.Combine(roundHandler.EnterBattleAction, new Action(CancelDragIfDraggingBoardUnit));
		}

		private void LateUpdate()
		{
			if ((bool)draggedObject)
			{
				if (!isDragging)
				{
					if (StartDragAction != null)
					{
						StartDragAction();
					}
					isDragging = true;
				}
			}
			else if (isDragging)
			{
				if (EndDragAction != null)
				{
					EndDragAction();
				}
				isDragging = false;
			}
			DoEndDrag();
			DoLookForUnitToPickUp();
			DoDrag();
		}

		private void DoEndDrag()
		{
			if (Input.GetKeyUp(KeyCode.Mouse0))
			{
				EndDrag();
			}
		}

		private void DoLookForUnitToPickUp()
		{
			if (!RoundHandler.instance.CanPlaceUnits() || !Input.GetKeyDown(KeyCode.Mouse0))
			{
				return;
			}
			BoardHoverInfo boardHoverInfo = BoardManager.instance.MouseIsOverBoard();
			if (boardHoverInfo.isHovered)
			{
				Board.Unit unitFromPos = BoardManager.instance.GetUnitFromPos(boardHoverInfo.boardPos);
				if ((bool)unitFromPos.unitObject)
				{
					StartDrag(unitFromPos.unitDataInstance, null, unitFromPos.unitObject);
				}
			}
		}

		private void DoDrag()
		{
			if ((bool)draggedObject)
			{
				draggedObject.transform.position = RayCastDragPos();
			}
		}

		private Vector3 RayCastDragPos()
		{
			Vector3 result = Vector3.zero;
			Physics.Raycast(MainCam.instance.cam.ScreenPointToRay(Input.mousePosition), out var hitInfo, 1000f, mask);
			if ((bool)hitInfo.transform)
			{
				result = hitInfo.point + Vector3.up * 2f;
			}
			return result;
		}

		public void GetOffBoard(UnitData dataToGetOff)
		{
			dataToGetOff.isBeingDestroyed = true;
			int freeBenchSlot = GetFreeBenchSlot();
			if (freeBenchSlot != -1)
			{
				BoardManager.instance.RemoveUnit(dataToGetOff.dataInstance.boardPos);
				MoveUnitAround(dataToGetOff.dataInstance, BoardManagerUI.instance.buttons[freeBenchSlot], new int2(0, 0));
			}
			else
			{
				Sell(dataToGetOff.dataInstance);
				dataToGetOff.Destroy();
			}
			UnitHandler.instance.UpdateUnits();
		}

		private int GetFreeBenchSlot()
		{
			int result = -1;
			for (int i = 0; i < BoardManagerUI.instance.buttons.Length; i++)
			{
				if (!BoardManagerUI.instance.buttons[i].data.dataInstance.unit)
				{
					result = i;
				}
			}
			return result;
		}

		public void StartDrag(UnitDataInstance unitDataToDrag, UnitButton bottonToDragFrom = null, GameObject draggedUnitObject = null)
		{
			if (!(unitDataToDrag.unit == null))
			{
				draggedUnitData = new UnitDataInstance(unitDataToDrag.unit, draggedUnitObject, unitDataToDrag.ownedByPlayer, unitDataToDrag.boardPos, unitDataToDrag.level);
				if ((bool)bottonToDragFrom)
				{
					draggedObject = unitDataToDrag.Spawn();
					fromButton = bottonToDragFrom;
					bottonToDragFrom.Clear();
				}
				else
				{
					draggedObject = draggedUnitData.unitObject;
					draggedObject.GetComponent<UnitData>().RemoveUnit();
				}
				startDragAction?.Invoke(draggedUnitData, bottonToDragFrom, draggedUnitObject);
			}
		}

		public void EndDrag()
		{
			if ((bool)draggedObject && !(draggedUnitData.unit == null))
			{
				UnitButton currentUnitButton = BoardManagerUI.instance.currentUnitButton;
				BoardHoverInfo boardHoverInfo = BoardManager.instance.MouseIsOverBoard();
				endDragAction?.Invoke(boardHoverInfo.boardPos, currentUnitButton);
				if (boardHoverInfo.isHovered && boardHoverInfo.boardPos.y >= 0 && !GameFlowHandlerServer.isDebug)
				{
					UIEffects.instance.NotThere();
					CancelDrag();
				}
				else if ((bool)currentUnitButton && currentUnitButton.isSellButton)
				{
					Sell(draggedUnitData);
				}
				else if ((bool)currentUnitButton || boardHoverInfo.isHovered)
				{
					DragSwap(currentUnitButton, boardHoverInfo);
				}
				else
				{
					UIEffects.instance.NotThere();
					CancelDrag();
				}
				UnitHandler.instance.UpdateUnits();
			}
		}

		private void PlaceUnitOnBoard(UnitDataInstance unit, int2 boardPos)
		{
			GameObject gameObject = unit.Spawn();
			gameObject.GetComponent<UnitData>().dataInstance.ownedByPlayer = true;
			gameObject.GetComponent<UnitData>().dataInstance.unitObject = gameObject;
			gameObject.GetComponent<UnitData>().dataInstance.level = unit.level;
			gameObject.transform.position = BoardData.instance.BoardPosToWorld(boardPos);
			gameObject.GetComponent<UnitData>().PlaceUnit();
			UnitDataInstance dataInstance = gameObject.GetComponent<UnitData>().dataInstance;
			BoardManager.instance.AddNewUnit(gameObject, dataInstance, boardPos);
		}

		private void MoveUnitAround(UnitDataInstance data, UnitButton newButton, int2 boardPos)
		{
			if ((bool)newButton)
			{
				if (data != null)
				{
					newButton.GetComponent<ScaleShake>()?.AddForce(0.1f);
				}
				newButton.SetUnit(data, data.ownedByPlayer);
			}
			else
			{
				PlaceUnitOnBoard(data, boardPos);
			}
			if ((bool)data.unitObject)
			{
				UnityEngine.Object.Destroy(data.unitObject);
			}
		}

		private void BuyUnit(UnitDataInstance data, UnitButton benchButton, int2 boardPos)
		{
			if ((bool)benchButton)
			{
				if (data != null)
				{
					benchButton.GetComponent<ScaleShake>()?.AddForce(0.1f);
				}
				benchButton.SetUnit(data, isOWned: true);
			}
			else
			{
				PlaceUnitOnBoard(data, boardPos);
			}
		}

		public void Sell(UnitDataInstance unitData, bool getNoMoney = false)
		{
			if (unitData.ownedByPlayer)
			{
				if ((bool)unitData.unitObject)
				{
					BoardManager.instance.RemoveUnit(unitData.boardPos);
				}
				if (!getNoMoney)
				{
					WalletHandlerClient.instance.AddMoney(unitData.unit.cost);
				}
			}
			RemoveDrag();
			UnitHandler.instance.UpdateUnits();
		}

		private void SellUnit(UnitDataInstance data, UnitButton shopButton, int2 boardPos)
		{
			if ((bool)shopButton)
			{
				if (data != null)
				{
					shopButton.GetComponent<ScaleShake>().AddForce(0.1f);
				}
				shopButton.SetUnit(data, isOWned: false);
			}
			if ((bool)data.unitObject)
			{
				UnityEngine.Object.Destroy(data.unitObject);
			}
		}

		private void DragSwap(UnitButton dropButton, BoardHoverInfo hoverInfo)
		{
			UnitDataInstance unitDataInstance = null;
			if ((bool)dropButton)
			{
				if (dropButton.data != null)
				{
					unitDataInstance = new UnitDataInstance(dropButton.data.dataInstance.unit, dropButton.data.dataInstance.unitObject, dropButton.data.dataInstance.ownedByPlayer, dropButton.data.dataInstance.boardPos, dropButton.data.dataInstance.level);
				}
			}
			else
			{
				Board.Unit unitFromPos = BoardManager.instance.GetUnitFromPos(hoverInfo.boardPos);
				if (unitFromPos.unitDataInstance != null)
				{
					unitDataInstance = new UnitDataInstance(unitFromPos.unitDataInstance.unit, unitFromPos.unitDataInstance.unitObject, unitFromPos.unitDataInstance.ownedByPlayer, unitFromPos.unitDataInstance.boardPos, unitFromPos.unitDataInstance.level);
				}
			}
			if ((unitDataInstance == null && draggedUnitData.ownedByPlayer) || (unitDataInstance != null && draggedUnitData.ownedByPlayer == unitDataInstance.ownedByPlayer))
			{
				if (!dropButton && !RoundHandler.instance.CanPlaceUnits())
				{
					UIEffects.instance.NotDuringBattle();
					CancelDrag();
				}
				else
				{
					if ((bool)draggedUnitData.unitObject)
					{
						BoardManager.instance.RemoveUnit(draggedUnitData.boardPos);
					}
					if (unitDataInstance != null && (!draggedUnitData.boardPos.Equals(hoverInfo.boardPos) || !hoverInfo.isHovered))
					{
						if ((bool)unitDataInstance.unitObject)
						{
							BoardManager.instance.RemoveUnit(hoverInfo.boardPos);
						}
						if ((bool)unitDataInstance.unit)
						{
							MoveUnitAround(unitDataInstance, fromButton, draggedUnitData.boardPos);
						}
					}
					MoveUnitAround(draggedUnitData, dropButton, hoverInfo.boardPos);
				}
			}
			else if (draggedUnitData.ownedByPlayer)
			{
				if (!dropButton && !RoundHandler.instance.CanPlaceUnits())
				{
					UIEffects.instance.NotDuringBattle();
					CancelDrag();
				}
				else
				{
					int num = 0;
					if (draggedUnitData != null)
					{
						num = draggedUnitData.unit.cost;
					}
					int num2 = 0;
					if (unitDataInstance != null && (bool)unitDataInstance.unit)
					{
						num2 += unitDataInstance.unit.cost;
					}
					num2 -= num;
					if (WalletHandlerClient.instance.Spend(num2))
					{
						if ((bool)draggedUnitData.unitObject)
						{
							BoardManager.instance.RemoveUnit(draggedUnitData.boardPos);
						}
						if (unitDataInstance != null && (bool)unitDataInstance.unit)
						{
							BuyUnit(unitDataInstance, fromButton, draggedUnitData.boardPos);
						}
						SellUnit(draggedUnitData, dropButton, hoverInfo.boardPos);
					}
					else
					{
						CancelDrag();
					}
				}
			}
			else if (!dropButton && !RoundHandler.instance.CanPlaceUnits())
			{
				UIEffects.instance.NotDuringBattle();
				CancelDrag();
			}
			else
			{
				int num3 = 0;
				if (unitDataInstance != null && (bool)unitDataInstance.unit)
				{
					num3 = unitDataInstance.unit.cost;
				}
				int num4 = 0;
				if ((bool)draggedUnitData.unit)
				{
					num4 += draggedUnitData.unit.cost;
				}
				num4 -= num3;
				if (WalletHandlerClient.instance.Spend(num4))
				{
					if (unitDataInstance != null && (bool)unitDataInstance.unitObject)
					{
						BoardManager.instance.RemoveUnit(unitDataInstance.boardPos);
					}
					if (unitDataInstance != null && unitDataInstance.unit != null)
					{
						SellUnit(unitDataInstance, fromButton, draggedUnitData.boardPos);
					}
					BuyUnit(draggedUnitData, dropButton, hoverInfo.boardPos);
				}
				else
				{
					CancelDrag();
				}
			}
			RemoveDrag();
		}

		private void CancelDrag()
		{
			if (draggedUnitData != null)
			{
				if ((bool)fromButton)
				{
					fromButton.SetUnit(draggedUnitData, draggedUnitData.ownedByPlayer);
				}
				else
				{
					BoardManager.instance.RemoveUnit(draggedUnitData.boardPos);
					PlaceUnitOnBoard(draggedUnitData, draggedUnitData.boardPos);
				}
			}
			UnitHandler.instance.UpdateUnits();
			RemoveDrag();
		}

		private void CancelDragIfDraggingBoardUnit()
		{
			if (draggedUnitData != null && !fromButton)
			{
				UIEffects.instance.NotDuringBattle();
				CancelDrag();
			}
		}

		private void CancelDragIfDraggingShopUnit()
		{
			if (draggedUnitData != null && !draggedUnitData.ownedByPlayer)
			{
				UIEffects.instance.TooSlow();
				CancelDrag();
			}
		}

		private void RemoveDrag(bool destroyDraggedObject = true)
		{
			if ((bool)draggedObject && destroyDraggedObject)
			{
				UnityEngine.Object.Destroy(draggedObject);
			}
			fromButton = null;
			draggedUnitData = null;
			draggedObject = null;
			endDragAction?.Invoke(new int2(-100, -100), null);
		}
	}
}
