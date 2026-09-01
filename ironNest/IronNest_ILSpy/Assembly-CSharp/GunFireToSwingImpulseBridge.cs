using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public sealed class GunFireToSwingImpulseBridge : MonoBehaviour
{
	private SwingController swingController;

	private List<GunController> gunsToWatch;

	private Vector2 impulseDirectionWorldXZ;

	private float impulseStrength;

	private float twistImpulseWorldY;

	private bool randomizeStrengthPerShot;

	private Vector2 strengthMultiplierMinMax;

	private bool randomizeDirectionPerShot;

	private float directionJitterDegrees;

	private void Reset()
	{
		if (this.swingController == null)
		{
			SwingController swingController = UnityEngine.Object.FindFirstObjectByType<SwingController>();
			this.swingController = swingController;
		}
	}

	private void OnEnable()
	{
		Subscribe(subscribe: true);
	}

	private void OnDisable()
	{
		Subscribe(subscribe: false);
	}

	private void Subscribe(bool subscribe)
	{
		//IL_00da: Expected O, but got I4
		//IL_00e3: Expected O, but got I4
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		List<GunController> list = gunsToWatch;
		object obj = 0;
		object obj2 = 0;
		UnityEngine.Object obj3 = default(UnityEngine.Object);
		while ((nint)obj2 < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (obj3 != null)
			{
				Action value = HandleGunFired;
				if (!subscribe)
				{
					((GunController)obj3).OnGunFired -= value;
				}
				else
				{
					((GunController)obj3).OnGunFired += value;
				}
			}
			list = gunsToWatch;
			obj++;
			obj2 = obj;
		}
	}

	private void HandleGunFired()
	{
		//IL_0058: Expected F4, but got I
		//IL_0058: Expected F4, but got O
		//IL_007e: Expected O, but got I
		//IL_00a5: Invalid comparison between O and F4
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Expected F4, but got Unknown
		bool flag = swingController == null;
		if (flag)
		{
			return;
		}
		if (randomizeStrengthPerShot != flag)
		{
			Vector2 vector = strengthMultiplierMinMax;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (GunFireToSwingImpulseBridge)+48]");
			float num = UnityEngine.Random.Range((float)vector, 0f);
		}
		if (randomizeDirectionPerShot)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (GunFireToSwingImpulseBridge)+34]");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (GunFireToSwingImpulseBridge)+34]");
			object obj = num2 * 0;
			object obj2 = impulseDirectionWorldXZ * impulseDirectionWorldXZ;
			object obj3 = obj + obj2;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-06f))
			{
				float num3 = directionJitterDegrees;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
				float minInclusive = num3 ^ 0;
				float num4 = UnityEngine.Random.Range(minInclusive, directionJitterDegrees);
				float num5 = num4 * ((float)Math.PI / 180f);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
			}
		}
		Vector2 worldXZImpulse = default(Vector2);
		swingController.TriggerExternalImpulse(worldXZImpulse, twistImpulseWorldY);
	}

	public GunFireToSwingImpulseBridge()
	{
		//IL_0010: Expected O, but got I4
		//IL_0026: Expected O, but got I4
		List<GunController> list = new List<GunController>();
		gunsToWatch = list;
		impulseDirectionWorldXZ = (Vector2)1065353216;
		impulseStrength = 1f;
		strengthMultiplierMinMax = (Vector2)1063675494;
		_ = 1066192077;
		directionJitterDegrees = 5f;
		base._002Ector();
	}
}
