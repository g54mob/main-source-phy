using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class UINotificationManager : MonoBehaviour
{
	private static UINotificationManager _003CInstance_003Ek__BackingField;

	public Transform notificationRoot;

	public UINotification notificationPrefab;

	public float defaultLifetime;

	public Color defaultBorderColor;

	public static UINotificationManager Instance
	{
		get
		{
			return _003CInstance_003Ek__BackingField;
		}
		private set
		{
			_003CInstance_003Ek__BackingField = value;
		}
	}

	public static bool HasInstance => _003CInstance_003Ek__BackingField != null;

	private unsafe void Awake()
	{
		//IL_00a4: Expected O, but got Ref
		//IL_00ac: Expected O, but got Ref
		//IL_0195: Expected O, but got I4
		//IL_03ac: Expected I, but got O
		//IL_03c9: Expected I, but got O
		//IL_03d7: Expected I, but got O
		//IL_013a: Expected O, but got I
		//IL_0143: Expected O, but got I4
		//IL_01c2: Expected O, but got I
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Expected O, but got Unknown
		//IL_01f8: Expected I, but got O
		//IL_0208: Expected O, but got I
		//IL_023c: Expected I, but got O
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Expected O, but got Unknown
		//IL_025a: Expected O, but got I
		//IL_028f: Expected I, but got O
		if (_003CInstance_003Ek__BackingField != null && _003CInstance_003Ek__BackingField != this)
		{
			GameObject obj = base.gameObject;
			UnityEngine.Object.Destroy(obj);
			return;
		}
		_003CInstance_003Ek__BackingField = this;
		GameObject target = base.gameObject;
		UnityEngine.Object.DontDestroyOnLoad(target);
		IEnumerator enumerator = notificationRoot.GetEnumerator();
		object obj3 = default(object);
		object obj2 = (object)(&obj3);
		object obj5 = default(object);
		object obj4 = (object)(&obj5);
		object obj6 = default(object);
		object obj17 = default(object);
		object obj18 = default(object);
		Component component = default(Component);
		Transform transform2;
		while (true)
		{
			object obj16;
			object obj8;
			if (obj3 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj6 != null)
				{
					bool flag = obj3 == null;
					Transform transform = null;
					if (flag)
					{
						goto IL_030f;
					}
					object obj7 = obj3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v283 @ r10_v6+12E]");
					if ((nint)0 >= (nint)0)
					{
						goto IL_017a;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v283 @ r10_v6+B0]");
					obj8 = 0;
					object obj9 = 0;
					while (true)
					{
						object obj10 = obj9 + obj9;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v604 @ r8_v11+v536 @ rax_v42*8]");
						if (0 == (nint)typeof(IEnumerator))
						{
							break;
						}
						obj9++;
						object obj11 = obj9;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v283 @ r10_v6+12E]");
						if ((nint)obj11 < 0)
						{
							continue;
						}
						goto IL_017a;
					}
					object obj12 = obj9 + obj9;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v604 @ r8_v11+8+v596 @ rcx_v36*8]");
					object obj13 = (nint)0 + (nint)1;
					object obj14 = obj13 << 4;
					object obj15 = obj14 + 312;
					obj16 = obj15 + obj7;
					goto IL_0394;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				obj4 = obj17;
				if (obj17 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				return;
			}
			throw new NullReferenceException();
			IL_030f:
			throw new NullReferenceException();
			IL_017a:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj16 = obj18;
			obj8 = 1;
			goto IL_0394;
			IL_0394:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v603 @ rdx_v18] (should have been resolved before IL gen)");
			nint num = (nint)typeof(Transform);
			bool flag2 = (object)component == null;
			nint num2 = (nint)typeof(IEnumerator);
			nint num3 = (nint)typeof(Transform);
			if (flag2)
			{
				break;
			}
			num2 = (nint)component;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v299 @ rcx_v28 (Il2CppClass<UnityEngine.Transform>)+130]");
			object obj19 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ r9_v5 (Il2CppClass<System.Collections.IEnumerator>)+130]");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v299 @ rcx_v28 (Il2CppClass<UnityEngine.Transform>)+130]");
			bool flag3 = num4 < 0;
			transform2 = (Transform)component;
			num3 = (nint)typeof(Transform);
			if (!flag3)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ r9_v5 (Il2CppClass<System.Collections.IEnumerator>)+C8]");
				object obj20 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v428 @ rax_v33+FFFFFFF8+v427 @ rax_v32*8]");
				bool flag4 = 0 != (nint)typeof(Transform);
				transform2 = (Transform)component;
				num3 = (nint)typeof(Transform);
				if (!flag4)
				{
					GameObject obj21 = component.gameObject;
					UnityEngine.Object.Destroy(obj21);
					continue;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
			goto IL_030f;
		}
		transform2 = (Transform)component;
		throw new NullReferenceException();
	}

	private void OnDestroy()
	{
		if (_003CInstance_003Ek__BackingField == this)
		{
			_003CInstance_003Ek__BackingField = null;
		}
	}

	public static UINotification ShowNotification(string title, string description, float lifetime = -1f, Color? borderColor = null)
	{
		if (!(_003CInstance_003Ek__BackingField != null))
		{
			Debug.LogWarning("[UINotificationManager] No active manager in scene.");
			return null;
		}
		Color? borderColor2 = default(Color?);
		if ((object)_003CInstance_003Ek__BackingField != null)
		{
			return _003CInstance_003Ek__BackingField.Spawn(title, description, lifetime, borderColor2);
		}
		return (UINotification)(object)new NullReferenceException();
	}

	private unsafe UINotification Spawn(string title, string description, float lifetime, Color? borderColor)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0117: Invalid comparison between F4 and I4
		//IL_0274: Expected O, but got I
		//IL_0150: Expected O, but got Ref
		//IL_02a7: Expected F4, but got O
		//IL_02a2: Expected native int or pointer, but got O
		//IL_02b5: Expected O, but got Ref
		//IL_02ce: Expected O, but got Ref
		//IL_02c9: Expected native int or pointer, but got O
		//IL_019e: Expected O, but got I
		//IL_01d1: Expected O, but got Ref
		//IL_01e7: Expected O, but got I
		//IL_0217: Expected O, but got F4
		object obj = default(object);
		Color color = (Color)(&obj);
		_ = 0;
		_ = 0;
		UINotification uINotification;
		if (notificationRoot != null && notificationPrefab != null)
		{
			uINotification = UnityEngine.Object.Instantiate(notificationPrefab, notificationRoot);
			if ((object)uINotification != null)
			{
				GameObject gameObject = uINotification.gameObject;
				if ((object)gameObject != null)
				{
					gameObject.SetActive(value: true);
					Transform transform = uINotification.transform;
					if ((object)transform != null)
					{
						transform.SetAsLastSibling();
						bool flag = lifetime > 0f;
						float lifetime2 = lifetime;
						if (!flag)
						{
							lifetime2 = defaultLifetime;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Color)+B0]");
						object obj2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v336 @ rax_v21+10]");
						_ = 0;
						object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 16));
						nint num = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
						object obj4 = default(object);
						Color color2;
						if (obj4 != null)
						{
							nint num2 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v386 @ rcx_v23 (Il2CppClass<UnityEngine.Color>)+FC]");
							object obj5 = (nint)0 + (nint)15;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v386 @ rcx_v23 (Il2CppClass<UnityEngine.Color>)+FC]");
							if ((nint)obj5 > 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
							}
							nint num3 = 0;
							object obj6 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 16));
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v439 @ rcx_v25 (Il2CppClass<System.Nullable`1<UnityEngine.Color>>)+80]");
							object obj7 = (nint)0 + (nint)32;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
							color2 = (Color)color.r;
						}
						else
						{
							color2 = defaultBorderColor;
						}
						((Color*)(nint)color)->r = (float)color2;
						Color? color3 = (Color?)(object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj, 40));
						_ = 0;
						_ = 0;
						*(Color?*)(nint)color3 = (Color)(&obj);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Color)+38]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1 (UnityEngine.Color)+28]");
						_ = 0;
						Color? borderColor2 = default(Color?);
						uINotification.Show(title, description, lifetime2, borderColor2);
						goto IL_0306;
					}
				}
			}
			return (UINotification)(object)new NullReferenceException();
		}
		Debug.LogWarning("[UINotificationManager] Missing notification root or prefab.", this);
		uINotification = null;
		goto IL_0306;
		IL_0306:
		return uINotification;
	}

	public UINotificationManager()
	{
		//IL_0012: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C60]");
		defaultBorderColor = (Color)0;
		defaultLifetime = 4f;
		base._002Ector();
	}
}
