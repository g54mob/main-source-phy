using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;

namespace Kamgam.SettingsGenerator;

public class SettingsVolume : MonoBehaviour
{
	private static SettingsVolume _instance;

	[NonSerialized]
	public Volume Volume;

	private static float _defaultPriority = 99f;

	protected List<ISettingsVolumeControl> _controls;

	[NonSerialized]
	protected bool _volumeWasRegisteredWithMananger;

	public static SettingsVolume Instance
	{
		get
		{
			if (!_instance)
			{
				GameObject gameObject = new GameObject();
				if ((object)gameObject != null)
				{
					SettingsVolume instance = gameObject.AddComponent<SettingsVolume>();
					_instance = instance;
					if ((object)_instance != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
						object obj = default(object);
						if (obj != null)
						{
							object obj2 = obj;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v256 @ rdx_v8+168] (should have been resolved before IL gen)");
							string text = default(string);
							_instance.name = text;
							if ((object)_instance != null)
							{
								_instance.createVolume();
								if ((object)_instance != null)
								{
									GameObject target = _instance.gameObject;
									UnityEngine.Object.DontDestroyOnLoad(target);
									goto IL_014c;
								}
							}
						}
					}
				}
				return (SettingsVolume)(object)new NullReferenceException();
			}
			goto IL_014c;
			IL_014c:
			return _instance;
		}
	}

	public static float Priority
	{
		get
		{
			if (!(_instance != null))
			{
				return _defaultPriority;
			}
			SettingsVolume instance = _instance;
			Volume volume = instance.Volume;
			return volume.priority;
		}
		set
		{
			_defaultPriority = value;
			if (_instance != null)
			{
				SettingsVolume instance = _instance;
				Volume volume = instance.Volume;
				volume.priority = value;
			}
		}
	}

	protected virtual void createVolume()
	{
		GameObject gameObject = base.gameObject;
		Volume volume = gameObject.AddComponent<Volume>();
		Volume = volume;
		Volume volume2 = Volume;
		volume2.priority = _defaultPriority;
		Volume volume3 = Volume;
		VolumeProfile internalProfile = ScriptableObject.CreateInstance<VolumeProfile>();
		volume3.m_InternalProfile = internalProfile;
		Volume.isGlobal = true;
	}

	public void MatchMainCameraLayer()
	{
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Expected I4, but got Unknown
		//IL_013e: Expected O, but got I4
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Expected O, but got Unknown
		Camera main = Camera.main;
		if (!(main != null) || !(Volume != null))
		{
			return;
		}
		Camera main2 = Camera.main;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		int num = 0;
		object obj = default(object);
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
			int num2 = 1 << num;
			int num3 = num2 & obj;
			bool flag = num3 == 0;
			bool flag2 = num3 < 0;
			bool flag3 = !flag2;
			object obj2 = !flag;
			object obj3 = flag3 & obj2;
			if (obj3 != null)
			{
				break;
			}
			num++;
			if (num >= 32)
			{
				return;
			}
		}
		GameObject gameObject = Volume.gameObject;
		gameObject.layer = num;
	}

	public TComp GetOrAddComponent<TComp>()
	{
		if ((object)Volume != null)
		{
			VolumeProfile profile = Volume.profile;
			if ((object)profile != null)
			{
				if (profile.TryGet<TComp>(out TComp component))
				{
					return component;
				}
				if ((object)Volume != null)
				{
					VolumeProfile profile2 = Volume.profile;
					if ((object)profile2 != null)
					{
						return profile2.Add<TComp>(false);
					}
				}
			}
		}
		return (TComp)new NullReferenceException();
	}

	public unsafe T GetOrCreateControl<T>() where T : new()
	{
		//IL_0008: Expected O, but got Ref
		//IL_006b: Expected O, but got I
		//IL_007b: Expected O, but got I
		//IL_0091: Expected O, but got I
		//IL_00cf: Expected O, but got I
		//IL_00dd: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ r8 (Kamgam.SettingsGenerator.SettingsVolume)+38]");
		bool flag = (nint)0 != 0;
		SettingsVolume settingsVolume = this;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
			SettingsVolume settingsVolume2 = default(SettingsVolume);
			settingsVolume = settingsVolume2;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ r8 (Kamgam.SettingsGenerator.SettingsVolume)+38]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rax_v2+8]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r9_v1+FC]");
		object obj5 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ r9_v1+FC]");
		if ((nint)obj5 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ r8 (Kamgam.SettingsGenerator.SettingsVolume)+38]");
			object obj6 = 0;
			object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 48));
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180744BC0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		}
		T result = default(T);
		return result;
	}

	public unsafe T GetOrCreateControl<T>(out bool isNew) where T : new()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0077: Expected O, but got I
		//IL_008f: Expected O, but got I
		//IL_00aa: Expected O, but got I
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Expected O, but got Unknown
		//IL_038f: Expected O, but got I
		//IL_00dc: Expected O, but got I8
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Expected O, but got Unknown
		//IL_03e6: Expected O, but got I
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Expected O, but got Unknown
		//IL_043d: Expected O, but got I
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Expected O, but got Unknown
		//IL_0494: Expected O, but got I
		//IL_00ee: Expected O, but got I8
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Expected O, but got Unknown
		//IL_0501: Expected O, but got I
		//IL_0100: Expected O, but got I8
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Expected O, but got Unknown
		//IL_0112: Expected O, but got I8
		//IL_0124: Expected O, but got I8
		//IL_017e: Expected O, but got Ref
		//IL_01b6: Expected O, but got Ref
		//IL_057c: Expected O, but got Ref
		//IL_05c6: Expected O, but got I
		//IL_01cf: Expected O, but got Ref
		//IL_01dd: Expected O, but got Ref
		//IL_01f7: Expected O, but got I
		//IL_02d9: Expected O, but got I
		//IL_0234: Expected O, but got I
		//IL_023d: Expected O, but got I4
		//IL_024d: Expected O, but got I
		//IL_0321: Expected O, but got I
		//IL_032a: Expected O, but got I4
		//IL_0349: Expected O, but got I
		//IL_0359: Expected O, but got I
		//IL_028b: Expected O, but got Ref
		//IL_029b: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v387 @ r9_v2+38]");
		if ((nint)0 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v387 @ r9_v2+38]");
			if ((nint)0 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v387 @ r9_v2+38]");
		object obj3 = 0;
		object obj4 = obj3;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
		obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
		object obj5 = (nint)0 + (nint)16;
		object obj6 = obj5 + 15;
		object obj7;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			_ = ref obj2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
			obj7 = (nint)0 + (nint)15;
			object obj8 = obj7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
			if ((nint)obj8 > 0)
			{
				goto IL_03b3;
			}
		}
		obj7 = 1152921504606846960L;
		goto IL_03b3;
		IL_03b3:
		object obj9 = obj7 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		_ = ref obj2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
		object obj10 = (nint)0 + (nint)15;
		object obj11 = obj10;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
		if ((nint)obj11 <= 0)
		{
			obj10 = 1152921504606846960L;
		}
		object obj12 = obj10 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		_ = ref obj2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
		object obj13 = (nint)0 + (nint)15;
		object obj14 = obj13;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
		if ((nint)obj14 <= 0)
		{
			obj13 = 1152921504606846960L;
		}
		object obj15 = obj13 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		_ = ref obj2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
		object obj16 = (nint)0 + (nint)15;
		object obj17 = obj16;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
		if ((nint)obj17 <= 0)
		{
			obj16 = 1152921504606846960L;
		}
		object obj18 = obj16 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		_ = ref obj2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
		_ = 0;
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
		object obj19 = (nint)0 + (nint)15;
		object obj20 = obj19;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
		if ((nint)obj20 <= 0)
		{
			obj19 = 1152921504606846960L;
		}
		object obj21 = obj19 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		_ = ref obj2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
		if (_controls == null)
		{
			List<ISettingsVolumeControl> controls = new List<ISettingsVolumeControl>();
			_controls = controls;
		}
		List<ISettingsVolumeControl> controls2 = _controls;
		if (_controls != null)
		{
			object obj22 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 8));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v24 @ rbp_v1+8]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v24 @ rbp_v1+18]");
			_ = 0;
			_ = 0;
			List<ISettingsVolumeControl>.Enumerator enumerator = (List<ISettingsVolumeControl>.Enumerator)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 32));
			object obj26 = default(object);
			ISettingsVolumeControl item = default(ISettingsVolumeControl);
			T result = default(T);
			while (true)
			{
				List<ISettingsVolumeControl>.Enumerator enumerator2 = (List<ISettingsVolumeControl>.Enumerator)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 32));
				if (((List<ISettingsVolumeControl>.Enumerator*)enumerator2)->MoveNext())
				{
					object obj23 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 56));
					object obj24 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 32));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v387 @ r9_v2+38]");
					object obj25 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if (obj26 == null)
					{
						continue;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v24 @ rbp_v1+D8]");
					object obj27 = 0;
					obj27 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v387 @ r9_v2+38]");
					object obj28 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A67B0");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
					((List<ISettingsVolumeControl>.Enumerator*)enumerator)->Dispose();
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
					object obj29 = (object)(&obj2);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
					object obj30 = 0;
				}
				else
				{
					((List<ISettingsVolumeControl>.Enumerator*)enumerator)->Dispose();
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18067CD50");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v387 @ r9_v2+38]");
					object obj31 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A76C0");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v387 @ r9_v2+38]");
					object obj32 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					if (_controls == null)
					{
						break;
					}
					_controls.Add(item);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v24 @ rbp_v1+D8]");
					object obj33 = 0;
					obj33 = 1;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v24 @ rbp_v1+68]");
					object obj29 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rcx_v2+FC]");
					object obj30 = 0;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
				return result;
			}
		}
		throw new NullReferenceException();
	}

	public T FindDefaultVolumeComponent<T>(bool useStackAsFallback = false, int layerMask = -1)
	{
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Expected O, but got Unknown
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Expected O, but got Unknown
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Expected O, but got Unknown
		//IL_0463: Expected O, but got I4
		//IL_08c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ca: Expected O, but got Unknown
		//IL_08d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d8: Expected O, but got Unknown
		//IL_0645: Unknown result type (might be due to invalid IL or missing references)
		//IL_064a: Expected O, but got Unknown
		//IL_0655: Expected O, but got I4
		//IL_051f: Expected O, but got I
		//IL_0550: Expected O, but got I
		//IL_07c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cb: Expected O, but got Unknown
		//IL_07d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d9: Expected O, but got Unknown
		//IL_0757: Expected O, but got F4
		//IL_07a2: Expected O, but got F4
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r9 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		Camera main = Camera.main;
		T result;
		T component4;
		if (main != null)
		{
			VolumeManager instance = VolumeManager.instance;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
			LayerMask layerMask2 = default(LayerMask);
			Volume[] volumes = instance.GetVolumes(layerMask2);
			object obj = volumes + 32;
			Collider collider = null;
			T component = (T)null;
			Collider collider2 = null;
			SettingsVolume settingsVolume = this;
			for (; (nint)collider2 < volumes.Length; collider = (Collider)(collider + 1), obj += 8, collider2 = collider)
			{
				if ((nint)collider >= volumes.Length)
				{
					goto IL_0860;
				}
				Component component2 = (Component)obj;
				bool flag;
				if (!(main != null))
				{
					flag = false;
				}
				else
				{
					Transform transform = ((Component)obj).transform;
					Transform parent = main.transform;
					if (transform.IsChildOf(parent))
					{
						flag = true;
					}
					else
					{
						Transform transform2 = ((Component)obj).transform;
						Transform transform3 = main.transform;
						flag = transform2 == transform3;
						settingsVolume = this;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v337 @ rbx_v18 (UnityEngine.Component)+20]");
				if (((nint)0 == 0 && !flag) || !((Behaviour)obj).isActiveAndEnabled)
				{
					continue;
				}
				VolumeProfile profile = ((Volume)obj).profile;
				if (!(profile != null) || !((UnityEngine.Object)obj != settingsVolume.Volume))
				{
					continue;
				}
				VolumeProfile profile2 = ((Volume)obj).profile;
				if (!profile2.TryGet<T>(out component))
				{
					continue;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v181 @ stack_-A8_v6 (T)+18]");
				bool flag2 = (nint)0 != 0;
				result = component;
				if (!flag2)
				{
					continue;
				}
				goto IL_0896;
			}
			if (useStackAsFallback)
			{
				Camera main2 = Camera.main;
				if (!(main2 != null))
				{
					VolumeManager instance2 = VolumeManager.instance;
					result = instance2._003Cstack_003Ek__BackingField.GetComponent<T>();
				}
				else
				{
					Camera main3 = Camera.main;
					GameObject gameObject = main3.gameObject;
					int layer = gameObject.layer;
					unregisterFromVolumeManager(Volume, layer);
					VolumeManager instance3 = VolumeManager.instance;
					VolumeManager instance4 = VolumeManager.instance;
					Camera main4 = Camera.main;
					Transform trigger = main4.transform;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
					LayerMask layerMask3 = default(LayerMask);
					instance3.Update(instance4._003Cstack_003Ek__BackingField, trigger, layerMask3);
					VolumeManager instance5 = VolumeManager.instance;
					if (instance5._003Cstack_003Ek__BackingField == null)
					{
						Volume[] array = findVolumesInActiveScene(includeInactive: true);
						if (!CollectionExtensions.IsNullOrEmpty(array))
						{
							object obj2 = array + 32;
							Collider collider3 = null;
							Bounds bounds = (Bounds)0;
							Collider collider4 = null;
							UnityEngine.Object obj3 = null;
							for (; (nint)collider4 < array.Length; collider3 = (Collider)(collider3 + 1), obj2 += 8, collider4 = collider3)
							{
								if ((nint)collider3 < array.Length)
								{
									UnityEngine.Object obj4 = (UnityEngine.Object)obj2;
									if (!((UnityEngine.Object)obj2 != Volume))
									{
										continue;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v625 @ rbx_v17 (UnityEngine.Object)+20]");
									if ((nint)0 == 0)
									{
										continue;
									}
									bool flag3 = obj3 != null;
									Bounds bounds2 = bounds;
									if (flag3)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v625 @ rbx_v17 (UnityEngine.Object)+24]");
										bounds2 = (Bounds)0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v625 @ rbx_v17 (UnityEngine.Object)+24]");
										nint num = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1343 @ rdi_v16 (UnityEngine.Object)+24]");
										bool flag4 = num <= 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v625 @ rbx_v17 (UnityEngine.Object)+24]");
										bounds = (Bounds)0;
										if (flag4)
										{
											continue;
										}
									}
									bounds = bounds2;
									obj3 = (UnityEngine.Object)obj2;
									continue;
								}
								goto IL_0860;
							}
							if (obj3 != null)
							{
								VolumeProfile profile3 = ((Volume)obj3).profile;
								if (profile3.TryGet<T>(out T component3))
								{
									result = component3;
									goto IL_0896;
								}
							}
							Camera main5 = Camera.main;
							if (main5 != null)
							{
								Camera main6 = Camera.main;
								Transform transform4 = main6.transform;
								Vector3 position = transform4.position;
								object obj5 = array + 32;
								Bounds bounds3 = (Bounds)0;
								component4 = (T)null;
								Collider component5 = null;
								Vector3 point = default(Vector3);
								for (Collider collider5 = null; (nint)collider5 < array.Length; collider5 = (Collider)(collider5 + 1), obj5 += 8)
								{
									if ((nint)collider5 < array.Length)
									{
										UnityEngine.Object obj6 = (UnityEngine.Object)obj5;
										bool flag5 = (UnityEngine.Object)obj5 == Volume;
										if (flag5)
										{
											continue;
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ rbx_v16 (UnityEngine.Object)+20]");
										if ((nint)0 != (flag5 ? 1 : 0))
										{
											continue;
										}
										GameObject gameObject2 = ((Component)obj5).gameObject;
										if (!gameObject2.TryGetComponent<Collider>(out component5))
										{
											continue;
										}
										Bounds bounds4 = component5.bounds;
										bool flag6 = bounds3.Internal_Contains(ref point);
										bool flag7 = !flag6;
										point = (Vector3)position.x;
										bounds3 = (Bounds)bounds4.m_Center;
										if (flag7)
										{
											continue;
										}
										VolumeProfile profile4 = ((Volume)obj5).profile;
										bool flag8 = profile4.TryGet<T>(out component4);
										point = (Vector3)position.x;
										bounds3 = (Bounds)bounds4.m_Center;
										if (!flag8)
										{
											continue;
										}
										goto IL_07de;
									}
									goto IL_0860;
								}
							}
						}
						goto IL_0856;
					}
					VolumeManager instance6 = VolumeManager.instance;
					result = instance6._003Cstack_003Ek__BackingField.GetComponent<T>();
					Camera main7 = Camera.main;
					GameObject gameObject3 = main7.gameObject;
					int layer2 = gameObject3.layer;
					registerWithVolumeManager(Volume, layer2);
				}
				goto IL_0896;
			}
		}
		goto IL_0856;
		IL_0860:
		return (T)new IndexOutOfRangeException();
		IL_0896:
		return result;
		IL_0856:
		result = (T)null;
		goto IL_0896;
		IL_07de:
		result = component4;
		goto IL_0896;
	}

	private static Volume[] findVolumesInActiveScene(bool includeInactive = false)
	{
		return UnityEngine.Object.FindObjectsByType<Volume>(FindObjectsInactive.Include, FindObjectsSortMode.None);
	}

	private static void registerWithVolumeManager(Volume volume, int layer)
	{
		VolumeManager instance = VolumeManager.instance;
		instance.Register(volume);
	}

	private static void unregisterFromVolumeManager(Volume volume, int layer)
	{
		VolumeManager instance = VolumeManager.instance;
		instance.Unregister(volume);
	}

	public void Update()
	{
		if (_volumeWasRegisteredWithMananger)
		{
			return;
		}
		VolumeManager instance = VolumeManager.instance;
		if (instance._003CisInitialized_003Ek__BackingField)
		{
			Camera main = Camera.main;
			if (main != null)
			{
				_volumeWasRegisteredWithMananger = true;
				Camera main2 = Camera.main;
				GameObject gameObject = main2.gameObject;
				int layer = gameObject.layer;
				VolumeManager instance2 = VolumeManager.instance;
				instance2.Unregister(Volume);
				Camera main3 = Camera.main;
				GameObject gameObject2 = main3.gameObject;
				int layer2 = gameObject2.layer;
				VolumeManager instance3 = VolumeManager.instance;
				instance3.Register(Volume);
			}
		}
	}
}
