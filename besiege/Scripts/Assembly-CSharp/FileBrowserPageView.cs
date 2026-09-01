using System;
using System.Collections.Generic;
using UnityEngine;

public class FileBrowserPageView : MonoBehaviour
{
	public int FilesPerPage = 12;

	public Action<IVirtualObject> SlotClicked;

	public Action<IVirtualObject> SlotDoubleClicked;

	public Action<IVirtualObject> SlotDeleteConfirmed;

	public Action<IVirtualObject> SlotToggleRemoteClicked;

	public Action<IVirtualObject> SlotUploadClicked;

	public Action<IVirtualObject> SlotDownloadClicked;

	public Action<IVirtualObject> SlotLoadAsSelectionClicked;

	public Action<IVirtualObject> SlotVersionsClicked;

	[SerializeField]
	private float lerpSpeed = 90f;

	[SerializeField]
	private float pageMarginX = 20f;

	[SerializeField]
	private float marginX = 2.15f;

	[SerializeField]
	private float marginY = 2.59f;

	[SerializeField]
	private FileBrowserSlot fileBrowserSlotPrefab;

	[SerializeField]
	private Transform fileSlotParentTransform;

	private bool isOpen;

	private FileBrowserSlot selectedSlot;

	private List<FileBrowserSlot> objectSlots;

	private WorkshopType workshopType;

	private FileBrowserView view;

	public void Initialize(FileBrowserView view, IEnumerable<IVirtualObject> objects, WorkshopType workshopType)
	{
		objectSlots = new List<FileBrowserSlot>();
		this.workshopType = workshopType;
		this.view = view;
		GenerateSlots(objects);
	}

	public void Open(PageMoveDirection direction)
	{
		isOpen = true;
		SetActive(true);
		if (direction == PageMoveDirection.None)
		{
			OnTweenComplete();
			return;
		}
		base.transform.localPosition = new Vector3((direction != PageMoveDirection.Right) ? pageMarginX : (0f - pageMarginX), 0f, 0f);
		MovePage(direction);
	}

	public void Close(PageMoveDirection direction)
	{
		isOpen = false;
		RemoveDelegates();
		if (direction == PageMoveDirection.None)
		{
			SetActive(false);
		}
		else
		{
			MovePage(direction);
		}
	}

	protected virtual void AssignSlotDelegates(FileBrowserSlot slot)
	{
		slot.Clicked = OnSlotClicked;
		slot.DoubleClicked = OnSlotDoubleClicked;
		slot.DeleteClicked = OnSlotDeleteClicked;
		slot.DeleteConfirmed = OnSlotDeleteConfirmed;
		slot.ToggleRemoteClicked = OnSlotToggleRemoteClicked;
		slot.UploadClicked = OnSlotUploadClicked;
		slot.DownloadClicked = OnSlotDownloadClicked;
		slot.LoadAsSelectionClicked = OnSlotLoadAsSelectionClicked;
		slot.VersionsClicked = OnSlotVersionsClicked;
	}

	protected virtual void RemoveSlotDelegates(FileBrowserSlot slot)
	{
		slot.Clicked = null;
		slot.DoubleClicked = null;
		slot.DeleteClicked = null;
		slot.DeleteConfirmed = null;
		slot.UploadClicked = null;
		slot.DownloadClicked = null;
		slot.LoadAsSelectionClicked = null;
		slot.VersionsClicked = null;
	}

	private void MovePage(PageMoveDirection direction)
	{
		Vector3 toPosition = ((direction != PageMoveDirection.Left) ? new Vector3(base.transform.localPosition.x + pageMarginX, 0f, 0f) : new Vector3(base.transform.localPosition.x - pageMarginX, 0f, 0f));
		MovePage(toPosition);
	}

	private void MovePage(Vector3 toPosition)
	{
		iTween.Stop(base.gameObject);
		iTween.MoveTo(base.gameObject, iTween.Hash("position", toPosition, "islocal", true, "speed", lerpSpeed, "oncomplete", "OnTweenComplete", "ignoretimescale", true, "easetype", iTween.EaseType.linear));
	}

	private void OnTweenComplete()
	{
		if (!isOpen)
		{
			SetActive(false);
		}
		else
		{
			AssignDelegates();
		}
	}

	private void SetActive(bool a)
	{
		base.gameObject.SetActive(a);
		foreach (FileBrowserSlot objectSlot in objectSlots)
		{
			if (a)
			{
				objectSlot.SetActive();
				objectSlot.Thumbnail.SetVisible();
			}
			else
			{
				objectSlot.Thumbnail.SetInvisible();
			}
		}
	}

	private void AssignDelegates()
	{
		foreach (FileBrowserSlot objectSlot in objectSlots)
		{
			AssignSlotDelegates(objectSlot);
		}
	}

	private void RemoveDelegates()
	{
		foreach (FileBrowserSlot objectSlot in objectSlots)
		{
			RemoveSlotDelegates(objectSlot);
		}
	}

	private void GenerateSlots(IEnumerable<IVirtualObject> virtualObjects)
	{
		int num = 0;
		foreach (IVirtualObject virtualObject in virtualObjects)
		{
			if (objectSlots.Count == FilesPerPage)
			{
				break;
			}
			FileBrowserSlot item = GenerateSlot(num++, virtualObject);
			objectSlots.Add(item);
		}
	}

	private Vector3 CalculatePosition(int elementNumber)
	{
		Vector3 zero = Vector3.zero;
		int num = FilesPerPage / 2;
		zero.x = (float)(elementNumber % num) * marginX;
		zero.y = (float)(elementNumber / num) * (0f - marginY);
		return zero;
	}

	private FileBrowserSlot GenerateSlot(int slotIndex, IVirtualObject virtualObject)
	{
		FileBrowserSlot fileBrowserSlot = UnityEngine.Object.Instantiate(fileBrowserSlotPrefab);
		Vector3 localScale = fileBrowserSlot.transform.localScale;
		fileBrowserSlot.gameObject.SetActive(true);
		fileBrowserSlot.transform.parent = fileSlotParentTransform;
		fileBrowserSlot.transform.localPosition = CalculatePosition(slotIndex);
		fileBrowserSlot.transform.localScale = localScale;
		fileBrowserSlot.Initialize(view, virtualObject, workshopType);
		return fileBrowserSlot;
	}

	private void OnSlotUploadClicked(FileBrowserSlot slot)
	{
		if (SlotUploadClicked != null)
		{
			SlotUploadClicked(slot.VirtualObject);
		}
	}

	private void OnSlotDoubleClicked(FileBrowserSlot slot)
	{
		if (SlotDoubleClicked != null)
		{
			SlotDoubleClicked(slot.VirtualObject);
		}
	}

	private void OnSlotDownloadClicked(FileBrowserSlot slot)
	{
		if (SlotDownloadClicked != null)
		{
			SlotDownloadClicked(slot.VirtualObject);
		}
	}

	private void OnSlotClicked(FileBrowserSlot slot)
	{
		SelectSlot(slot);
		if (SlotClicked != null)
		{
			SlotClicked(slot.VirtualObject);
		}
	}

	private void OnSlotLoadAsSelectionClicked(FileBrowserSlot slot)
	{
		if (SlotLoadAsSelectionClicked != null)
		{
			SlotLoadAsSelectionClicked(slot.VirtualObject);
		}
	}

	private void OnSlotVersionsClicked(FileBrowserSlot slot)
	{
		if (SlotVersionsClicked != null)
		{
			SlotVersionsClicked(slot.VirtualObject);
		}
	}

	private void SelectSlot(FileBrowserSlot slot)
	{
		if (selectedSlot != slot)
		{
			DeselectSlot();
		}
		selectedSlot = slot;
		selectedSlot.Select();
	}

	private void DeselectSlot()
	{
		if (selectedSlot != null)
		{
			selectedSlot.Deselect();
		}
		selectedSlot = null;
	}

	private void OnSlotDeleteConfirmed(FileBrowserSlot slot)
	{
		DeselectSlot();
		if (SlotDeleteConfirmed != null)
		{
			SlotDeleteConfirmed(slot.VirtualObject);
		}
	}

	private void OnSlotToggleRemoteClicked(FileBrowserSlot slot)
	{
		if (SlotToggleRemoteClicked != null)
		{
			SlotToggleRemoteClicked(slot.VirtualObject);
		}
	}

	private void OnSlotDeleteClicked(FileBrowserSlot slot)
	{
		SelectSlot(slot);
	}
}
