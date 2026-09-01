using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FactionBuilder : MonoBehaviour
{
	public FactionSlotButton buttonBase;

	public FactionSlotButton draggedSlotButton;

	private FactionSlotButton[] slotButtons;

	private Vector3 lastMousePosition;

	private Vector3 deltaMouse;

	private void Start()
	{
		lastMousePosition = Input.mousePosition;
	}

	private void LateUpdate()
	{
		deltaMouse = Input.mousePosition - lastMousePosition;
		lastMousePosition = Input.mousePosition;
		FactionSlotButton factionSlotButton = null;
		if (!draggedSlotButton)
		{
			PointerEventData pointerEventData = ExtendedStandaloneInputModule.GetPointerEventData();
			for (int i = 0; i < pointerEventData.hovered.Count; i++)
			{
				if ((bool)pointerEventData.hovered[i].GetComponent<FactionSlotButton>())
				{
					factionSlotButton = pointerEventData.hovered[i].GetComponent<FactionSlotButton>();
				}
			}
		}
		if ((bool)draggedSlotButton)
		{
			DragObject();
		}
		if (Input.GetKeyDown(KeyCode.Mouse0) && (bool)factionSlotButton)
		{
			StartDrag(factionSlotButton);
		}
	}

	private void DragObject()
	{
		if ((bool)draggedSlotButton)
		{
			draggedSlotButton.rect.anchoredPosition = new Vector2(draggedSlotButton.rect.anchoredPosition.x, draggedSlotButton.rect.anchoredPosition.y) + (Vector2)deltaMouse * 0.96f;
		}
		float num = float.PositiveInfinity;
		bool flag = false;
		FactionSlotButton factionSlotButton = null;
		slotButtons = GetComponentsInChildren<FactionSlotButton>();
		for (int i = 0; i < slotButtons.Length; i++)
		{
			if (slotButtons[i] == draggedSlotButton || !CheckIfSnapChild(draggedSlotButton, slotButtons[i]))
			{
				continue;
			}
			float num2 = Vector3.Distance(slotButtons[i].transform.position, draggedSlotButton.transform.position);
			float num3 = num2;
			num2 += (float)((draggedSlotButton.snapTarget == slotButtons[i]) ? (-2) : 0);
			num2 += (float)(slotButtons[i].isSlot ? (-3) : 0);
			if (num2 < 20f && num2 < num && !slotButtons[i].isSlotted)
			{
				if (num3 < 3f && slotButtons[i].isSlot)
				{
					flag = true;
				}
				num = num2;
				factionSlotButton = slotButtons[i];
			}
		}
		if ((bool)factionSlotButton)
		{
			if (flag)
			{
				draggedSlotButton.buttonOffsetVelocity += (factionSlotButton.transform.position - draggedSlotButton.rootImage.transform.position) * Time.deltaTime * 50000f;
				factionSlotButton.rootImage.GetComponent<ScaleJiggle>().velocity += 100f * Time.deltaTime;
			}
			if (draggedSlotButton.snapTarget != factionSlotButton)
			{
				draggedSlotButton.rootImage.GetComponent<ScaleJiggle>().velocity += 5f;
				factionSlotButton.rootImage.GetComponent<ScaleJiggle>().velocity += 5f;
			}
			draggedSlotButton.snapTarget = factionSlotButton;
		}
		else if (!draggedSlotButton.snapTarget || Vector3.Distance(draggedSlotButton.transform.position, draggedSlotButton.snapTarget.transform.position) > 25f)
		{
			if ((bool)draggedSlotButton.snapTarget)
			{
				draggedSlotButton.rootImage.GetComponent<ScaleJiggle>().velocity -= 10f;
				draggedSlotButton.snapTarget.rootImage.GetComponent<ScaleJiggle>().velocity -= 10f;
			}
			draggedSlotButton.snapTarget = null;
		}
		if (Input.GetKeyUp(KeyCode.Mouse0))
		{
			if (flag)
			{
				TransferSlot(draggedSlotButton, factionSlotButton);
			}
			if ((bool)draggedSlotButton)
			{
				EndDrag((Vector2)deltaMouse);
			}
		}
	}

	private void TransferSlot(FactionSlotButton slotter, FactionSlotButton slotted)
	{
		for (int i = 0; i < slotButtons.Length; i++)
		{
			if ((bool)slotButtons[i].snapTarget && slotButtons[i].snapTarget == slotter)
			{
				slotButtons[i].snapTarget = slotted;
			}
		}
		if (slotted.thumbnail.enabled)
		{
			GameObject obj = Object.Instantiate(buttonBase.gameObject, slotted.transform.position, slotted.transform.rotation);
			obj.transform.parent = slotted.transform.parent.parent;
			obj.transform.localScale = slotted.transform.localScale;
			obj.GetComponent<FactionSlotButton>().Throw(((slotted.transform.position - slotter.transform.position).normalized + Vector3.up * 1.5f) * 50f);
		}
		slotted.thumbnail.enabled = true;
		slotted.thumbnail.sprite = slotter.thumbnail.sprite;
		Object.Destroy(slotter.gameObject);
	}

	private bool CheckIfSnapChild(FactionSlotButton original, FactionSlotButton tested)
	{
		bool flag = true;
		if ((bool)tested.snapTarget)
		{
			if (tested.snapTarget == original)
			{
				return false;
			}
			return CheckIfSnapChild(original, tested.snapTarget);
		}
		return true;
	}

	private void StartDrag(FactionSlotButton hovered)
	{
		if (hovered.isSlot)
		{
			if (!hovered.thumbnail.enabled)
			{
				return;
			}
			hovered.thumbnail.enabled = false;
			GameObject obj = Object.Instantiate(buttonBase.gameObject, hovered.transform.position, hovered.transform.rotation);
			obj.transform.parent = hovered.transform.parent.parent;
			obj.transform.localScale = hovered.transform.localScale;
			FactionSlotButton component = obj.GetComponent<FactionSlotButton>();
			for (int i = 0; i < slotButtons.Length; i++)
			{
				if ((bool)slotButtons[i].snapTarget && slotButtons[i].snapTarget == hovered)
				{
					slotButtons[i].snapTarget = component;
				}
			}
			hovered = component;
		}
		draggedSlotButton = hovered;
		draggedSlotButton.isDragged = true;
		draggedSlotButton.PickUp();
		draggedSlotButton.GetComponent<Button>().interactable = false;
	}

	private void EndDrag(Vector3 delta)
	{
		draggedSlotButton.GetComponent<Button>().interactable = true;
		draggedSlotButton.Throw(deltaMouse);
		draggedSlotButton.isDragged = false;
		draggedSlotButton = null;
	}
}
