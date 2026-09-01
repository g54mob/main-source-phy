using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapMarkerPlacer : MonoBehaviour
{
	public Camera mainCamera;

	public List<GameObject> markerPrefabs;

	private GameObject activeMarkerPrefab;

	public Canvas mapCanvas;

	public VirtualCursor virtualCursor;

	public List<InputActionReference> primaryClickActions;

	public List<InputActionReference> secondaryClickActions;

	public bool enableActionsOnEnable;

	public LayerMask mapPieceBlockingLayers;

	public bool enableHover;

	public bool logDebug;

	public bool logActiveMarkerOnAwake;

	private GameObject currentMarker;

	private MapMarkerLineUI currentMarkerUI;

	private Vector2 markerOriginLocal;

	private RectTransform mapRect;

	private readonly List<MapMarkerLineUI> placedMarkers;

	private MapMarkerHitTarget hoveredHitTarget;

	private static Action<MapMarkerLineUI> m_OnMarkerFinalized;

	private bool prevPrimaryPressed;

	private bool prevSecondaryPressed;

	private bool isDraggingMapPiece;

	private bool primaryHeld;

	public static event Action<MapMarkerLineUI> OnMarkerFinalized
	{
		add
		{
			//IL_003a: Expected I, but got O
			Delegate obj = MapMarkerPlacer.m_OnMarkerFinalized;
			Delegate obj4 = default(Delegate);
			while (true)
			{
				Delegate obj2 = Delegate.Combine(obj, value);
				bool flag = (object)obj2 == null;
				Delegate obj3 = obj2;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj3 == null)
					{
						break;
					}
				}
				nint num = (nint)typeof(MapMarkerPlacer);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj4 != obj;
				obj = obj4;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_003a: Expected I, but got O
			Delegate obj = MapMarkerPlacer.m_OnMarkerFinalized;
			Delegate obj4 = default(Delegate);
			while (true)
			{
				Delegate obj2 = Delegate.Remove(obj, value);
				bool flag = (object)obj2 == null;
				Delegate obj3 = obj2;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj3 == null)
					{
						break;
					}
				}
				nint num = (nint)typeof(MapMarkerPlacer);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj4 != obj;
				obj = obj4;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	private void Awake()
	{
		if (mainCamera == null)
		{
			Camera main = Camera.main;
			mainCamera = main;
		}
		string arg = default(string);
		object message = default(object);
		if (mainCamera != null)
		{
			if (mapCanvas != null)
			{
				if (virtualCursor != null)
				{
					Transform transform = mapCanvas.transform;
					if ((object)transform == null)
					{
						mapRect = null;
					}
					else
					{
						bool flag = (object)transform.GetType() != typeof(RectTransform);
						Transform transform2 = null;
						if (!flag)
						{
							transform2 = transform;
						}
						mapRect = (RectTransform)transform2;
						if ((object)transform.GetType() == typeof(RectTransform))
						{
							goto IL_02a4;
						}
					}
					if (mapRect != null)
					{
						EnsureActiveMarkerPrefabSelected();
						if (HasValidActivePrefab())
						{
							if (logActiveMarkerOnAwake)
							{
								if (activeMarkerPrefab != null)
								{
									string text = activeMarkerPrefab.name;
									arg = text;
								}
								else
								{
									arg = "<null>";
								}
								goto IL_02a4;
							}
							return;
						}
						Debug.LogError("MapMarkerPlacer: No valid active marker prefab.\nAssign 'Active Marker Prefab' OR add at least one prefab to 'Marker Prefabs'.", this);
					}
					else
					{
						Debug.LogError("MapMarkerPlacer: mapCanvas.transform is not a RectTransform.");
					}
					base.enabled = false;
					return;
				}
				message = "MapMarkerPlacer: Assign the VirtualCursor reference! This script requires it.";
			}
			else
			{
				message = "MapMarkerPlacer: Assign the mapCanvas reference!";
			}
		}
		else
		{
			message = "MapMarkerPlacer: No camera assigned and Camera.main is null. Assign a Camera in the inspector.";
		}
		goto IL_02be;
		IL_02be:
		Debug.LogError(message);
		base.enabled = false;
		return;
		IL_02a4:
		if (markerPrefabs == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg2 = default(object);
			string message2 = $"[MapMarkerPlacer] Awake activeMarkerPrefab='{arg}', libraryCount={arg2}";
			Debug.Log(message2, this);
			return;
		}
		goto IL_02be;
	}

	private void OnEnable()
	{
		if (enableActionsOnEnable)
		{
			EnableAll(primaryClickActions);
			EnableAll(secondaryClickActions);
		}
	}

	private void Update()
	{
		bool flag = IsAnyPressed(primaryClickActions);
		bool flag2 = IsAnyPressed(secondaryClickActions);
		if (flag)
		{
			if (!prevPrimaryPressed)
			{
				HandlePrimaryPressed();
			}
		}
		else if (~(prevPrimaryPressed ? 1u : 0u) == 0)
		{
			HandlePrimaryReleased();
		}
		if (flag2 && !prevSecondaryPressed)
		{
			HandleSecondaryPressed();
		}
		bool flag3 = !primaryHeld;
		prevPrimaryPressed = flag;
		prevSecondaryPressed = flag2;
		if (!flag3 && currentMarker != null && !isDraggingMapPiece)
		{
			Camera cameraForCanvas = GetCameraForCanvas();
			Vector2 vector = default(Vector2);
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(mapRect, vector, cameraForCanvas, out var localPoint))
			{
				if (currentMarkerUI == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
					MapMarkerLineUI mapMarkerLineUI = default(MapMarkerLineUI);
					currentMarkerUI = mapMarkerLineUI;
				}
				if (currentMarkerUI != null)
				{
					currentMarkerUI.UpdateLine(vector, localPoint, mapRect);
				}
			}
		}
		if (enableHover)
		{
			UpdateHover();
		}
	}

	public void SetActiveMarkerPrefabIndex(int index)
	{
		if (markerPrefabs != null)
		{
			List<GameObject> list = markerPrefabs;
			if (list._size != 0)
			{
				if (index >= 0)
				{
					int num = list._size - 1;
					if (index <= num)
					{
						num = index;
					}
				}
				else
				{
					int num = 0;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				UnityEngine.Object obj = default(UnityEngine.Object);
				if (obj != null)
				{
					activeMarkerPrefab = (GameObject)obj;
					if (logDebug)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						string arg = obj.name;
						object arg2 = default(object);
						string message = $"[MapMarkerPlacer] Active marker prefab set from library index {arg2} -> '{arg}'.";
						Debug.Log(message, this);
					}
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg3 = default(object);
					string message2 = $"MapMarkerPlacer: Marker Prefabs[{arg3}] is null; active prefab not changed.";
					Debug.LogWarning(message2, this);
				}
				return;
			}
		}
		Debug.LogWarning("MapMarkerPlacer: Cannot set active prefab index because Marker Prefabs list is empty.", this);
	}

	public void SetActiveMarkerPrefab(GameObject prefab)
	{
		if (prefab != null)
		{
			activeMarkerPrefab = prefab;
			if (logDebug)
			{
				string text = prefab.name;
				string message = "[MapMarkerPlacer] Active marker prefab set to '" + text + "'.";
				Debug.Log(message, this);
			}
		}
		else
		{
			Debug.LogWarning("MapMarkerPlacer: SetActiveMarkerPrefab called with null.", this);
		}
	}

	private unsafe void HandlePrimaryPressed()
	{
		//IL_0008: Expected O, but got Ref
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		//IL_0076: Expected O, but got Ref
		//IL_00ce: Expected O, but got Ref
		//IL_0224: Expected O, but got I
		//IL_0146: Expected O, but got Ref
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Expected Ref, but got Unknown
		//IL_02ae: Expected O, but got I
		//IL_02c1: Expected O, but got Ref
		//IL_02dc: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		primaryHeld = true;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		EnsureActiveMarkerPrefabSelected();
		string text2;
		UnityEngine.Object context;
		object message;
		if (activeMarkerPrefab != null)
		{
			object obj3 = this + 92;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
			object obj4 = default(object);
			if (obj4 != null)
			{
				Vector3 pos = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 121));
				_ = 0;
				Ray ray = mainCamera.ScreenPointToRay(pos);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
				ref RaycastHit hitInfo = ref System.Runtime.CompilerServices.Unsafe.As<object, RaycastHit>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 65));
				_ = ray.m_Origin;
				Ray ray2 = (Ray)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 121));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v587 @ rax_v64 (UnityEngine.Ray)+10]");
				_ = 0;
				int layerMask = default(int);
				if (Physics.Raycast(ray2, out hitInfo, 1f / 0f, layerMask))
				{
					isDraggingMapPiece = true;
					if (logDebug)
					{
						RaycastHit raycastHit = (RaycastHit)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 65));
						Collider collider = ((RaycastHit*)raycastHit)->collider;
						string text = collider.name;
						text2 = "[MapMarkerPlacer] Blocked by 3D collider: " + text;
						goto IL_04c8;
					}
					return;
				}
			}
			isDraggingMapPiece = false;
			Camera cameraForCanvas = GetCameraForCanvas();
			Vector2 vector = default(Vector2);
			if (RectTransformUtility.RectangleContainsScreenPoint(mapRect, vector, cameraForCanvas))
			{
				if (RectTransformUtility.ScreenPointToLocalPointInRectangle(mapRect, vector, cameraForCanvas, out System.Runtime.CompilerServices.Unsafe.As<object, Vector2>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 119))))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+77]");
					markerOriginLocal = (Vector2)0;
					Transform parent = mapCanvas.transform;
					GameObject gameObject = UnityEngine.Object.Instantiate(activeMarkerPrefab, parent);
					currentMarker = gameObject;
					if (currentMarker.TryGetComponent<RectTransform>(out System.Runtime.CompilerServices.Unsafe.As<object, RectTransform>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103))))
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+67]");
						((RectTransform)0).anchoredPosition = vector;
						Quaternion localRotation = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 121));
						_ = Quaternion.identityQuaternion;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+67]");
						((Transform)0).localRotation = localRotation;
					}
					if (!currentMarker.TryGetComponent<MapMarkerLineUI>(out *(MapMarkerLineUI*)(this + 112)))
					{
						Debug.LogWarning("MapMarkerPlacer: Marker prefab is missing MapMarkerLineUI.", this);
					}
					else
					{
						placedMarkers.Add(currentMarkerUI);
						MapMarkerLineUI mapMarkerLineUI = currentMarkerUI;
						mapMarkerLineUI._003COriginLocal_003Ek__BackingField = markerOriginLocal;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MapMarkerPlacer)+7C]");
						_ = 0;
						mapMarkerLineUI.placementTooltipFinalized = false;
						mapMarkerLineUI.minimumDistanceEventFired = false;
						mapMarkerLineUI._003CHasReachedMinimumDragDistance_003Ek__BackingField = false;
						mapMarkerLineUI.hasPreviousTipPosition = false;
						mapMarkerLineUI._003CNormalizedMarkerSpeed_003Ek__BackingField = 0f;
						mapMarkerLineUI.inspectorRawSpeedUnitsPerSecond = 0f;
						mapMarkerLineUI.UpdateLine(vector, vector, mapRect);
						MapMarkerLineUI mapMarkerLineUI2 = currentMarkerUI;
						if (mapMarkerLineUI2.placementTooltip != null)
						{
							MapMarkerLineUI mapMarkerLineUI3 = currentMarkerUI;
							mapMarkerLineUI3.placementTooltip.SetActive(value: true);
						}
					}
					if (!currentMarker.TryGetComponent<MapMarkerHitTarget>(out System.Runtime.CompilerServices.Unsafe.As<object, MapMarkerHitTarget>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 127))))
					{
						Debug.LogWarning("MapMarkerPlacer: Marker prefab is missing MapMarkerHitTarget. Hover and deletion will not work for this marker. Add MapMarkerHitTarget to the prefab root.", currentMarker);
					}
					if (logDebug)
					{
						string text3 = activeMarkerPrefab.name;
						text2 = "[MapMarkerPlacer] Marker placed. Prefab='" + text3 + "'.";
						goto IL_04c8;
					}
					return;
				}
				if (!logDebug)
				{
					return;
				}
				context = this;
				message = "[MapMarkerPlacer] Primary press ignored: failed to convert screen to local.";
			}
			else
			{
				if (!logDebug)
				{
					return;
				}
				context = this;
				message = "[MapMarkerPlacer] Primary press ignored: pointer not over map rect.";
			}
			goto IL_05a2;
		}
		Debug.LogWarning("MapMarkerPlacer: Primary press ignored because there is no valid active marker prefab.", this);
		return;
		IL_04c8:
		context = this;
		message = text2;
		goto IL_05a2;
		IL_05a2:
		Debug.Log(message, context);
	}

	private unsafe void HandlePrimaryReleased()
	{
		//IL_018c: Expected F4, but got Ref
		//IL_018c: Expected F4, but got Ref
		primaryHeld = false;
		if (!isDraggingMapPiece && currentMarker != null)
		{
			MapMarkerLineUI mapMarkerLineUI = default(MapMarkerLineUI);
			if (currentMarkerUI == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
				currentMarkerUI = mapMarkerLineUI;
			}
			if (currentMarkerUI != null)
			{
				MapMarkerLineUI mapMarkerLineUI2 = currentMarkerUI;
				mapMarkerLineUI2.hasPreviousTipPosition = false;
				if (mapMarkerLineUI2.resetSpeedOnFinalize)
				{
					mapMarkerLineUI2._003CNormalizedMarkerSpeed_003Ek__BackingField = 0f;
					mapMarkerLineUI2.inspectorRawSpeedUnitsPerSecond = 0f;
				}
				mapMarkerLineUI2.placementTooltipFinalized = true;
				if (mapMarkerLineUI2.placementTooltip != null)
				{
					mapMarkerLineUI2.placementTooltip.SetActive(value: false);
				}
				if (!mapMarkerLineUI2.placementEventFired)
				{
					mapMarkerLineUI2.placementEventFired = true;
					if (mapMarkerLineUI2.onPlacementFinalized != null)
					{
						object obj = default(object);
						mapMarkerLineUI2.onPlacementFinalized.Invoke((nint)(&obj), (nint)(&mapMarkerLineUI));
					}
				}
				Action<MapMarkerLineUI> onMarkerFinalized = MapMarkerPlacer.m_OnMarkerFinalized;
				if (MapMarkerPlacer.m_OnMarkerFinalized != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v329 @ rcx_v22 (System.Action`1<MapMarkerLineUI>)+18] (should have been resolved before IL gen)");
				}
			}
			if (logDebug)
			{
				Debug.Log("[MapMarkerPlacer] Marker finalized.", this);
			}
		}
		currentMarker = null;
		currentMarkerUI = null;
		isDraggingMapPiece = false;
	}

	private void HandleSecondaryPressed()
	{
		//IL_02d2: Expected O, but got I4
		Camera cameraForCanvas = GetCameraForCanvas();
		Vector2 vector = default(Vector2);
		UnityEngine.Object context;
		object message2;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(mapRect, vector, cameraForCanvas, out var localPoint))
		{
			List<MapMarkerLineUI> list = placedMarkers;
			bool flag = (nint)placedMarkers < 0;
			int num = list._size - 1;
			MapMarkerHitTarget component = null;
			if (!flag)
			{
				UnityEngine.Object obj = default(UnityEngine.Object);
				Vector2 markerOriginLocalOnMap = default(Vector2);
				float markerDistanceLocalUnits = default(float);
				float markerAngleDegrees = default(float);
				object obj3;
				do
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					bool flag3;
					if ((bool)obj)
					{
						bool flag2 = ((Component)obj).TryGetComponent(out component);
						flag3 = (flag2 ? 1 : 0) < (false ? 1 : 0);
						if (flag2)
						{
							bool flag4 = component.HitTest(vector, cameraForCanvas, localPoint, markerOriginLocalOnMap, markerDistanceLocalUnits, markerAngleDegrees);
							flag3 = (flag4 ? 1 : 0) < (false ? 1 : 0);
							if (flag4)
							{
								if (logDebug)
								{
									string text = obj.name;
									string message = "[MapMarkerPlacer] Deleting marker under pointer: " + text;
									Debug.Log(message, this);
								}
								if (hoveredHitTarget == component)
								{
									MapMarkerHitTarget mapMarkerHitTarget = hoveredHitTarget;
									if (mapMarkerHitTarget._003CIsHovered_003Ek__BackingField)
									{
										mapMarkerHitTarget._003CIsHovered_003Ek__BackingField = false;
										if (mapMarkerHitTarget.onHoverExit != null)
										{
											mapMarkerHitTarget.onHoverExit.Invoke();
										}
									}
									hoveredHitTarget = null;
								}
								placedMarkers.RemoveAt(num);
								GameObject obj2 = ((Component)obj).gameObject;
								UnityEngine.Object.Destroy(obj2);
								return;
							}
						}
					}
					else
					{
						flag3 = (nint)placedMarkers < 0;
						placedMarkers.RemoveAt(num);
					}
					num--;
					obj3 = !flag3;
				}
				while (obj3 != null);
			}
			if (!logDebug)
			{
				return;
			}
			context = this;
			message2 = "[MapMarkerPlacer] Secondary press: no marker under pointer.";
		}
		else
		{
			if (!logDebug)
			{
				return;
			}
			context = this;
			message2 = "[MapMarkerPlacer] Secondary press ignored: failed to convert screen to local.";
		}
		Debug.Log(message2, context);
	}

	private void UpdateHover()
	{
		//IL_0318: Expected O, but got I4
		Camera cameraForCanvas = GetCameraForCanvas();
		Vector2 vector = default(Vector2);
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(mapRect, vector, cameraForCanvas, out var localPoint))
		{
			List<MapMarkerLineUI> list = placedMarkers;
			bool flag = (nint)placedMarkers < 0;
			int num = list._size - 1;
			UnityEngine.Object obj = null;
			MapMarkerHitTarget component = null;
			if (!flag)
			{
				UnityEngine.Object obj2 = default(UnityEngine.Object);
				Vector2 markerOriginLocalOnMap = default(Vector2);
				float markerDistanceLocalUnits = default(float);
				float markerAngleDegrees = default(float);
				while (true)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					bool flag3;
					if ((bool)obj2)
					{
						bool flag2 = ((Component)obj2).TryGetComponent(out component);
						flag3 = (flag2 ? 1 : 0) < (false ? 1 : 0);
						if (flag2)
						{
							bool flag4 = component.HitTest(vector, cameraForCanvas, localPoint, markerOriginLocalOnMap, markerDistanceLocalUnits, markerAngleDegrees);
							flag3 = (flag4 ? 1 : 0) < (false ? 1 : 0);
							if (flag4)
							{
								obj = component;
								break;
							}
						}
					}
					else
					{
						flag3 = (nint)placedMarkers < 0;
						placedMarkers.RemoveAt(num);
					}
					num--;
					object obj3 = !flag3;
					if (obj3 == null)
					{
						obj = null;
						break;
					}
				}
			}
			if (!(obj != hoveredHitTarget))
			{
				return;
			}
			if (hoveredHitTarget != null)
			{
				MapMarkerHitTarget mapMarkerHitTarget = hoveredHitTarget;
				if (mapMarkerHitTarget._003CIsHovered_003Ek__BackingField)
				{
					mapMarkerHitTarget._003CIsHovered_003Ek__BackingField = false;
					if (mapMarkerHitTarget.onHoverExit != null)
					{
						mapMarkerHitTarget.onHoverExit.Invoke();
					}
				}
			}
			hoveredHitTarget = (MapMarkerHitTarget)obj;
			if (!(hoveredHitTarget != null))
			{
				return;
			}
			MapMarkerHitTarget mapMarkerHitTarget2 = hoveredHitTarget;
			if (!mapMarkerHitTarget2._003CIsHovered_003Ek__BackingField)
			{
				mapMarkerHitTarget2._003CIsHovered_003Ek__BackingField = true;
				if (mapMarkerHitTarget2.onHoverEnter != null)
				{
					mapMarkerHitTarget2.onHoverEnter.Invoke();
				}
			}
		}
		else
		{
			if (!(hoveredHitTarget != null))
			{
				return;
			}
			MapMarkerHitTarget mapMarkerHitTarget3 = hoveredHitTarget;
			if (mapMarkerHitTarget3._003CIsHovered_003Ek__BackingField)
			{
				mapMarkerHitTarget3._003CIsHovered_003Ek__BackingField = false;
				if (mapMarkerHitTarget3.onHoverExit != null)
				{
					mapMarkerHitTarget3.onHoverExit.Invoke();
				}
			}
			hoveredHitTarget = null;
		}
	}

	private void ClearHover()
	{
		if (!(hoveredHitTarget != null))
		{
			return;
		}
		MapMarkerHitTarget mapMarkerHitTarget = hoveredHitTarget;
		if (mapMarkerHitTarget._003CIsHovered_003Ek__BackingField)
		{
			mapMarkerHitTarget._003CIsHovered_003Ek__BackingField = false;
			if (mapMarkerHitTarget.onHoverExit != null)
			{
				mapMarkerHitTarget.onHoverExit.Invoke();
			}
		}
		hoveredHitTarget = null;
	}

	private void EnsureActiveMarkerPrefabSelected()
	{
		if (!(activeMarkerPrefab == null) || markerPrefabs == null)
		{
			return;
		}
		List<GameObject> list = markerPrefabs;
		if (list._size <= 0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
		UnityEngine.Object obj = default(UnityEngine.Object);
		if (obj != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			GameObject gameObject = default(GameObject);
			activeMarkerPrefab = gameObject;
			if (logActiveMarkerOnAwake || logDebug)
			{
				Debug.LogWarning("[MapMarkerPlacer] Active Marker Prefab was null; fell back to Marker Prefabs[0]. Assign Active Marker Prefab explicitly to guarantee startup selection.", this);
			}
		}
	}

	private bool HasValidActivePrefab()
	{
		return activeMarkerPrefab != null;
	}

	private void EnableAll(List<InputActionReference> list)
	{
		if (list == null)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<InputActionReference>.Enumerator enumerator = default(List<InputActionReference>.Enumerator);
		UnityEngine.Object obj = default(UnityEngine.Object);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (!(obj != null))
				{
					continue;
				}
				if ((object)obj != null)
				{
					InputAction action = ((InputActionReference)obj).action;
					if (action != null)
					{
						InputAction action2 = ((InputActionReference)obj).action;
						if (action2 == null)
						{
							break;
						}
						if (!action2.enabled)
						{
							InputAction action3 = ((InputActionReference)obj).action;
							action3.Enable();
						}
					}
					continue;
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	private bool IsAnyPressed(List<InputActionReference> list)
	{
		if (list != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			List<InputActionReference>.Enumerator enumerator = default(List<InputActionReference>.Enumerator);
			UnityEngine.Object obj = default(UnityEngine.Object);
			while (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (!(obj != null))
				{
					continue;
				}
				if ((object)obj != null)
				{
					InputAction action = ((InputActionReference)obj).action;
					if (action == null)
					{
						continue;
					}
					InputAction action2 = ((InputActionReference)obj).action;
					if (action2 != null)
					{
						if (action2.enabled)
						{
							InputAction action3 = ((InputActionReference)obj).action;
							if (action3.IsPressed())
							{
								enumerator.Dispose();
								return true;
							}
						}
						continue;
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			enumerator.Dispose();
		}
		return false;
	}

	private Camera GetCameraForCanvas()
	{
		//IL_0081: Expected O, but got I4
		if (!(mapCanvas != null))
		{
			goto IL_012b;
		}
		if ((object)mapCanvas != null)
		{
			RenderMode renderMode = mapCanvas.renderMode;
			if (renderMode == RenderMode.ScreenSpaceOverlay)
			{
				return null;
			}
			object obj = renderMode - 1;
			if ((nint)obj > 1)
			{
				goto IL_012b;
			}
			if ((object)mapCanvas != null)
			{
				Camera worldCamera = mapCanvas.worldCamera;
				if (!(worldCamera != null))
				{
					goto IL_012b;
				}
				if ((object)mapCanvas != null)
				{
					return mapCanvas.worldCamera;
				}
			}
		}
		return (Camera)(object)new NullReferenceException();
		IL_012b:
		return mainCamera;
	}

	public MapMarkerPlacer()
	{
		List<GameObject> list = new List<GameObject>();
		markerPrefabs = list;
		primaryClickActions = new List<InputActionReference>();
		secondaryClickActions = new List<InputActionReference>();
		enableActionsOnEnable = true;
		enableHover = true;
		placedMarkers = new List<MapMarkerLineUI>();
		base._002Ector();
	}
}
