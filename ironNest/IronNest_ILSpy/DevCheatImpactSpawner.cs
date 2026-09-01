using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.InputSystem;

public class DevCheatImpactSpawner : MonoBehaviour
{
	private bool cheatEnabled = true;

	private GameObject impactEffectPrefab;

	private ShellDefinition emulatedShellDefinition;

	private Canvas mapCanvas;

	private RectTransform mapRect;

	private RectTransform standardImpactSpawnContainer;

	private VirtualCursor virtualCursor;

	private InputActionReference spawnImpactAction;

	private bool enableActionOnEnable = true;

	private bool initializeImpactEffectPayload;

	private int payloadDamage = 25;

	private float payloadArmorPenetration = 0.1f;

	private float payloadImpactRadius = 2f;

	private float payloadKnockbackForce = 50f;

	private bool debugLogs;

	private void Awake()
	{
		if (!(mapRect == null) || !(mapCanvas != null))
		{
			return;
		}
		Transform transform = mapCanvas.transform;
		if ((object)transform == null)
		{
			mapRect = null;
			return;
		}
		bool flag = (object)transform.GetType() != typeof(RectTransform);
		Transform transform2 = null;
		if (!flag)
		{
			transform2 = transform;
		}
		mapRect = (RectTransform)transform2;
		if ((object)transform.GetType() != typeof(RectTransform))
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Warning: Method ends with non empty stack (-28), the output could be wrong!");
		/*Error: End of method reached without returning.*/;
	}

	private void OnEnable()
	{
		if (!enableActionOnEnable || !(spawnImpactAction != null))
		{
			return;
		}
		InputAction action = spawnImpactAction.action;
		if (action != null)
		{
			InputAction action2 = spawnImpactAction.action;
			if (!action2.enabled)
			{
				InputAction action3 = spawnImpactAction.action;
				action3.Enable();
			}
		}
	}

	private void Update()
	{
		if (!cheatEnabled || !(spawnImpactAction != null))
		{
			return;
		}
		InputAction action = spawnImpactAction.action;
		if (action == null)
		{
			return;
		}
		InputAction action2 = spawnImpactAction.action;
		if (action2.enabled)
		{
			InputAction action3 = spawnImpactAction.action;
			if (action3.WasPerformedThisFrame())
			{
				TrySpawnImpactAtCursor();
			}
		}
	}

	private unsafe void TrySpawnImpactAtCursor()
	{
		//IL_0008: Expected O, but got Ref
		//IL_01b7: Expected O, but got Ref
		//IL_01f2: Expected O, but got Ref
		//IL_0250: Expected O, but got Ref
		//IL_026c: Expected O, but got I
		//IL_02dd: Expected O, but got Ref
		//IL_02f7: Expected O, but got I
		//IL_0577: Expected O, but got Ref
		//IL_0592: Expected O, but got I
		//IL_05a5: Expected I, but got O
		//IL_05b3: Expected O, but got Ref
		//IL_05eb: Expected O, but got I
		//IL_02ad: Expected O, but got Ref
		//IL_030a: Expected O, but got Ref
		//IL_032b: Expected O, but got I
		//IL_0390: Expected O, but got I
		//IL_05fe: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		if (impactEffectPrefab != null)
		{
			if (mapCanvas != null && mapRect != null)
			{
				if (standardImpactSpawnContainer != null)
				{
					if (virtualCursor != null)
					{
						Camera cam;
						if (mapCanvas != null && mapCanvas.renderMode != RenderMode.ScreenSpaceOverlay)
						{
							Camera worldCamera = mapCanvas.worldCamera;
							cam = worldCamera;
						}
						else
						{
							cam = null;
						}
						Vector2 screenPoint = default(Vector2);
						UnityEngine.Object context;
						object message;
						if (RectTransformUtility.RectangleContainsScreenPoint(mapRect, screenPoint, cam))
						{
							if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(mapRect, screenPoint, cam, out System.Runtime.CompilerServices.Unsafe.As<object, Vector2>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103))))
							{
								if (debugLogs)
								{
									Debug.LogWarning("[DevCheatImpactSpawner] Spawn blocked: failed screen->map local conversion.", this);
								}
								return;
							}
							Vector3 position = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
							_ = 0;
							Vector3 vector = mapRect.TransformPoint(position);
							_ = vector.x;
							Vector3 position2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
							_ = vector.z;
							Vector3 vector2 = standardImpactSpawnContainer.InverseTransformPoint(position2);
							_ = vector2.x;
							_ = vector2.z;
							GameObject gameObject = UnityEngine.Object.Instantiate(impactEffectPrefab, standardImpactSpawnContainer);
							object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 119));
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+77]");
							if (!((UnityEngine.Object)0 != null))
							{
								Transform transform = gameObject.transform;
								_ = vector.x;
								Vector3 position3 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
								_ = vector.z;
								transform.position = position3;
							}
							else
							{
								_ = 0;
								Vector3 localPosition = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+77]");
								((Transform)0).localPosition = localPosition;
								Quaternion localRotation = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
								_ = Quaternion.identityQuaternion;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+77]");
								((Transform)0).localRotation = localRotation;
								nint num = (nint)typeof(Vector3);
								Vector3 localScale = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1091 @ rax_v77 (Il2CppClass<UnityEngine.Vector3>)+B8]");
								nint num2 = 0;
								_ = Vector3.oneVector;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1092 @ rax_v78 (Il2CppStaticFields<UnityEngine.Vector3>)+14]");
								_ = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+77]");
								((Transform)0).localScale = localScale;
							}
							object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 119));
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806D9540");
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+77]");
							bool flag = (UnityEngine.Object)0 != null;
							if (!flag)
							{
								if (debugLogs == flag)
								{
									return;
								}
								Debug.LogWarning("[DevCheatImpactSpawner] Spawned prefab does not contain ImpactLocation. It will NOT report to LocalSpaceEventLogger using your standard pipeline.", gameObject);
							}
							else
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+77]");
								((ImpactLocation)0).Init(emulatedShellDefinition);
							}
							if (!debugLogs)
							{
								return;
							}
							object arg;
							if (emulatedShellDefinition != null)
							{
								ShellDefinition shellDefinition = emulatedShellDefinition;
								arg = shellDefinition.DisplayName;
							}
							else
							{
								arg = "None";
							}
							object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
							_ = 0;
							object arg2 = (Vector3)obj5;
							string text = $"[DevCheatImpactSpawner] Spawned cheat impact at containerLocal={arg2} shell={arg}";
							context = gameObject;
							message = text;
						}
						else
						{
							if (!debugLogs)
							{
								return;
							}
							context = this;
							message = "[DevCheatImpactSpawner] Spawn ignored: cursor not over mapRect.";
						}
						Debug.Log(message, context);
					}
					else if (debugLogs)
					{
						Debug.LogWarning("[DevCheatImpactSpawner] Blocked: virtualCursor not assigned.", this);
					}
				}
				else if (debugLogs)
				{
					Debug.LogWarning("[DevCheatImpactSpawner] Blocked: standardImpactSpawnContainer not assigned (must match standard impact parent).", this);
				}
			}
			else if (debugLogs)
			{
				Debug.LogWarning("[DevCheatImpactSpawner] Blocked: mapCanvas/mapRect not assigned.", this);
			}
		}
		else if (debugLogs)
		{
			Debug.LogWarning("[DevCheatImpactSpawner] Blocked: impactEffectPrefab is not assigned.", this);
		}
	}

	private static Camera GetCameraForCanvas(Canvas canvas)
	{
		if (canvas != null)
		{
			if ((object)canvas == null)
			{
				return (Camera)(object)new NullReferenceException();
			}
			if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
			{
				return canvas.worldCamera;
			}
		}
		return null;
	}
}
