using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Events;

public class GunFireWatcher : MonoBehaviour
{
	[Serializable]
	public class GunControllerEvent : UnityEvent<GunController>
	{
	}

	private static class ListPool<T>
	{
		private static readonly Stack<List<T>> pool;

		public static List<T> Get()
		{
			//IL_002a: Expected O, but got I
			//IL_003f: Expected O, but got I
			//IL_0165: Expected O, but got I
			//IL_017f: Expected O, but got I4
			//IL_00ad: Expected O, but got I
			//IL_00c7: Expected O, but got I4
			//IL_0109: Expected O, but got I
			//IL_011e: Expected O, but got I
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v71 @ rax_v9 (Il2CppRgctx<GunFireWatcher+ListPool`1>)+10]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v85 @ rax_v11+B8]");
			object obj2 = 0;
			object obj3 = obj2;
			if (obj2 != null)
			{
				nint num2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v87 @ rdi_v1+18]");
				if ((nint)0 <= (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v149 @ rax_v16 (Il2CppClass<GunFireWatcher+ListPool`1>)+135]");
					object obj4 = (nint)0 & (nint)1;
					bool flag = obj4 == null;
					object obj5 = !flag;
					if (obj5 == null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B10");
					}
					nint num3 = 0;
					List<T> result = null;
					nint num4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6A40");
					return result;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v149 @ rax_v16 (Il2CppClass<GunFireWatcher+ListPool`1>)+135]");
				object obj6 = (nint)0 & (nint)1;
				bool flag2 = obj6 == null;
				object obj7 = !flag2;
				if (obj7 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B10");
				}
				nint num5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v291 @ rax_v36 (Il2CppRgctx<GunFireWatcher+ListPool`1>)+10]");
				object obj8 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v308 @ rax_v38+B8]");
				object obj9 = 0;
				if (obj9 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180917300");
					List<T> result2 = default(List<T>);
					return result2;
				}
			}
			return (List<T>)(object)new NullReferenceException();
		}

		public static void Release(List<T> list)
		{
			//IL_001b: Expected O, but got I
			//IL_004b: Expected O, but got I
			//IL_005b: Expected O, but got I
			//IL_0116: Expected O, but got I
			//IL_012b: Expected O, but got I
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rax_v5 (Il2CppRgctx<GunFireWatcher+ListPool`1>)+38]");
			object obj = 0;
			int version = list._version + 1;
			list._version = version;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ rcx_v3+20]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rax_v6+C0]");
			object obj3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
			object obj4 = default(object);
			if (obj4 == null)
			{
				list._size = 0;
			}
			else
			{
				list._size = 0;
				if (list._size > 0)
				{
					Array.Clear(list._items, 0, list._size);
				}
			}
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v217 @ rax_v17 (Il2CppRgctx<GunFireWatcher+ListPool`1>)+10]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v229 @ rax_v19+B8]");
			object obj6 = 0;
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1809176D0");
		}

		static ListPool()
		{
			//IL_0045: Expected O, but got I
			//IL_005a: Expected O, but got I
			nint num = 0;
			object obj = null;
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180918060");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ rax_v12 (Il2CppRgctx<GunFireWatcher+ListPool`1>)+10]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v90 @ rax_v14+B8]");
			object obj3 = 0;
			obj3 = obj;
		}
	}

	private sealed class _003C_003Ec__DisplayClass12_0
	{
		public GunFireWatcher _003C_003E4__this;

		public GunController gun;

		internal void _003CSubscribe_003Eb__0()
		{
			GunFireWatcher gunFireWatcher = _003C_003E4__this;
			if ((object)_003C_003E4__this != null)
			{
				if (gunFireWatcher.onAnyGunFired != null)
				{
					gunFireWatcher.onAnyGunFired.Invoke();
				}
				if (gunFireWatcher.onAnyGunFiredWithGun != null)
				{
					gunFireWatcher.onAnyGunFiredWithGun.Invoke(gun);
				}
				return;
			}
			throw new NullReferenceException();
		}
	}

	private List<GunController> guns;

	private UnityEvent onAnyGunFired;

	private GunControllerEvent onAnyGunFiredWithGun;

	private bool periodicRescan;

	private float rescanIntervalSeconds;

	private readonly HashSet<GunController> subscribed;

	private float nextRescanTime;

	private void OnEnable()
	{
		RefreshSubscriptions();
		float unscaledTime = Time.unscaledTime;
		bool flag = !(0.05f < rescanIntervalSeconds);
		float num = 0.05f;
		if (!flag)
		{
			num = rescanIntervalSeconds;
		}
		float num2 = num + unscaledTime;
		nextRescanTime = num2;
	}

	private void OnDisable()
	{
		subscribed.Clear();
	}

	private void Update()
	{
		if (!periodicRescan)
		{
			return;
		}
		float unscaledTime = Time.unscaledTime;
		if (!(unscaledTime < nextRescanTime))
		{
			RefreshSubscriptions();
			float unscaledTime2 = Time.unscaledTime;
			bool flag = !(0.05f < rescanIntervalSeconds);
			float num = 0.05f;
			if (!flag)
			{
				num = rescanIntervalSeconds;
			}
			float num2 = num + unscaledTime2;
			nextRescanTime = num2;
		}
	}

	public void RefreshSubscriptions()
	{
		//IL_0306: Expected O, but got I4
		//IL_030f: Expected O, but got I4
		//IL_01bb: Expected O, but got I4
		//IL_01c4: Expected O, but got I4
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Expected O, but got Unknown
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Expected O, but got Unknown
		List<GunController> list = ListPool<GunController>.Get();
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18087B860");
		HashSet<GunController>.Enumerator enumerator = default(HashSet<GunController>.Enumerator);
		UnityEngine.Object obj = default(UnityEngine.Object);
		UnityEngine.Object obj4 = default(UnityEngine.Object);
		GunController gun = default(GunController);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (!(obj != null) || guns == null || !guns.Contains((GunController)obj))
				{
					if (list == null)
					{
						break;
					}
					list.Add((GunController)obj);
				}
				continue;
			}
			enumerator.Dispose();
			object obj2 = 0;
			object obj3 = 0;
			while ((nint)obj3 < list._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				if (obj4 != null && subscribed.Contains((GunController)obj4))
				{
					bool flag = subscribed.Remove((GunController)obj4);
				}
				obj2++;
				obj3 = obj2;
			}
			ListPool<GunController>.Release(list);
			if (guns == null)
			{
				return;
			}
			List<GunController> list2 = guns;
			object obj5 = 0;
			object obj6 = 0;
			while ((nint)obj6 < list2._size)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				_003C_003Ec__DisplayClass12_0 CS_0024_003C_003E8__locals9 = new _003C_003Ec__DisplayClass12_0();
				CS_0024_003C_003E8__locals9._003C_003E4__this = this;
				CS_0024_003C_003E8__locals9.gun = gun;
				if (CS_0024_003C_003E8__locals9.gun != null && !subscribed.Contains(CS_0024_003C_003E8__locals9.gun))
				{
					Action value = delegate
					{
						GunFireWatcher gunFireWatcher = CS_0024_003C_003E8__locals9._003C_003E4__this;
						if ((object)CS_0024_003C_003E8__locals9._003C_003E4__this != null)
						{
							if (gunFireWatcher.onAnyGunFired != null)
							{
								gunFireWatcher.onAnyGunFired.Invoke();
							}
							if (gunFireWatcher.onAnyGunFiredWithGun != null)
							{
								gunFireWatcher.onAnyGunFiredWithGun.Invoke(CS_0024_003C_003E8__locals9.gun);
							}
							return;
						}
						throw new NullReferenceException();
					};
					CS_0024_003C_003E8__locals9.gun.OnGunFired += value;
					subscribed.Add(CS_0024_003C_003E8__locals9.gun);
				}
				obj5++;
				list2 = guns;
				obj6 = obj5;
			}
			return;
		}
		throw new NullReferenceException();
	}

	private void Subscribe(GunController gun)
	{
		_003C_003Ec__DisplayClass12_0 CS_0024_003C_003E8__locals9 = new _003C_003Ec__DisplayClass12_0();
		CS_0024_003C_003E8__locals9._003C_003E4__this = this;
		CS_0024_003C_003E8__locals9.gun = gun;
		if (!(CS_0024_003C_003E8__locals9.gun != null) || subscribed.Contains(CS_0024_003C_003E8__locals9.gun))
		{
			return;
		}
		Action value = delegate
		{
			GunFireWatcher gunFireWatcher = CS_0024_003C_003E8__locals9._003C_003E4__this;
			if ((object)CS_0024_003C_003E8__locals9._003C_003E4__this != null)
			{
				if (gunFireWatcher.onAnyGunFired != null)
				{
					gunFireWatcher.onAnyGunFired.Invoke();
				}
				if (gunFireWatcher.onAnyGunFiredWithGun != null)
				{
					gunFireWatcher.onAnyGunFiredWithGun.Invoke(CS_0024_003C_003E8__locals9.gun);
				}
				return;
			}
			throw new NullReferenceException();
		};
		CS_0024_003C_003E8__locals9.gun.OnGunFired += value;
		subscribed.Add(CS_0024_003C_003E8__locals9.gun);
	}

	private void Unsubscribe(GunController gun)
	{
		if (gun != null && subscribed.Contains(gun))
		{
			bool flag = subscribed.Remove(gun);
		}
	}

	private void UnsubscribeAll()
	{
		subscribed.Clear();
	}

	private void HandleGunFired(GunController gun)
	{
		if (onAnyGunFired != null)
		{
			onAnyGunFired.Invoke();
		}
		if (onAnyGunFiredWithGun != null)
		{
			onAnyGunFiredWithGun.Invoke(gun);
		}
	}

	public GunFireWatcher()
	{
		List<GunController> list = new List<GunController>();
		guns = list;
		rescanIntervalSeconds = 0.5f;
		subscribed = new HashSet<GunController>();
		base._002Ector();
	}
}
