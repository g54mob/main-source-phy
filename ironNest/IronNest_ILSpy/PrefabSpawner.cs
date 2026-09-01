using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
	private GameObject[] prefabs;

	private Transform spawnPoint;

	private Transform spawnParent;

	private bool worldPositionStays;

	private int maxSpawnCount;

	private bool enableRandomPositionOffset;

	private Vector3 randomOffsetMin;

	private Vector3 randomOffsetMax;

	private bool debugTriggerSpawn;

	private int _spawnedCount;

	public int SpawnedCount => _spawnedCount;

	public unsafe void Spawn()
	{
		//IL_0008: Expected O, but got Ref
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Expected O, but got Unknown
		//IL_0118: Expected O, but got I4
		//IL_0121: Expected O, but got I4
		//IL_0090: Expected O, but got Ref
		//IL_01d4: Expected O, but got Ref
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Expected O, but got Unknown
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Expected O, but got Unknown
		//IL_07f0: Expected I, but got O
		//IL_0819: Expected F4, but got I
		//IL_0248: Expected F4, but got O
		//IL_0248: Expected F4, but got O
		//IL_0269: Expected F4, but got I
		//IL_0269: Expected F4, but got I
		//IL_02d7: Expected O, but got Ref
		//IL_02e5: Expected O, but got Ref
		//IL_028f: Expected F4, but got I
		//IL_028f: Expected F4, but got I
		//IL_03fd: Expected O, but got Ref
		//IL_0410: Expected O, but got Ref
		//IL_0438: Expected O, but got I
		//IL_0444: Expected F4, but got O
		//IL_039a: Expected O, but got Ref
		//IL_03ad: Expected O, but got Ref
		//IL_03ce: Expected F4, but got O
		//IL_03d8: Expected I, but got O
		//IL_0478: Expected O, but got I4
		//IL_04a3: Expected O, but got I4
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Expected O, but got Unknown
		//IL_0516: Expected O, but got Ref
		//IL_0659: Expected O, but got Ref
		//IL_056c: Expected O, but got Ref
		//IL_058c: Expected O, but got I4
		//IL_069f: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		if (prefabs != null)
		{
			GameObject[] array = prefabs;
			if (array.Length != 0)
			{
				if (maxSpawnCount > 0 && _spawnedCount >= maxSpawnCount)
				{
					string arg = base.name;
					object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
					_ = maxSpawnCount;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg2 = default(object);
					string message = $"[PrefabSpawner] '{arg}': Max spawn count ({arg2}) reached. Spawn cancelled.";
					Debug.Log(message, this);
					return;
				}
				GameObject[] array2 = prefabs;
				List<GameObject> list = new List<GameObject>(array2.Length);
				GameObject[] array3 = prefabs;
				object obj4 = prefabs + 32;
				object obj5 = 0;
				object obj6 = 0;
				while ((nint)obj6 < array3.Length)
				{
					if ((Object)obj4 != null)
					{
						list.Add((GameObject)obj4);
					}
					obj5++;
					obj4 += 8;
					obj6 = obj5;
				}
				if (list._size != 0)
				{
					int num = Random.Range(0, list._size);
					object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					Transform transform;
					if (spawnPoint != null)
					{
						transform = spawnPoint;
					}
					else
					{
						Transform transform2 = base.transform;
						transform = transform2;
					}
					Vector3 vector2 = default(Vector3);
					float num7;
					if (enableRandomPositionOffset)
					{
						float num2 = Random.Range((float)randomOffsetMin, (float)randomOffsetMax);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (PrefabSpawner)+48]");
						nint num3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (PrefabSpawner)+54]");
						float num4 = Random.Range(num3, 0f);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (PrefabSpawner)+4C]");
						nint num5 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (PrefabSpawner)+58]");
						float num6 = Random.Range(num5, 0f);
						Vector3 vector = vector2;
						num7 = num6;
					}
					else
					{
						nint num8 = (nint)typeof(Vector3);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v960 @ rax_v81 (Il2CppClass<UnityEngine.Vector3>)+B8]");
						nint num9 = 0;
						Vector3 vector = Vector3.zeroVector;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v961 @ rcx_v63 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
						num7 = 0f;
						_ = Vector3.zeroVector;
					}
					Quaternion rotation = transform.rotation;
					Vector3 vector3 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
					Quaternion quaternion = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
					_ = rotation.x;
					Vector3 vector4 = quaternion * vector3;
					Vector3 position = transform.position;
					_ = vector4.x;
					float x = position.x;
					float num10 = vector4.z + position.z;
					_ = position.x;
					GameObject gameObject = default(GameObject);
					if (spawnParent != null)
					{
						Quaternion rotation2 = transform.rotation;
						object obj8 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
						object obj9 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
						_ = rotation2.x;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180733CA0");
						float num11 = (float)vector2;
						nint num12 = (nint)spawnParent;
					}
					else
					{
						Quaternion rotation3 = transform.rotation;
						Quaternion rotation4 = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
						Vector3 position2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
						_ = rotation3.x;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+67]");
						gameObject = Object.Instantiate((GameObject)0, position2, rotation4);
						float num11 = (float)vector2;
						nint num12 = 0;
					}
					bool flag = spawnParent != null;
					bool flag2 = !flag;
					object obj10 = 0;
					if (!flag2)
					{
						bool flag3 = worldPositionStays;
						obj10 = 0;
						if (!flag3)
						{
							Transform transform3 = gameObject.transform;
							Vector3 localPosition = transform.localPosition;
							_ = localPosition.x;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-45]");
							Vector3 vector = 0 + vector2;
							x = localPosition.z + num7;
							Vector3 localPosition2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
							transform3.localPosition = localPosition2;
							Transform transform4 = gameObject.transform;
							Quaternion localRotation = transform.localRotation;
							float num11 = localRotation.x;
							Quaternion localRotation2 = (Quaternion)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
							_ = localRotation.x;
							transform4.localRotation = localRotation2;
							obj10 = 0;
						}
					}
					int spawnedCount = _spawnedCount + 1;
					_spawnedCount = spawnedCount;
					string[] array4 = new string[6] { "[PrefabSpawner] '", null, null, null, null, null };
					string text = base.name;
					array4[1] = text;
					array4[2] = "': Spawned '";
					string text2 = gameObject.name;
					array4[3] = text2;
					array4[4] = "' ";
					object obj11 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
					_ = _spawnedCount;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					string arg3;
					if (maxSpawnCount > 0)
					{
						object obj12 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 119));
						_ = maxSpawnCount;
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						object arg4 = default(object);
						arg3 = $"/{arg4}";
					}
					else
					{
						arg3 = "";
					}
					object arg5 = default(object);
					string text3 = $"(#{arg5}{arg3}).";
					array4[5] = text3;
					string message2 = string.Concat(array4);
					Debug.Log(message2, this);
				}
				else
				{
					string text4 = base.name;
					string message3 = "[PrefabSpawner] '" + text4 + "': All prefab slots are null. Spawn cancelled.";
					Debug.LogWarning(message3, this);
				}
				return;
			}
		}
		string text5 = base.name;
		string message4 = "[PrefabSpawner] '" + text5 + "': Prefab list is empty. Spawn cancelled.";
		Debug.LogWarning(message4, this);
	}

	public void ResetSpawnCount()
	{
		_spawnedCount = 0;
		string text = base.name;
		string message = "[PrefabSpawner] '" + text + "': Spawn count reset.";
		Debug.Log(message, this);
	}

	private unsafe Vector3 GetRandomLocalOffset()
	{
		//IL_0011: Expected F4, but got O
		//IL_0011: Expected F4, but got O
		//IL_0032: Expected F4, but got I
		//IL_0032: Expected F4, but got I
		//IL_0058: Expected F4, but got I
		//IL_0058: Expected F4, but got I
		//IL_0064: Expected native int or pointer, but got O
		//IL_0071: Expected native int or pointer, but got O
		//IL_007e: Expected native int or pointer, but got O
		float x = Random.Range((float)randomOffsetMin, (float)randomOffsetMax);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (PrefabSpawner)+48]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (PrefabSpawner)+54]");
		float y = Random.Range(num, 0f);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (PrefabSpawner)+4C]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (PrefabSpawner)+58]");
		float z = Random.Range(num2, 0f);
		Vector3 vector = default(Vector3);
		((Vector3*)(nint)vector)->x = x;
		((Vector3*)(nint)vector)->y = y;
		((Vector3*)(nint)vector)->z = z;
		return vector;
	}

	public PrefabSpawner()
	{
		//IL_001e: Expected I, but got O
		//IL_0059: Expected I, but got O
		worldPositionStays = true;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		randomOffsetMin = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ rdx_v1 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		nint num3 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rax_v5 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num4 = 0;
		randomOffsetMax = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rcx_v3 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		base._002Ector();
	}
}
