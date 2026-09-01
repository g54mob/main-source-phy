using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using cakeslice;

public class SelectionTool : MonoBehaviour
{
	public class AdditionalData
	{
		public ISelectable selectable;

		public int symmetryIndex;

		public float transformMultiplier = 1f;
	}

	private const float timeToDrag = 0.12f;

	public bool active = true;

	protected bool _startDragging;

	private float dragWaited;

	public static bool BatchChange = false;

	private static readonly List<AdditionalData> EmptyAdditionalList = new List<AdditionalData>(0);

	[SerializeField]
	protected Transform ToolTransform;

	public RectTransform selectionReference;

	protected List<ISelectable> SelectedObjects = new List<ISelectable>();

	protected List<ISelectable> DragSelection = new List<ISelectable>();

	private HashSet<long> DragSelectionIds = new HashSet<long>();

	protected Vector2 downPos;

	protected Vector2 lastPos;

	[SerializeField]
	protected RectTransform rectTransform;

	[SerializeField]
	protected Image image;

	protected bool isInitialized;

	protected bool isMultiSelect;

	protected bool isSubtractiveSelect;

	protected Vector2 canvasScale;

	protected bool once;

	protected bool movedMouse;

	public bool IsDragging
	{
		get
		{
			return dragWaited > 0.12f;
		}
	}

	public int Count
	{
		get
		{
			return SelectedObjects.Count;
		}
	}

	public ISelectable First
	{
		get
		{
			object result;
			if (Count > 0)
			{
				ISelectable selectable = SelectedObjects[0];
				result = selectable;
			}
			else
			{
				result = null;
			}
			return (ISelectable)result;
		}
	}

	public ISelectable Last
	{
		get
		{
			object result;
			if (Count > 0)
			{
				ISelectable selectable = SelectedObjects[Count - 1];
				result = selectable;
			}
			else
			{
				result = null;
			}
			return (ISelectable)result;
		}
	}

	public List<ISelectable> Selection
	{
		get
		{
			return new List<ISelectable>(SelectedObjects);
		}
	}

	public virtual bool CanSelect()
	{
		return false;
	}

	public virtual bool CanDrag()
	{
		return CanSelect();
	}

	public void Init()
	{
		if (!isInitialized)
		{
			if (ToolTransform == null)
			{
				ToolTransform = LevelEditor.Instance.ToolTransform;
			}
			if (rectTransform == null)
			{
				rectTransform = GetComponent<RectTransform>();
			}
			if (image == null)
			{
				image = GetComponent<Image>();
			}
			isInitialized = true;
		}
	}

	protected void Start()
	{
		OnResolutionChanged();
		ReferenceMaster.onResolutionChanged = (Action)Delegate.Combine(ReferenceMaster.onResolutionChanged, new Action(OnResolutionChanged));
	}

	protected void OnDestroy()
	{
		BatchChange = false;
		ReferenceMaster.onResolutionChanged = (Action)Delegate.Remove(ReferenceMaster.onResolutionChanged, new Action(OnResolutionChanged));
	}

	private void OnResolutionChanged()
	{
		BesiegeConfig besiegeConfig = OptionsMaster.BesiegeConfig;
		canvasScale = new Vector2(selectionReference.rect.size.x / (float)besiegeConfig.ScreenWidth, selectionReference.rect.size.y / (float)besiegeConfig.ScreenHeight);
	}

	public void Hide()
	{
		if (image.enabled)
		{
			image.enabled = false;
		}
		_startDragging = false;
		movedMouse = false;
		dragWaited = 0f;
	}

	public Vector3 GetSelectionCenter()
	{
		Vector3 zero = Vector3.zero;
		int num = 0;
		for (int i = 0; i < SelectedObjects.Count; i++)
		{
			if (!SelectedObjects[i].IsSelectedExtra)
			{
				num++;
				zero += SelectedObjects[i].GetCenter();
			}
		}
		return (num <= 0) ? Vector3.zero : (zero / num);
	}

	public void Clear()
	{
		DeselectAll(false);
	}

	public virtual bool Contains(long id)
	{
		for (int i = 0; i < SelectedObjects.Count; i++)
		{
			if (SelectedObjects[i].identifier == id)
			{
				return true;
			}
		}
		return false;
	}

	public virtual void Remove(ISelectable entity)
	{
		if (entity.IsSelected)
		{
			RemoveFromSelection(entity);
			if (DragSelection.Contains(entity))
			{
				DragSelection.Remove(entity);
			}
		}
	}

	public void Select(ISelectable entity, bool multiSelect, bool addToUndo, bool checkAdditional)
	{
		List<ISelectable> list = new List<ISelectable>();
		List<AdditionalData> extraBlocks = new List<AdditionalData>();
		if (checkAdditional)
		{
			List<ISelectable> list2 = new List<ISelectable>();
			list2.Add(entity);
			List<ISelectable> list3 = list2;
			if (multiSelect)
			{
				List<ISelectable> oldSelection = SelectedObjects.Union(DragSelection).ToList();
				List<ISelectable> completeNewSelection = oldSelection.Where((ISelectable s) => !s.IsSelectedExtra).Union(list3).ToList();
				GetAdditionalSelection(completeNewSelection).ForEach(delegate(AdditionalData x)
				{
					ISelectable selectable2 = x.selectable;
					if (!oldSelection.Contains(selectable2))
					{
						extraBlocks.Add(x);
					}
					else if (selectable2.IsSelectedExtra && (selectable2.SymmetryIndex != x.symmetryIndex || selectable2.TransformMultiplier != x.transformMultiplier))
					{
						extraBlocks.Add(x);
					}
				});
			}
			else
			{
				extraBlocks = GetAdditionalSelection(list3);
			}
			for (int num = 0; num < extraBlocks.Count; num++)
			{
				ISelectable selectable = extraBlocks[num].selectable;
				list.Add(selectable);
			}
		}
		list.Add(entity);
		Select(list, multiSelect, addToUndo, extraBlocks);
	}

	public virtual void Select(ISelectable entity, bool multiSelect, bool addToUndo, bool isAdditional, int symmetryIndex, float transformMultiplier)
	{
		List<AdditionalData> list = new List<AdditionalData>();
		if (isAdditional)
		{
			list.Add(new AdditionalData
			{
				selectable = entity,
				symmetryIndex = symmetryIndex,
				transformMultiplier = transformMultiplier
			});
		}
		Select(new List<ISelectable> { entity }, multiSelect, addToUndo, list);
	}

	public virtual void Select(List<ISelectable> entities, bool multiSelect, bool addToUndo, List<AdditionalData> additionalData = null)
	{
		if (entities.Count == 0)
		{
			return;
		}
		BeforeSelectionChanged();
		List<ISelectable> completeEntities = new List<ISelectable>(entities);
		if (additionalData == null)
		{
			object source;
			if (multiSelect)
			{
				IEnumerable<ISelectable> enumerable = SelectedObjects.Union(entities);
				source = enumerable;
			}
			else
			{
				source = entities;
			}
			List<ISelectable> selectList = ((IEnumerable<ISelectable>)source).Where((ISelectable s) => !s.IsSelectedExtra).ToList();
			additionalData = (from s in GetAdditionalSelection(selectList)
				where !selectList.Contains(s.selectable)
				select s).ToList();
			additionalData.ForEach(delegate(AdditionalData x)
			{
				if (!completeEntities.Contains(x.selectable) || x.selectable.IsSelectedExtra)
				{
					completeEntities.Add(x.selectable);
				}
			});
		}
		bool flag = additionalData.Count > 0;
		if (!multiSelect)
		{
			DeselectAll(addToUndo, false);
		}
		if (StatMaster.Mode.displayDrag && AeroDynamicDisplay.IsSelected)
		{
			AeroDynamicDisplay.Select(false);
		}
		bool flag2 = false;
		foreach (ISelectable item in completeEntities)
		{
			if (!item.IsDestroyed)
			{
				bool isSelected = item.IsSelected;
				if (isSelected)
				{
					AddSelectionChangeUndo(item, false);
					item.Select(false);
				}
				else
				{
					flag2 = true;
				}
				int num = ((!flag) ? (-1) : AdditionalIndex(additionalData, item));
				if (num != -1)
				{
					AdditionalData additionalData2 = additionalData[num];
					item.IsSelectedExtra = true;
					item.SymmetryIndex = additionalData2.symmetryIndex;
					item.TransformMultiplier = additionalData2.transformMultiplier;
				}
				else
				{
					item.IsSelectedExtra = false;
					item.SymmetryIndex = 0;
					item.TransformMultiplier = 1f;
				}
				if (addToUndo && !isSelected)
				{
					AddSelectionChangeUndo(item, true);
				}
				AddToSelection(item);
			}
		}
		if (flag2)
		{
			OutlineEffect.ToggleOutline(SelectedObjects.Count > 0);
		}
		if (addToUndo)
		{
			StoreSelectionChangeUndos();
		}
		if (flag2 && !IsDragging && !BatchChange)
		{
			SelectionChanged();
		}
	}

	protected virtual void AddToSelection(ISelectable entity)
	{
		entity.Select(true);
		SelectedObjects.Add(entity);
	}

	protected virtual void RemoveFromSelection(ISelectable entity)
	{
		SelectedObjects.Remove(entity);
		entity.Select(false);
	}

	protected void AddAdditional(List<AdditionalData> additionalList, AdditionalData data)
	{
		int num = AdditionalIndex(additionalList, data.selectable);
		if (num == -1)
		{
			additionalList.Add(data);
			return;
		}
		AdditionalData additionalData = additionalList[num];
		additionalData.symmetryIndex = Mathf.Min(additionalData.symmetryIndex, data.symmetryIndex);
		additionalData.transformMultiplier = Mathf.Max(additionalData.transformMultiplier, data.transformMultiplier);
	}

	protected int AdditionalIndex(List<AdditionalData> additionalList, ISelectable selectable)
	{
		return additionalList.FindIndex((AdditionalData x) => x.selectable == selectable);
	}

	protected virtual List<AdditionalData> GetAdditionalSelection(IEnumerable<ISelectable> completeNewSelection)
	{
		return new List<AdditionalData>();
	}

	protected virtual void BeforeSelectionChanged()
	{
	}

	protected virtual void SelectionChanged()
	{
	}

	protected virtual void AddSelectionChangeUndo(ISelectable entity, bool selected)
	{
	}

	protected virtual void StoreSelectionChangeUndos()
	{
	}

	public void Deselect(ISelectable entity, bool addToUndo, bool updateAdditional)
	{
		List<ISelectable> entityList = new List<ISelectable> { entity };
		if (updateAdditional)
		{
			IEnumerable<ISelectable> source = SelectedObjects.Union(DragSelection);
			IEnumerable<ISelectable> noExtraList = source.Where((ISelectable x) => !x.IsSelectedExtra);
			List<AdditionalData> additionalSelection = GetAdditionalSelection(noExtraList);
			List<AdditionalData> newAdditionals = GetAdditionalSelection(noExtraList.Where((ISelectable x) => x != entity));
			additionalSelection.ForEach(delegate(AdditionalData x)
			{
				ISelectable selectable = x.selectable;
				int num = AdditionalIndex(newAdditionals, selectable);
				if (num != -1)
				{
					AdditionalData additionalData = newAdditionals[num];
					if (x.symmetryIndex != additionalData.symmetryIndex || x.transformMultiplier != additionalData.transformMultiplier)
					{
						if (addToUndo)
						{
							AddSelectionChangeUndo(selectable, false);
						}
						selectable.SymmetryIndex = additionalData.symmetryIndex;
						selectable.TransformMultiplier = additionalData.transformMultiplier;
						if (addToUndo)
						{
							AddSelectionChangeUndo(selectable, true);
						}
					}
				}
				else if (!noExtraList.Contains(selectable))
				{
					entityList.Add(selectable);
				}
			});
		}
		Deselect(entityList, addToUndo);
	}

	public virtual void Deselect(List<ISelectable> entities, bool addToUndo)
	{
		if (entities.Count == 0)
		{
			return;
		}
		BeforeSelectionChanged();
		bool flag = false;
		int count = entities.Count;
		for (int num = SelectedObjects.Count - 1; num >= 0; num--)
		{
			long identifier = SelectedObjects[num].identifier;
			for (int i = 0; i < count; i++)
			{
				ISelectable selectable = entities[i];
				if (selectable.identifier == identifier)
				{
					if (addToUndo)
					{
						AddSelectionChangeUndo(selectable, false);
					}
					RemoveSelectionAt(num);
					flag = true;
					break;
				}
			}
		}
		if (addToUndo)
		{
			StoreSelectionChangeUndos();
		}
		else
		{
			CleanupSelection();
		}
		if (flag && !IsDragging)
		{
			SelectionChanged();
		}
	}

	protected virtual void RemoveSelectionAt(int index)
	{
		SelectedObjects[index].Select(false);
		SelectedObjects.RemoveAt(index);
	}

	public virtual void CleanupSelection()
	{
	}

	public virtual void SelectAll(bool addToUndo)
	{
		Select(new List<ISelectable>(ReferenceMaster.Selectables), false, addToUndo);
	}

	public virtual void DeselectAll(bool addToUndo, bool autoFlush = true)
	{
		if (SelectedObjects.Count == 0)
		{
			return;
		}
		BeforeSelectionChanged();
		float num = dragWaited;
		dragWaited = 10f;
		for (int num2 = SelectedObjects.Count - 1; num2 >= 0; num2--)
		{
			ISelectable entity = SelectedObjects[num2];
			if (addToUndo)
			{
				AddSelectionChangeUndo(entity, false);
			}
			RemoveFromSelection(entity);
		}
		if (addToUndo && autoFlush)
		{
			StoreSelectionChangeUndos();
		}
		bool flag = InputManager.LeftMouseButtonHeld();
		if (_startDragging && flag && movedMouse)
		{
			dragWaited = num;
		}
		else
		{
			dragWaited = 0f;
		}
		SelectionChanged();
	}

	protected virtual bool MultiSelect()
	{
		return InputManager.AdvancedBuilding.LeftShiftKey();
	}

	protected virtual bool FocusKey()
	{
		return InputManager.LevelEditor.FocusLevelSelectionKey();
	}

	protected virtual void Update()
	{
		if (!isInitialized || !active)
		{
			return;
		}
		if (StatMaster.levelSimulating)
		{
			if (image.enabled)
			{
				image.enabled = false;
				_startDragging = false;
			}
			return;
		}
		ProcessDragSelecting();
		if (FocusKey())
		{
			FocusOnSelection();
		}
		else if (InputManager.CloseKey() && InputManager.ToCloseCount <= 0 && !StatMaster.inMenu)
		{
			DeselectAll(true);
		}
	}

	protected virtual void ProcessDragSelecting(bool AddToUndo = true)
	{
		bool flag = InputManager.LeftMouseButton();
		bool flag2 = InputManager.LeftMouseButtonHeld();
		bool flag3 = InputManager.LeftMouseButtonReleased();
		Vector2 vector = InputManager.CursorPosition();
		if (StatMaster.Mode.isTranslating || StatMaster.Mode.isRotating || StatMaster.Mode.isScaling)
		{
			return;
		}
		if (flag && CanDrag() && !StatMaster.hudOccluding && !StatMaster.gizmoOccluding)
		{
			downPos = vector;
			lastPos = downPos;
			_startDragging = true;
			DragSelection.Clear();
			DragSelectionIds.Clear();
			isMultiSelect = MultiSelect();
			once = true;
			movedMouse = false;
			dragWaited = 0f;
		}
		if (_startDragging && flag2 && (movedMouse || Vector2.SqrMagnitude(vector - downPos) > Mathf.Pow((float)Screen.height / 80f, 2f)))
		{
			movedMouse = true;
			if (!CanDrag())
			{
				Hide();
				return;
			}
			float num = ((Time.timeScale != 0f) ? (Time.deltaTime / Time.timeScale) : Time.unscaledDeltaTime);
			if (dragWaited + num <= 0.12f)
			{
				dragWaited += num;
				return;
			}
			if (once)
			{
				dragWaited += num;
				once = false;
				StartDragSelection();
			}
			BeforeSelectionChanged();
			if (lastPos != vector)
			{
				Vector2 vector2 = vector - downPos;
				if (vector2.sqrMagnitude > 25f)
				{
					if (!image.enabled && !isMultiSelect && !isSubtractiveSelect)
					{
						DeselectAll(true);
					}
					image.enabled = true;
					rectTransform.anchoredPosition = GetUIPosition(downPos);
					rectTransform.sizeDelta = GetUIPosition(new Vector2((!(vector2.x < 0f)) ? vector2.x : (0f - vector2.x), (!(vector2.y < 0f)) ? vector2.y : (0f - vector2.y)));
					rectTransform.localScale = new Vector3((!(vector2.x < 0f)) ? 1 : (-1), (!(vector2.y < 0f)) ? 1 : (-1), 1f);
					Dictionary<long, ISelectable> selectedObjects = GetSelectedObjects(downPos, vector);
					List<ISelectable> list = new List<ISelectable>();
					int count = DragSelection.Count;
					for (int num2 = count - 1; num2 >= 0; num2--)
					{
						ISelectable selectable = DragSelection[num2];
						long identifier = selectable.identifier;
						if (!selectedObjects.ContainsKey(identifier))
						{
							DragSelection.RemoveAt(num2);
							DragSelectionIds.Remove(identifier);
							list.Add(selectable);
						}
					}
					Deselect(list, false);
					list.Clear();
					foreach (KeyValuePair<long, ISelectable> item in selectedObjects)
					{
						long key = item.Key;
						ISelectable value = item.Value;
						if (!value.IsSelected && !DragSelectionIds.Contains(key))
						{
							DragSelection.Add(value);
							DragSelectionIds.Add(key);
							list.Add(value);
						}
					}
					Select(list, true, false, EmptyAdditionalList);
					SelectionChanged();
				}
				lastPos = vector;
			}
		}
		if (flag3 && _startDragging)
		{
			if (image.enabled && DragSelection.Count > 0)
			{
				Deselect(DragSelection, false);
				Select(DragSelection, isMultiSelect, AddToUndo);
				RecoverMissingDragSelection();
				DragSelection.Clear();
				DragSelectionIds.Clear();
			}
			bool isDragging = IsDragging;
			Hide();
			if (isDragging)
			{
				FinishDragSelection();
			}
		}
	}

	protected virtual void StartDragSelection()
	{
	}

	protected virtual void FinishDragSelection()
	{
	}

	protected virtual void RecoverMissingDragSelection()
	{
	}

	protected Vector2 GetUIPosition(Vector2 screenPos)
	{
		return new Vector2(screenPos.x * canvasScale.x, screenPos.y * canvasScale.y);
	}

	protected virtual Dictionary<long, ISelectable> GetSelectedObjects(Vector3 startPos, Vector3 endPos)
	{
		Vector3 min = Vector3.Min(startPos, endPos);
		Vector3 max = Vector3.Max(startPos, endPos);
		Bounds bounds = default(Bounds);
		bounds.SetMinMax(min, max);
		Camera main = Camera.main;
		Vector3 forward = main.transform.forward;
		Vector3 position = main.transform.position;
		Dictionary<long, ISelectable> dictionary = new Dictionary<long, ISelectable>();
		foreach (ISelectable selectable in ReferenceMaster.Selectables)
		{
			Vector3 center = selectable.GetCenter();
			float num = Vector3.Dot(forward, center - position);
			if (!(num <= 0f))
			{
				Vector2 vector = main.WorldToScreenPoint(center);
				if (bounds.Contains(vector))
				{
					dictionary.Add(selectable.identifier, selectable);
				}
			}
		}
		return dictionary;
	}

	protected void FocusOnSelection()
	{
		Vector3 position;
		if (Selection.Count > 0)
		{
			position = ToolTransform.position;
		}
		else
		{
			BlockMapper currentInstance = BlockMapper.CurrentInstance;
			if (currentInstance == null || !currentInstance.IsEntity)
			{
				return;
			}
			position = currentInstance.Entity.entity.Position;
		}
		MouseOrbit instance = SingleInstanceFindOnly<MouseOrbit>.Instance;
		if (instance != null)
		{
			instance.FocusOnTarget(position);
		}
	}
}
