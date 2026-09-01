using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class CullLayerCache : MonoBehaviour
{
	[Serializable]
	public sealed class CullItem
	{
		public UnityEngine.Object objectToCull;

		public int cullType;

		public int cullMask;

		public int cullWidth;

		public int cullHashA;

		public int cullHashB;

		public int cullMode;

		public ulong V
		{
			get
			{
				//IL_001f: Expected I8, but got I4
				int num = cullHashA << 32;
				return (ulong)(num | cullHashB);
			}
		}
	}

	public static CullLayerCache Instance;

	public UnityEngine.Object[] objectsToCull;

	public GameObject[] cullingRoots;

	public Renderer[] cullingTargets;

	public Camera cullingCamera;

	private LayerMask cullingLayers;

	private bool cacheOnAwake;

	private CullItem[] cullingItems;

	private int bakedHashA;

	private int bakedHashB;

	private int cachedCullMask;

	public int CachedCullMask => cachedCullMask;

	private void Awake()
	{
		Instance = this;
		if (cacheOnAwake)
		{
			int num = ReadCullMask();
			cachedCullMask = num;
		}
	}

	public int ReadCullMask()
	{
		bool flag = A0(0);
		bool flag2 = A0(1);
		bool flag3 = (byte)((flag ? 1u : 0u) | 2u) != 0;
		if (!flag2)
		{
			flag3 = flag;
		}
		bool flag4 = A1(2);
		bool flag5 = (byte)((flag3 ? 1u : 0u) | 4u) != 0;
		if (!flag4)
		{
			flag5 = flag3;
		}
		bool flag6 = A1(3);
		bool flag7 = (byte)((flag5 ? 1u : 0u) | 8u) != 0;
		if (!flag6)
		{
			flag7 = flag5;
		}
		bool flag8 = A2(4);
		bool flag9 = (byte)((flag7 ? 1u : 0u) | 0x10u) != 0;
		if (!flag8)
		{
			flag9 = flag7;
		}
		bool flag10 = A3();
		bool flag11 = (byte)((flag9 ? 1u : 0u) | 0x20u) != 0;
		bool flag12 = !flag10;
		if (!flag10)
		{
			flag11 = flag9;
		}
		int num = bakedHashA << 32;
		int num2 = num | bakedHashB;
		if (!flag12)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
			int l = default(int);
			ulong num3 = B0(this, l);
			if (num3 == (ulong)num2)
			{
				goto IL_01aa;
			}
		}
		flag11 = (byte)((flag11 ? 1u : 0u) | 0x40u) != 0;
		goto IL_01aa;
		IL_01aa:
		cachedCullMask = (flag11 ? 1 : 0);
		return flag11 ? 1 : 0;
	}

	public static int Read()
	{
		//IL_0072: Expected I4, but got O
		if (Instance == null)
		{
			return 0;
		}
		if ((object)Instance != null)
		{
			return Instance.ReadCullMask();
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	private unsafe bool A0(int t)
	{
		//IL_00b4: Expected O, but got Ref
		string dataPath = Application.dataPath;
		string path = Path.Combine(dataPath, "..");
		string fullPath = Path.GetFullPath(path);
		if (!string.IsNullOrEmpty(fullPath) && Directory.Exists(fullPath))
		{
			IEnumerable<string> enumerable = Directory.EnumerateFileSystemEntries(fullPath);
			if (enumerable == null)
			{
				throw new NullReferenceException();
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj2 = default(object);
			object obj = (object)(&obj2);
			CullLayerCache cullLayerCache = null;
			object obj3 = default(object);
			string path2 = default(string);
			while (true)
			{
				if (obj2 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					if (obj3 == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					string fileName = Path.GetFileName(path2);
					if (M0(fileName, t, z: false))
					{
						if (obj != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
						}
						return true;
					}
					continue;
				}
				throw new NullReferenceException();
			}
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
		}
		return false;
	}

	private bool A1(int t)
	{
		//IL_00e6: Expected I4, but got O
		//IL_0049: Expected O, but got I4
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A57D]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		AppDomain curDomain = AppDomain.getCurDomain();
		if (curDomain != null)
		{
			Assembly[] assemblies = curDomain.GetAssemblies();
			object obj = 0;
			while ((nint)obj < assemblies.Length)
			{
				AssemblyName assemblyName = assemblies[obj].GetName();
				bool flag = assemblyName.name == null;
				string s = "";
				if (!flag)
				{
					s = assemblyName.name;
				}
				if (M0(s, t, z: true))
				{
					return true;
				}
				obj++;
			}
			return false;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private bool A2(int t)
	{
		//IL_0021: Expected O, but got I4
		//IL_00c2: Expected I4, but got O
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		Process[] processes = Process.GetProcesses();
		bool flag = processes == null;
		object obj = 0;
		if (!flag)
		{
			while ((nint)obj < processes.Length)
			{
				string processName = processes[obj].ProcessName;
				if (!M0(processName, t, z: true))
				{
					obj++;
					continue;
				}
				return true;
			}
			return false;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private static bool A3()
	{
		return Debugger.IsAttached;
	}

	private bool A4()
	{
		//IL_0057: Expected O, but got I8
		int num = bakedHashA << 32;
		int num2 = num | bakedHashB;
		object obj = default(object);
		if (obj == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
			int l = default(int);
			ulong num3 = B0(this, l);
			object obj2 = (long)num3 - (long)num2;
			bool flag = obj2 == null;
			return !flag;
		}
		return true;
	}

	private bool M0(string s, int t, bool z)
	{
		//IL_0050: Expected O, but got I8
		//IL_005d: Expected O, but got I8
		//IL_0066: Expected O, but got I4
		//IL_006f: Expected O, but got I4
		//IL_008d: Expected O, but got I4
		//IL_0345: Expected I4, but got O
		//IL_00d5: Expected O, but got I
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Expected O, but got Unknown
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Expected O, but got Unknown
		//IL_026c: Expected O, but got I
		//IL_04be: Expected O, but got I4
		//IL_0312: Expected O, but got I8
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Expected I4, but got Unknown
		//IL_048b: Expected O, but got I8
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Expected I4, but got Unknown
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Expected O, but got Unknown
		//IL_02d8: Expected O, but got I
		//IL_02e5: Expected O, but got I8
		//IL_02ed: Expected O, but got I4
		//IL_023c: Expected O, but got I8
		string text = C0(s);
		if (text._stringLength != 0)
		{
			CullItem[] array = cullingItems;
			object obj = 1099511628211L;
			object obj2 = -3750763034362895579L;
			object obj3 = 32;
			object obj4 = 0;
			int num = t;
			CullLayerCache cullLayerCache = this;
			bool flag = z;
			for (object obj5 = 0; (nint)obj5 < array.Length; array = cullLayerCache.cullingItems, obj4++, obj3 += 8, obj5 = obj4)
			{
				CullItem[] array2 = cullLayerCache.cullingItems;
				if ((nint)obj4 < array2.Length)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rsi_v7 (CullItem[])+v70 @ r13_v7]");
					CullItem cullItem = (CullItem)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rsi_v7 (CullItem[])+v70 @ r13_v7]");
					if ((nint)0 == 0 || cullItem.cullType != num)
					{
						continue;
					}
					bool flag2 = cullItem.cullWidth == 0;
					if (cullItem.cullWidth <= 0 || flag2 || text._stringLength < cullItem.cullWidth)
					{
						continue;
					}
					if (!flag && cullItem.cullMode <= 0)
					{
						if (text._stringLength != cullItem.cullWidth)
						{
							continue;
						}
						int stringLength = text._stringLength;
						bool flag3 = text._stringLength <= 0;
						object obj6 = obj2;
						int num2 = 0;
						object obj7 = obj2;
						if (!flag3)
						{
							do
							{
								char c = text.get_Chars(num2);
								num2++;
								int num3 = obj7 ^ c;
								obj7 = num3 * obj;
							}
							while (num2 < stringLength);
							obj2 = -3750763034362895579L;
							cullLayerCache = this;
							obj6 = obj7;
						}
						int num4 = cullItem.cullHashA << 32;
						int num5 = num4 | cullItem.cullHashB;
						if ((nint)obj6 == num5)
						{
							goto IL_024e;
						}
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rsi_v7 (CullItem[])+v70 @ r13_v7]");
						CullItem cullItem2 = (CullItem)0;
						int num6 = 0;
						while (true)
						{
							object obj8 = text._stringLength - cullItem2.cullWidth;
							if (num6 > (nint)obj8)
							{
								break;
							}
							int stringLength = cullItem2.cullWidth + num6;
							bool flag4 = num6 >= stringLength;
							object obj9 = obj2;
							if (!flag4)
							{
								obj9 = obj2;
								int num7 = num6;
								do
								{
									char c2 = text.get_Chars(num7);
									num7++;
									int num8 = obj9 ^ c2;
									obj9 = num8 * 1099511628211L;
								}
								while (num7 < stringLength);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v61 @ rsi_v7 (CullItem[])+v70 @ r13_v7]");
								cullItem2 = (CullItem)0;
								obj2 = -3750763034362895579L;
								cullLayerCache = (CullLayerCache)num7;
							}
							int num9 = cullItem2.cullHashA << 32;
							int num10 = num9 | cullItem2.cullHashB;
							if ((nint)obj9 != num10)
							{
								num6++;
								continue;
							}
							goto IL_024e;
						}
						obj = 1099511628211L;
						cullLayerCache = this;
					}
					num = t;
					flag = z;
					continue;
				}
				IndexOutOfRangeException ex = new IndexOutOfRangeException();
				return (byte)(int)ex != 0;
				IL_024e:
				return true;
			}
		}
		return false;
	}

	private unsafe static ulong B0(CullLayerCache c, int l)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		//IL_0185: Expected O, but got I
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Expected O, but got Unknown
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Expected O, but got Unknown
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Expected O, but got Unknown
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Expected O, but got Unknown
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Expected I4, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected O, but got Unknown
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Expected I4, but got Unknown
		//IL_01cc: Expected O, but got I8
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Expected O, but got Unknown
		//IL_0270: Expected O, but got I
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Expected O, but got Unknown
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Expected O, but got Unknown
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Expected I4, but got Unknown
		//IL_02b7: Expected O, but got I8
		//IL_085b: Expected I8, but got O
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Expected Ref, but got Unknown
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_06fd: Expected O, but got I4
		//IL_03fb: Expected O, but got I4
		//IL_0361: Expected O, but got I
		//IL_0832: Expected I8, but got I
		//IL_05cb: Expected O, but got I4
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Expected O, but got Unknown
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Expected O, but got Unknown
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Expected I4, but got Unknown
		//IL_03a8: Expected O, but got I8
		//IL_046b: Expected O, but got I
		//IL_077f: Expected O, but got I
		//IL_0632: Expected O, but got I
		//IL_0921: Unknown result type (might be due to invalid IL or missing references)
		//IL_0926: Expected O, but got Unknown
		//IL_081b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0820: Expected O, but got Unknown
		//IL_06ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d3: Expected O, but got Unknown
		//IL_04cc: Expected O, but got I
		//IL_07e0: Expected O, but got I
		//IL_07ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f2: Expected Ref, but got Unknown
		//IL_0693: Expected O, but got I
		//IL_06a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a5: Expected Ref, but got Unknown
		//IL_04f2: Expected O, but got I
		//IL_0508: Expected O, but got I8
		//IL_0572: Expected O, but got I
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0580: Expected O, but got Unknown
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Expected I4, but got Unknown
		//IL_053d: Expected O, but got I8
		//IL_055d: Expected O, but got I8
		_ = 0;
		_ = 0;
		_ = -3750763034362895579L;
		GameObject gameObject = c.gameObject;
		Scene scene = gameObject.scene;
		object obj = default(object);
		Scene scene2 = (Scene)(obj + 40);
		if (((Scene*)scene2)->IsValid())
		{
			Scene scene3 = (Scene)(obj + 40);
			if (((Scene*)scene3)->isLoaded)
			{
				Scene scene4 = (Scene)(obj + 40);
				GameObject[] rootGameObjects = ((Scene*)scene4)->GetRootGameObjects();
				object obj2 = rootGameObjects + 32;
				int num = 0;
				int num2 = 0;
				while (num2 < rootGameObjects.Length)
				{
					if (num < rootGameObjects.Length)
					{
						Transform t = ((GameObject)obj2).transform;
						B1(t, l, ref *(ulong*)(obj - 40));
						num++;
						obj2 += 8;
						num2 = num;
						continue;
					}
					goto IL_084d;
				}
			}
		}
		string s = c.name;
		string text = C0(s);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-28]");
		object obj3 = 0;
		int num3 = 0;
		for (int num4 = 0; num4 < text._stringLength; num4 = num3)
		{
			char c2 = text.get_Chars(num3);
			int num5 = c2 ^ obj3;
			obj3 = num5 * 1099511628211L;
			num3++;
		}
		object obj4 = obj3 ^ 0xFF;
		object obj5 = obj4 * 1099511628211L;
		object obj6 = c + 64;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
		int num6 = obj + 64;
		string s2 = ((int*)num6)->ToString();
		string text2 = C0(s2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-28]");
		object obj7 = 0;
		int num7 = 0;
		for (int num8 = 0; num8 < text2._stringLength; num8 = num7)
		{
			char c3 = text2.get_Chars(num7);
			int num9 = c3 ^ obj7;
			obj7 = num9 * 1099511628211L;
			num7++;
		}
		object obj8 = obj7 ^ 0xFF;
		object obj9 = obj8 * 1099511628211L;
		if (c.cullingCamera != null)
		{
			string s3 = c.cullingCamera.name;
			string text3 = C0(s3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-28]");
			object obj10 = 0;
			int num10 = 0;
			for (int num11 = 0; num11 < text3._stringLength; num11 = num10)
			{
				char c4 = text3.get_Chars(num10);
				int num12 = c4 ^ obj10;
				obj10 = num12 * 1099511628211L;
				num10++;
			}
			object obj11 = obj10 ^ 0xFF;
			object obj12 = obj11 * 1099511628211L;
		}
		if (c.objectsToCull != null)
		{
			object obj13 = 32;
			int num13 = 0;
			int num14 = 0;
			while (true)
			{
				UnityEngine.Object[] array = c.objectsToCull;
				if (num13 >= array.Length)
				{
					break;
				}
				if (num14 < array.Length)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v996 @ rbx_v19+v130 @ rax_v46 (UnityEngine.Object[])]");
					if (!((UnityEngine.Object)0 != null))
					{
						goto IL_090a;
					}
					UnityEngine.Object[] array2 = c.objectsToCull;
					if (num14 < array2.Length)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v996 @ rbx_v19+v131 @ rax_v50 (UnityEngine.Object[])]");
						string s4 = ((UnityEngine.Object)0).name;
						string text4 = C0(s4);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-28]");
						object obj14 = 0;
						int num15 = 0;
						object obj15 = 1099511628211L;
						while (num15 < text4._stringLength)
						{
							char c5 = text4.get_Chars(num15);
							int num16 = c5 ^ obj14;
							obj14 = num16 * 1099511628211L;
							num15++;
							obj15 = 1099511628211L;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-20]");
						obj13 = 0;
						object obj16 = obj14 ^ 0xFF;
						object obj17 = obj16 * obj15;
						goto IL_090a;
					}
				}
				goto IL_084d;
				IL_090a:
				num14++;
				obj13 += 8;
				num13 = num14;
			}
		}
		if (c.cullingRoots != null)
		{
			int num17 = 0;
			object obj18 = 32;
			int num18 = 0;
			while (true)
			{
				GameObject[] array3 = c.cullingRoots;
				if (num18 >= array3.Length)
				{
					break;
				}
				if (num17 < array3.Length)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ rsi_v13+v133 @ rax_v37 (UnityEngine.GameObject[])]");
					if (!((UnityEngine.Object)0 != null))
					{
						goto IL_06b7;
					}
					GameObject[] array4 = c.cullingRoots;
					if (num17 < array4.Length)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ rsi_v13+v134 @ rax_v41 (UnityEngine.GameObject[])]");
						string s5 = ((UnityEngine.Object)0).name;
						D0(ref *(ulong*)(obj - 40), s5);
						goto IL_06b7;
					}
				}
				goto IL_084d;
				IL_06b7:
				num17++;
				obj18 += 8;
				num18 = num17;
			}
		}
		bool flag = c.cullingTargets == null;
		object obj19 = 32;
		int num19 = 0;
		if (!flag)
		{
			while (true)
			{
				Renderer[] array5 = c.cullingTargets;
				if (num19 >= array5.Length)
				{
					break;
				}
				if (num19 < array5.Length)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v135 @ rax_v28 (UnityEngine.Renderer[])+v61 @ r13_v6]");
					if (!((UnityEngine.Object)0 != null))
					{
						goto IL_0804;
					}
					Renderer[] array6 = c.cullingTargets;
					if (num19 < array6.Length)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v136 @ rax_v32 (UnityEngine.Renderer[])+v61 @ r13_v6]");
						string s6 = ((UnityEngine.Object)0).name;
						D0(ref *(ulong*)(obj - 40), s6);
						goto IL_0804;
					}
				}
				goto IL_084d;
				IL_0804:
				num19++;
				obj19 += 8;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-28]");
		return 0uL;
		IL_084d:
		IndexOutOfRangeException ex = new IndexOutOfRangeException();
		return (ulong)(long)ex;
	}

	private static void B1(Transform t, int l, ref ulong h)
	{
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Expected O, but got Unknown
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Expected O, but got Unknown
		GameObject gameObject = t.gameObject;
		int layer = gameObject.layer;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"bt r13d,eax\"");
		bool flag = (nint)gameObject >= 0;
		int num = 0;
		if (!flag)
		{
			string s = gameObject.name;
			D0(ref h, s);
			int layer2 = gameObject.layer;
			int num2 = default(int);
			string s2 = num2.ToString();
			D0(ref h, s2);
			string s3 = gameObject.tag;
			D0(ref h, s3);
			bool activeSelf = gameObject.activeSelf;
			bool flag2 = !activeSelf;
			string s4 = "0";
			if (!flag2)
			{
				s4 = "1";
			}
			D0(ref h, s4);
			Component[] components = gameObject.GetComponents<Component>();
			object obj = components + 32;
			int num3 = 0;
			int num4 = 0;
			string s5 = default(string);
			object obj3 = default(object);
			while (true)
			{
				bool flag3 = num4 >= components.Length;
				num = 0;
				if (flag3)
				{
					break;
				}
				if ((UnityEngine.Object)obj == null)
				{
					s5 = "0";
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
					object obj2 = obj3;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v384 @ rdx_v25+2D8] (should have been resolved before IL gen)");
				}
				D0(ref h, s5);
				num3++;
				obj += 8;
				num4 = num3;
			}
		}
		while (true)
		{
			int childCount = t.childCount;
			if (num < childCount)
			{
				Transform child = t.GetChild(num);
				B1(child, l, ref h);
				num++;
				continue;
			}
			break;
		}
	}

	private static string C0(string s)
	{
		//IL_003d: Expected O, but got I4
		//IL_008e: Expected O, but got I4
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_00bc: Expected O, but got I4
		if (!string.IsNullOrEmpty(s))
		{
			if (s != null)
			{
				char[] array = new char[s._stringLength];
				object obj = 0;
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				while (true)
				{
					if (num3 < s._stringLength)
					{
						char c = s.get_Chars(num);
						char c2 = char.ToLowerInvariant(c);
						object obj2 = c2 - 97;
						if ((nint)obj2 > 25)
						{
							object obj3 = c2 - 48;
							if ((nint)obj3 > 9)
							{
								goto IL_017b;
							}
						}
						num2++;
						object obj4 = obj + 1;
						if (array == null)
						{
							break;
						}
						array[obj] = c2;
						obj = obj4;
						goto IL_017b;
					}
					return ((string)null).CreateString(array, 0, num2);
					IL_017b:
					num++;
					num3 = num;
				}
			}
			return (string)(object)new NullReferenceException();
		}
		return "";
	}

	private static ulong H0(string s, int a, int n)
	{
		//IL_000d: Expected O, but got I4
		//IL_00b8: Expected I8, but got O
		//IL_0077: Expected I4, but got I8
		object obj = a + n;
		bool flag = a >= (nint)obj;
		ulong result = 14695981039346656037uL;
		if (!flag)
		{
			int num = a;
			do
			{
				if (s != null)
				{
					char c = s.get_Chars(num);
					num++;
					int num2 = (int)(-3750763034362895579L ^ (int)c);
					result = (ulong)(num2 * 1099511628211L);
					continue;
				}
				NullReferenceException ex = new NullReferenceException();
				return (ulong)(long)ex;
			}
			while (num < (nint)obj);
		}
		return result;
	}

	private unsafe static void D0(ref ulong h, string s)
	{
		//IL_0094: Expected O, but got I8
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		//IL_004c: Expected I4, but got I8
		//IL_005e: Expected O, but got I8
		string text = C0(s);
		int num = 0;
		ref ulong reference;
		for (int num2 = 0; num2 < text._stringLength; num2 = num)
		{
			char c = text.get_Chars(num);
			int num3 = (int)((long)(int)c ^ (long)h);
			object obj = num3 * 1099511628211L;
			num++;
			reference = ref *(ulong*)obj;
		}
		object obj2 = h ^ 0xFF;
		object obj3 = obj2 * 1099511628211L;
		reference = ref *(ulong*)obj3;
	}

	private static string P0()
	{
		string dataPath = Application.dataPath;
		string path = Path.Combine(dataPath, "..");
		return Path.GetFullPath(path);
	}

	public CullLayerCache()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
		LayerMask layerMask = default(LayerMask);
		cullingLayers = layerMask;
		cacheOnAwake = true;
		cullingItems = System.EmptyArray<CullItem>.Value;
		base._002Ector();
	}
}
