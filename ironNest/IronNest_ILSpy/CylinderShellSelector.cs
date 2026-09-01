using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class CylinderShellSelector : MonoBehaviour
{
	public ShellSlotPool.ShellSlotSides ShellSlotSide;

	public GameObject[] shellPrefabs;

	public List<GameObject> bullets;

	public Transform[] slots;

	public ArtilleryReloadController artilleryReloadController;

	public Animator animator;

	public LookAtTarget loadButton;

	public string loadStateKey;

	public LookAtTarget moveButton;

	public List<string> moveStateKeys;

	public bool invertRotationDirection;

	public bool rotateShellPrefabsWithCylinder;

	public bool debugLogs;

	public GameObject lastLoadedShellPrefab;

	public UnityEvent onShellDeployedByPlayer;

	private string lastStateKey;

	private bool lastSlotAHasShell;

	private bool lastMoveButtonActive;

	private bool _initialized;

	public int SlotCount
	{
		get
		{
			if (slots != null)
			{
				Transform[] array = slots;
				return array.Length;
			}
			return 0;
		}
	}

	private void Start()
	{
		//IL_0082: Expected O, but got I4
		//IL_0110: Expected O, but got I4
		//IL_01a0: Expected O, but got I4
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Expected O, but got Unknown
		//IL_01e3: Expected O, but got I4
		//IL_01e8: Expected I, but got O
		if (!_initialized)
		{
			InitializeFromShellPrefabs(shellPrefabs);
			_initialized = true;
		}
		if (loadButton != null)
		{
			UnityAction unityAction = OnLoadButtonClicked;
			bool flag = (object)loadButton == null;
			UnityAction unityAction2 = unityAction;
			object obj = 0;
			nint num = 0;
			if (flag)
			{
				goto IL_0233;
			}
			loadButton.RegisterOnClickDown(unityAction);
		}
		if (moveButton != null)
		{
			UnityAction unityAction3 = OnMoveButtonClicked;
			bool flag2 = (object)moveButton == null;
			UnityAction unityAction2 = unityAction3;
			object obj = 0;
			nint num = 0;
			if (flag2)
			{
				goto IL_0233;
			}
			moveButton.RegisterOnClickDown(unityAction3);
		}
		if (artilleryReloadController != null)
		{
			UnityAction unityAction2 = (UnityAction)(object)artilleryReloadController;
			Action<ReloadStateDef> b = HandleReloadStateChanged;
			bool flag3 = (object)artilleryReloadController == null;
			object obj = 0;
			nint num = 0;
			if (flag3)
			{
				goto IL_0233;
			}
			object obj2 = artilleryReloadController + 120;
			Delegate obj3 = (Delegate)(object)((MulticastDelegate)unityAction2).delegates;
			bool flag6;
			Delegate obj6 = default(Delegate);
			do
			{
				Delegate obj4 = Delegate.Combine(obj3, b);
				bool flag4 = (object)obj4 == null;
				Delegate obj5 = obj4;
				if (!flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					bool flag5 = (object)obj5 == null;
					unityAction2 = (UnityAction)obj4;
					obj = 0;
					num = unchecked((nint)null);
					if (flag5)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
						return;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				flag6 = (object)obj6 != obj3;
				obj3 = obj6;
			}
			while (flag6);
		}
		UpdateButtonActives(force: true);
		return;
		IL_0233:
		throw new NullReferenceException();
	}

	private void OnDestroy()
	{
		//IL_00ca: Expected O, but got I
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Expected O, but got Unknown
		//IL_00ea: Expected O, but got I
		if (!(artilleryReloadController != null))
		{
			return;
		}
		Delegate obj = (Delegate)(object)artilleryReloadController;
		Action<ReloadStateDef> value = HandleReloadStateChanged;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v147 @ rdi_v5 (System.Delegate)+78]");
		Delegate obj2 = (Delegate)0;
		object obj3 = obj + 120;
		Delegate obj5 = default(Delegate);
		while (true)
		{
			Delegate obj4 = Delegate.Remove(obj2, value);
			if ((object)obj4 != null)
			{
				((CylinderShellSelector)(object)obj4).HandleReloadStateChanged((ReloadStateDef)(object)typeof(Action<ReloadStateDef>));
				if ((object)obj4 == null)
				{
					break;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
			bool flag = (object)obj5 != obj2;
			obj2 = obj5;
			if (!flag)
			{
				return;
			}
		}
		IntPtr intPtr = default(IntPtr);
		((CylinderShellSelector)(object)obj).HandleReloadStateChanged((ReloadStateDef)(nint)intPtr);
	}

	public void RefreshUI()
	{
		UpdateButtonActives(force: true);
	}

	public void EnsureInitialized()
	{
		if (!_initialized)
		{
			InitializeFromShellPrefabs(shellPrefabs);
			_initialized = true;
		}
	}

	private unsafe void InitializeFromShellPrefabs(GameObject[] prefabs)
	{
		//IL_0018: Expected O, but got I4
		//IL_0021: Expected O, but got I4
		//IL_002a: Expected O, but got I4
		//IL_0197: Expected O, but got I4
		//IL_01a0: Expected O, but got I4
		//IL_01a9: Expected O, but got I4
		//IL_0045: Expected O, but got I
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Expected O, but got Unknown
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Expected O, but got Unknown
		//IL_008f: Expected O, but got I
		//IL_00c4: Expected O, but got I
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Expected O, but got Unknown
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Expected O, but got Unknown
		//IL_01fc: Expected O, but got I
		//IL_00e8: Expected I, but got O
		//IL_0260: Expected O, but got I
		//IL_0289: Expected O, but got Ref
		//IL_02ae: Expected O, but got Ref
		if (slots == null)
		{
			return;
		}
		Transform[] array = slots;
		object obj = 32;
		object obj2 = 0;
		object obj3 = 0;
		Transform[] array2;
		while (true)
		{
			array2 = slots;
			if ((nint)obj3 >= array.Length)
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v140 @ r15_v3+v170 @ rdi_v3 (UnityEngine.Transform[])]");
			if ((bool)(UnityEngine.Object)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v140 @ r15_v3+v170 @ rdi_v3 (UnityEngine.Transform[])]");
				bool flag = (nint)0 < (nint)0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v140 @ r15_v3+v170 @ rdi_v3 (UnityEngine.Transform[])]");
				int childCount = ((Transform)0).childCount;
				int num = childCount - 1;
				if (!flag)
				{
					do
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v140 @ r15_v3+v170 @ rdi_v3 (UnityEngine.Transform[])]");
						Transform child = ((Transform)0).GetChild(num);
						GameObject obj4 = child.gameObject;
						nint num2 = (nint)typeof(UnityEngine.Object);
						UnityEngine.Object.Destroy(obj4);
						num--;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v629 @ rcx_v38 (Il2CppClass<UnityEngine.Object>)+E4]");
					}
					while ((nint)0 >= (nint)0);
				}
			}
			array = slots;
			obj2++;
			obj += 8;
			obj3 = obj2;
		}
		List<GameObject> list = new List<GameObject>(array2.Length);
		bullets = list;
		Transform[] array3 = slots;
		object obj5 = 0;
		object obj6 = 32;
		object obj7 = 0;
		Vector3 zeroVector = default(Vector3);
		Quaternion quaternion = default(Quaternion);
		bool flag4;
		do
		{
			if ((nint)obj7 < array3.Length)
			{
				UnityEngine.Object obj8;
				if (prefabs != null && (nint)obj5 < prefabs.Length)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [prefabs @ rdx (UnityEngine.GameObject[])+v319 @ r13_v5]");
					obj8 = (UnityEngine.Object)0;
				}
				else
				{
					obj8 = null;
				}
				bool flag2 = obj8 != null;
				bool flag3 = !flag2;
				GameObject item = null;
				if (!flag3)
				{
					Transform[] array4 = slots;
					UnityEngine.Object original = obj8;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v326 @ rax_v20 (UnityEngine.Transform[])+v319 @ r13_v5]");
					GameObject gameObject = UnityEngine.Object.Instantiate((GameObject)original, (Transform)0);
					Transform transform = gameObject.transform;
					transform.localPosition = (Vector3)(&zeroVector);
					Transform transform2 = gameObject.transform;
					transform2.localRotation = (Quaternion)(&quaternion);
					zeroVector = Vector3.zeroVector;
					item = gameObject;
				}
				bullets.Add(item);
				array3 = slots;
				obj5++;
				obj6 += 8;
				flag4 = slots != null;
				obj7 = obj5;
				continue;
			}
			lastLoadedShellPrefab = null;
			return;
		}
		while (flag4);
		throw new NullReferenceException();
	}

	public void ReplaceAllShells(GameObject[] newShellPrefabs, bool setAsDesignTimeTemplate = true)
	{
		//IL_00de: Expected O, but got I4
		//IL_00e7: Expected O, but got I4
		//IL_00f0: Expected O, but got I4
		//IL_011a: Expected O, but got I
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Expected O, but got Unknown
		//IL_02cb: Expected O, but got I4
		//IL_02d4: Expected O, but got I4
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Expected O, but got Unknown
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Expected O, but got Unknown
		//IL_0164: Expected O, but got I
		//IL_0335: Expected O, but got I4
		//IL_0199: Expected O, but got I
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Expected O, but got Unknown
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Expected O, but got Unknown
		//IL_01bd: Expected I, but got O
		//IL_0327: Expected O, but got I
		if (slots == null)
		{
			return;
		}
		if (bullets != null)
		{
			List<GameObject> list = bullets;
			int num = 0;
			UnityEngine.Object obj = default(UnityEngine.Object);
			UnityEngine.Object obj2 = default(UnityEngine.Object);
			for (int num2 = 0; num2 < list._size; num2 = num)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					UnityEngine.Object.Destroy(obj2);
				}
				list = bullets;
				num++;
			}
		}
		Transform[] array = slots;
		object obj3 = 32;
		object obj4 = 0;
		object obj5 = 0;
		bool flag2;
		do
		{
			if ((nint)obj5 < array.Length)
			{
				Transform[] array2 = slots;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v275 @ r15_v5+v311 @ rax_v21 (UnityEngine.Transform[])]");
				if ((bool)(UnityEngine.Object)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v275 @ r15_v5+v311 @ rax_v21 (UnityEngine.Transform[])]");
					bool flag = (nint)0 < (nint)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v275 @ r15_v5+v311 @ rax_v21 (UnityEngine.Transform[])]");
					int childCount = ((Transform)0).childCount;
					int num3 = childCount - 1;
					if (!flag)
					{
						do
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v275 @ r15_v5+v311 @ rax_v21 (UnityEngine.Transform[])]");
							Transform child = ((Transform)0).GetChild(num3);
							GameObject obj6 = child.gameObject;
							nint num4 = (nint)typeof(UnityEngine.Object);
							UnityEngine.Object.Destroy(obj6);
							num3--;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v722 @ rcx_v25 (Il2CppClass<UnityEngine.Object>)+E4]");
						}
						while ((nint)0 >= (nint)0);
					}
				}
				array = slots;
				obj4++;
				obj3 += 8;
				flag2 = slots != null;
				obj5 = obj4;
				continue;
			}
			InitializeFromShellPrefabs(newShellPrefabs);
			if (setAsDesignTimeTemplate)
			{
				Transform[] array3 = slots;
				GameObject[] array4 = new GameObject[array3.Length];
				Transform[] array5 = slots;
				object obj7 = array4 + 32;
				object obj8 = (object)newShellPrefabs - (object)array4;
				object obj9 = 0;
				object obj10 = 0;
				while ((nint)obj10 < array5.Length)
				{
					object obj11;
					if (newShellPrefabs != null && (nint)obj9 < newShellPrefabs.Length)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v277 @ r15_v9+v282 @ r14_v9]");
						obj11 = 0;
					}
					else
					{
						obj11 = 0;
					}
					obj7 = obj11;
					array5 = slots;
					obj9++;
					obj7 += 8;
					bool flag3 = slots != null;
					obj10 = obj9;
					if (!flag3)
					{
						goto end_IL_0417;
					}
				}
				shellPrefabs = array4;
			}
			_initialized = true;
			UpdateButtonActives(force: true);
			return;
			continue;
			end_IL_0417:
			break;
		}
		while (flag2);
		throw new NullReferenceException();
	}

	private void HandleReloadStateChanged(ReloadStateDef state)
	{
		UpdateButtonActives(force: true);
	}

	private void UpdateButtonActives(bool force = false)
	{
		//IL_0028: Expected I4, but got O
		//IL_0030: Expected O, but got I
		//IL_0066: Expected I4, but got O
		//IL_006e: Expected O, but got I
		//IL_0113: Expected O, but got I4
		//IL_00ea: Expected O, but got I4
		//IL_0386: Expected I, but got O
		//IL_0233: Expected I4, but got O
		//IL_0239: Expected O, but got I
		//IL_0175: Expected O, but got I4
		//IL_01d7: Expected O, but got I4
		//IL_01fe: Expected O, but got I4
		//IL_0206: Expected I4, but got O
		//IL_020c: Expected O, but got I
		bool flag = loadButton != null;
		bool flag2 = !flag;
		UnityEngine.Object obj = default(UnityEngine.Object);
		bool flag3 = (byte)(int)obj != 0;
		IntPtr intPtr = default(IntPtr);
		object obj2 = (nint)intPtr;
		string text;
		bool flag6;
		bool flag9;
		if (!flag2)
		{
			bool flag4 = artilleryReloadController != null;
			bool flag5 = !flag4;
			flag3 = (byte)(int)obj != 0;
			obj2 = (nint)intPtr;
			if (!flag5)
			{
				ReloadStateDef currentState = artilleryReloadController.CurrentState;
				if (currentState != null)
				{
					ReloadStateDef currentState2 = artilleryReloadController.CurrentState;
					text = currentState2.stateKey;
				}
				else
				{
					text = null;
				}
				if (string.IsNullOrEmpty(loadStateKey))
				{
					flag6 = false;
					object obj3 = 0;
				}
				else
				{
					bool flag7 = text == loadStateKey;
					flag6 = flag7;
					object obj3 = 0;
				}
				if (bullets != null)
				{
					List<GameObject> list = bullets;
					if (list._size > 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						bool flag8 = obj != null;
						flag9 = flag8;
						object obj3 = 0;
						goto IL_04c8;
					}
				}
				flag9 = false;
				goto IL_04c8;
			}
		}
		goto IL_02ca;
		IL_04c8:
		bool flag10 = flag6 & flag9;
		if (!force)
		{
			LookAtTarget lookAtTarget = loadButton;
			if (lookAtTarget.isActive == flag10)
			{
				bool flag11 = text == lastStateKey;
				object obj3 = 0;
				if (flag11)
				{
					bool flag12 = flag9 == lastSlotAHasShell;
					obj3 = 0;
					flag3 = (byte)(int)obj != 0;
					obj2 = 0;
					if (flag12)
					{
						goto IL_02ca;
					}
				}
			}
		}
		bool flag13 = !debugLogs;
		flag3 = (byte)(int)obj != 0;
		obj2 = 0;
		if (!flag13)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			object obj4 = default(object);
			string message = $"[CylinderShellSelector] LoadButton active={arg} (state={text}, slotA={obj4})";
			Debug.Log(message);
			flag3 = flag10;
			obj2 = obj4;
		}
		loadButton.SetActive(flag10);
		lastStateKey = text;
		lastSlotAHasShell = flag9;
		goto IL_02ca;
		IL_02ca:
		if (!(moveButton != null) || !(artilleryReloadController != null))
		{
			return;
		}
		ReloadStateDef currentState3 = artilleryReloadController.CurrentState;
		bool flag14 = currentState3 == null;
		string text2 = null;
		if (!flag14)
		{
			ReloadStateDef currentState4 = artilleryReloadController.CurrentState;
			text2 = currentState4.stateKey;
		}
		bool flag15;
		if (moveStateKeys == null)
		{
			flag15 = false;
			nint num = unchecked((nint)null);
		}
		else
		{
			bool flag16 = moveStateKeys.Contains(text2);
			flag15 = flag16;
			nint num = 0;
		}
		if (!force)
		{
			LookAtTarget lookAtTarget2 = moveButton;
			if (lookAtTarget2.isActive == flag15 && flag15 == lastMoveButtonActive)
			{
				return;
			}
		}
		if (debugLogs)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg2 = default(object);
			string message2 = $"[CylinderShellSelector] MoveButton active={arg2} (state={text2})";
			Debug.Log(message2);
		}
		moveButton.SetActive(flag15);
		lastMoveButtonActive = flag15;
	}

	private void OnLoadButtonClicked()
	{
		if (!(loadButton != null))
		{
			return;
		}
		LookAtTarget lookAtTarget = loadButton;
		if (!lookAtTarget.isActive || !(artilleryReloadController != null))
		{
			return;
		}
		if (bullets != null)
		{
			List<GameObject> list = bullets;
			if (list._size > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				UnityEngine.Object obj = default(UnityEngine.Object);
				if (obj != null)
				{
					artilleryReloadController.AdvanceState();
					return;
				}
			}
		}
		if (debugLogs)
		{
			Debug.LogWarning("[CylinderShellSelector] Load button clicked but slot A is empty — ignoring (stale button state during rotation).");
		}
		loadButton.SetActive(active: false);
	}

	private void OnMoveButtonClicked()
	{
		if (!(moveButton != null))
		{
			return;
		}
		LookAtTarget lookAtTarget = moveButton;
		if (lookAtTarget.isActive && animator != null)
		{
			if (loadButton != null)
			{
				loadButton.SetActive(active: false);
			}
			animator.Play("Move");
		}
	}

	public unsafe void AnimationEvent_RepopulateSlotA()
	{
		//IL_0117: Expected O, but got Ref
		//IL_013b: Expected O, but got Ref
		if (lastLoadedShellPrefab != null)
		{
			if (bullets == null)
			{
				return;
			}
			List<GameObject> list = bullets;
			if (list._size != 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				UnityEngine.Object obj = default(UnityEngine.Object);
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					UnityEngine.Object.Destroy(obj);
				}
				Transform[] array = slots;
				GameObject gameObject = UnityEngine.Object.Instantiate(lastLoadedShellPrefab, array[0]);
				Transform transform = gameObject.transform;
				Vector3 vector = default(Vector3);
				transform.localPosition = (Vector3)(&vector);
				Transform transform2 = gameObject.transform;
				transform2.localRotation = (Quaternion)(&vector);
				bullets.set_Item(0, gameObject);
				UpdateButtonActives(force: true);
			}
		}
		else if (debugLogs)
		{
			Debug.LogError("No prefab stored from last load! Cannot refill slot A.");
		}
	}

	public unsafe void AFRotateDone()
	{
		//IL_0082: Expected O, but got I4
		//IL_008b: Expected O, but got I4
		//IL_02eb: Expected O, but got I4
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Expected O, but got Unknown
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Expected O, but got Unknown
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Expected O, but got Unknown
		//IL_015b: Expected O, but got I4
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Expected O, but got Unknown
		//IL_0112: Expected O, but got I4
		//IL_0477: Expected O, but got I4
		//IL_048c: Expected O, but got I4
		//IL_03ad: Expected O, but got I4
		//IL_03b6: Expected O, but got I4
		//IL_03bf: Expected O, but got I4
		//IL_05db: Expected O, but got I4
		//IL_0210: Expected O, but got Ref
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Expected O, but got Unknown
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Expected O, but got Unknown
		//IL_0436: Expected O, but got I4
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Expected O, but got Unknown
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Expected O, but got Unknown
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Expected O, but got Unknown
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Expected O, but got Unknown
		//IL_0244: Expected O, but got Ref
		if (slots != null)
		{
			Transform[] array = slots;
			if (array.Length != 0 && bullets != null)
			{
				List<GameObject> list = bullets;
				if (list._size != 0)
				{
					object obj = 0;
					object obj2 = 0;
					UnityEngine.Object obj6 = default(UnityEngine.Object);
					GameObject gameObject = default(GameObject);
					GameObject gameObject2 = default(GameObject);
					Vector3 zeroVector = default(Vector3);
					GameObject gameObject3 = default(GameObject);
					Quaternion quaternion = default(Quaternion);
					while ((nint)obj < list._size)
					{
						object obj3 = obj2 + 1;
						if (~(invertRotationDirection ? 1u : 0u) == 0)
						{
							obj3 += -2;
							object obj4 = obj2 - 1;
							if ((nint)obj4 < 0)
							{
								Transform[] array2 = slots;
								obj3 = array2.Length - 1;
							}
						}
						else
						{
							Transform[] array3 = slots;
							object obj5 = obj2 + 1;
							if ((nint)obj5 >= array3.Length)
							{
								obj3 = 0;
							}
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						if (obj6 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							Transform transform = gameObject.transform;
							Transform[] array4 = slots;
							transform.SetParent(array4[obj3], worldPositionStays: false);
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							Transform transform2 = gameObject2.transform;
							transform2.localPosition = (Vector3)(&zeroVector);
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							Transform transform3 = gameObject3.transform;
							transform3.localRotation = (Quaternion)(&quaternion);
							zeroVector = Vector3.zeroVector;
						}
						list = bullets;
						obj2++;
						obj = obj2;
					}
					List<GameObject> list2 = bullets;
					GameObject item = default(GameObject);
					List<GameObject> list3;
					int index;
					if (~(invertRotationDirection ? 1u : 0u) == 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						list2.Add(item);
						list3 = bullets;
						index = 0;
					}
					else
					{
						object obj7 = list2._size - 1;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						list2.Insert(0, item);
						List<GameObject> list4 = bullets;
						list3 = bullets;
						index = list4._size - 1;
					}
					list3.RemoveAt(index);
					if (rotateShellPrefabsWithCylinder && shellPrefabs != null)
					{
						GameObject[] array5 = shellPrefabs;
						if (array5.Length != 0)
						{
							if (invertRotationDirection)
							{
								object obj8 = 32;
								object obj9 = 0;
								object obj10 = 0;
								GameObject[] array6;
								while (true)
								{
									array6 = shellPrefabs;
									object obj11 = array5.Length - 1;
									if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj9) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj11))
									{
										break;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v462 @ rdi_v14+8+v522 @ rcx_v18 (UnityEngine.GameObject[])]");
									_ = 0;
									array5 = shellPrefabs;
									obj10++;
									obj8 += 8;
									bool flag = shellPrefabs != null;
									obj9 = obj10;
									if (!flag)
									{
										throw new NullReferenceException();
									}
								}
								object obj12 = array6.Length - 1;
								array6[obj12] = array5[0];
							}
							else
							{
								object obj13 = array5.Length - 1;
								object obj14 = array5.Length - 1;
								if ((nint)obj14 > 0)
								{
									object obj15 = obj14 * 8;
									object obj16 = obj15 + 32;
									do
									{
										GameObject[] array7 = shellPrefabs;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v507 @ rax_v21 (UnityEngine.GameObject[])+FFFFFFF8+v486 @ rsi_v11]");
										_ = 0;
										obj14--;
										obj16 -= 8;
									}
									while ((nint)obj14 > 0);
								}
								GameObject[] array8 = shellPrefabs;
								array8[0] = array5[obj13];
							}
						}
					}
				}
			}
		}
		UpdateButtonActives(force: true);
	}

	public void AFRotateMid()
	{
		//IL_0018: Expected O, but got I4
		//IL_0021: Expected O, but got I4
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected O, but got Unknown
		if (bullets == null)
		{
			return;
		}
		List<GameObject> list = bullets;
		object obj = 0;
		object obj2 = 0;
		UnityEngine.Object obj3 = default(UnityEngine.Object);
		GameObject gameObject = default(GameObject);
		while ((nint)obj2 < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj3 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Transform transform = gameObject.transform;
				transform.parent = null;
			}
			list = bullets;
			obj++;
			obj2 = obj;
		}
	}

	public int FirstEmptySlotIndex()
	{
		//IL_0279: Expected I4, but got I8
		//IL_0287: Expected I4, but got O
		//IL_007b: Expected O, but got I4
		//IL_01d6: Expected O, but got I4
		int num;
		if (slots != null)
		{
			UnityEngine.Object obj = default(UnityEngine.Object);
			if (!invertRotationDirection)
			{
				if (bullets != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					if (!(obj != null))
					{
						return 0;
					}
					Transform[] array = slots;
					if (slots != null)
					{
						Transform[] array2 = slots;
						num = array.Length - 1;
						while (num < array2.Length)
						{
							List<GameObject> list = bullets;
							if (bullets != null)
							{
								list = (List<GameObject>)list._size;
							}
							if (num >= (nint)list)
							{
								goto IL_0264;
							}
							if (bullets != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								if (!(obj != null))
								{
									goto IL_0264;
								}
								array2 = slots;
								num--;
								if (slots != null)
								{
									continue;
								}
							}
							goto IL_0279;
						}
						goto IL_026f;
					}
				}
				goto IL_0279;
			}
			Transform[] array3 = slots;
			num = 0;
			int num2 = 0;
			while (num2 < array3.Length)
			{
				List<GameObject> list2 = bullets;
				if (bullets != null)
				{
					list2 = (List<GameObject>)list2._size;
				}
				if (num >= (nint)list2)
				{
					goto IL_0264;
				}
				if (bullets != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					if (!(obj != null))
					{
						goto IL_0264;
					}
					array3 = slots;
					num++;
					if (slots != null)
					{
						num2 = num;
						continue;
					}
				}
				goto IL_0279;
			}
		}
		goto IL_026f;
		IL_0264:
		return num;
		IL_026f:
		return -1;
		IL_0279:
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	public bool HasEmptySlot()
	{
		int num = FirstEmptySlotIndex();
		int num2 = num >> 31;
		return (byte)(num2 ^ 1) != 0;
	}

	public int EmptySlotCount()
	{
		//IL_0018: Expected O, but got I4
		//IL_002a: Expected O, but got I4
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Expected O, but got Unknown
		//IL_0065: Expected O, but got I4
		//IL_00fe: Expected I4, but got O
		if (slots != null)
		{
			Transform[] array = slots;
			object obj = 0;
			int num = 0;
			object obj2 = 0;
			UnityEngine.Object obj3 = default(UnityEngine.Object);
			while (true)
			{
				if ((nint)obj2 < array.Length)
				{
					List<GameObject> list = bullets;
					if (bullets != null)
					{
						list = (List<GameObject>)list._size;
					}
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<List<GameObject>, UIntPtr>(ref list))
					{
						if (bullets == null)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						if (obj3 == null)
						{
							num++;
						}
					}
					array = slots;
					obj++;
					if (slots == null)
					{
						break;
					}
					obj2 = obj;
					continue;
				}
				return num;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
		return 0;
	}

	public unsafe bool TryInsertShellRuntime(ShellDefinition shell, ShellSlotPool.ShellSource source, out int slotIndex)
	{
		//IL_02dd: Expected I4, but got O
		//IL_01c2: Expected O, but got Ref
		//IL_01e6: Expected O, but got Ref
		ref int reference = ref *(int*)4294967295L;
		if (shell != null && shell.BlueprintPrefab != null && slots != null)
		{
			int num = FirstEmptySlotIndex();
			if (num >= 0)
			{
				if (bullets == null)
				{
					Transform[] array = slots;
					List<GameObject> list = new List<GameObject>(array.Length);
					bullets = list;
				}
				List<GameObject> list2 = bullets;
				while (true)
				{
					Transform[] array2 = slots;
					if (list2._size >= array2.Length)
					{
						break;
					}
					bullets.Add(null);
					list2 = bullets;
				}
				Transform[] array3 = slots;
				if (num < array3.Length)
				{
					ShellBlueprint shellBlueprint = UnityEngine.Object.Instantiate(shell.BlueprintPrefab, array3[num]);
					shellBlueprint.shellDefinition = shell;
					Transform transform = shellBlueprint.transform;
					Vector3 vector = default(Vector3);
					transform.localPosition = (Vector3)(&vector);
					Transform transform2 = shellBlueprint.transform;
					transform2.localRotation = (Quaternion)(&vector);
					GameObject value = shellBlueprint.gameObject;
					bullets.set_Item(num, value);
					reference = ref *(int*)num;
					if (num == 0)
					{
						UpdateButtonActives(force: true);
					}
					if (source == ShellSlotPool.ShellSource.Punchcard && onShellDeployedByPlayer != null)
					{
						onShellDeployedByPlayer.Invoke();
					}
					return true;
				}
				IndexOutOfRangeException ex = new IndexOutOfRangeException();
				return (byte)(int)ex != 0;
			}
		}
		return false;
	}

	public CylinderShellSelector()
	{
		GameObject[] array = new GameObject[6];
		shellPrefabs = array;
		List<string> list = new List<string>();
		list._002Ector();
		moveStateKeys = list;
		rotateShellPrefabsWithCylinder = true;
		base._002Ector();
	}
}
