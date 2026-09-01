using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public class Turret3DMimic : MonoBehaviour
{
	public enum Axis
	{
		X,
		Y,
		Z
	}

	public TurretController turretController;

	public Transform turretBase3D;

	public Transform[] barrelPivots;

	public GameObject[] muzzleFlashPrefabs;

	public Axis turretRotationAxis;

	public Axis barrelElevationAxis;

	public bool invertRotation;

	public bool invertElevation;

	public Vector3 turretRotationOffset;

	public Vector3 barrelElevationOffset;

	private void Update()
	{
		bool flag = turretController == null;
	}

	public void SetElevationMapping(float minElevation, float maxElevation)
	{
	}

	public unsafe void SyncTurret(float currentAngle)
	{
		//IL_005b: Expected O, but got Ref
		//IL_006f: Expected O, but got I4
		//IL_0081: Expected O, but got I4
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected F4, but got Unknown
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Expected O, but got Unknown
		//IL_0136: Expected O, but got I
		//IL_014b: Expected F4, but got I
		//IL_01c4: Expected O, but got Ref
		bool flag = !invertRotation;
		float angle = currentAngle;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			angle = currentAngle ^ 0;
		}
		Quaternion quaternion = RotationWithAxis(angle, turretRotationAxis);
		Vector3 euler = default(Vector3);
		Quaternion quaternion2 = Quaternion.Internal_FromEulerRad(ref euler);
		float num = default(float);
		turretBase3D.localRotation = (Quaternion)(&num);
		TurretController turretController = this.turretController;
		object obj = 0;
		Vector3 vector = default(Vector3);
		euler = vector;
		object obj2 = 0;
		UnityEngine.Object obj3 = default(UnityEngine.Object);
		Vector3 vector2 = default(Vector3);
		while (true)
		{
			List<GunController> guns = turretController.guns;
			if ((nint)obj2 >= guns._size)
			{
				break;
			}
			TurretController turretController2 = this.turretController;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj3 != null)
			{
				Transform[] array = barrelPivots;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_8_v4 (UnityEngine.Object)+28]");
				if ((nint)0 < (nint)array.Length)
				{
					Transform[] array2 = barrelPivots;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_8_v4 (UnityEngine.Object)+28]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_8_v4 (UnityEngine.Object)+BC]");
					float angle2 = 0f;
					if (invertElevation)
					{
						TurretController turretController3 = this.turretController;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ stack_8_v4 (UnityEngine.Object)+BC]");
						float num2 = 0f - turretController3.minBarrelElevation;
						angle2 = turretController3.maxBarrelElevation - num2;
					}
					Quaternion quaternion3 = RotationWithAxis(angle2, barrelElevationAxis);
					Quaternion quaternion4 = Quaternion.Internal_FromEulerRad(ref euler);
					array2[obj4].localRotation = (Quaternion)(&vector2);
					euler = vector;
				}
			}
			turretController = this.turretController;
			obj++;
			obj2 = obj;
		}
	}

	public void OnFireBarrel(int barrelIndex)
	{
		if (barrelIndex < 0)
		{
			return;
		}
		Transform[] array = barrelPivots;
		if (barrelIndex >= array.Length)
		{
			return;
		}
		GameObject[] array2 = muzzleFlashPrefabs;
		if (barrelIndex < array2.Length)
		{
			Transform[] array3 = barrelPivots;
			if (array2[barrelIndex] != null)
			{
				Vector3 position = array3[barrelIndex].position;
				Quaternion rotation = array3[barrelIndex].rotation;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180733CA0");
			}
		}
	}

	private unsafe Quaternion RotationWithAxis(float angle, Axis axis)
	{
		//IL_008f: Expected O, but got F4
		//IL_00c1: Expected native int or pointer, but got O
		//IL_002b: Expected O, but got I4
		//IL_0072: Expected O, but got I4
		//IL_0064: Expected O, but got I4
		//IL_00a2: Expected F4, but got O
		//IL_009d: Expected native int or pointer, but got O
		bool flag = axis == Axis.X;
		Quaternion quaternion = default(Quaternion);
		Vector3 euler;
		if (!flag)
		{
			object obj = axis - 1;
			if (!flag)
			{
				if ((nint)obj != 1)
				{
					((Quaternion*)(nint)quaternion)->x = (float)Quaternion.identityQuaternion;
					return quaternion;
				}
				euler = (Vector3)0;
			}
			else
			{
				euler = (Vector3)0;
			}
		}
		else
		{
			float num = angle * ((float)Math.PI / 180f);
			euler = (Vector3)num;
		}
		((Quaternion*)(nint)quaternion)->x = Quaternion.Internal_FromEulerRad(ref euler).x;
		return quaternion;
	}

	public Turret3DMimic()
	{
		//IL_001e: Expected I, but got O
		//IL_0059: Expected I, but got O
		turretRotationAxis = Axis.Y;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v27 @ rax_v2 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		turretRotationOffset = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v28 @ rdx_v1 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		nint num3 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v46 @ rax_v5 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num4 = 0;
		barrelElevationOffset = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rcx_v3 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		base._002Ector();
	}
}
