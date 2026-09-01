using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB_Samples;

public class CheckIfInsideBeam : MonoBehaviour
{
	private bool isInsideBeam;

	private Material m_Material;

	private Collider m_Collider;

	private void Start()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Collider collider = default(Collider);
		m_Collider = collider;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		UnityEngine.Object obj = default(UnityEngine.Object);
		if ((bool)obj)
		{
			Material material = ((Renderer)obj).GetMaterial();
			m_Material = material;
		}
	}

	private unsafe void Update()
	{
		//IL_006c: Expected O, but got Ref
		if ((bool)m_Material)
		{
			if (isInsideBeam)
			{
			}
			object obj = default(object);
			m_Material.SetColor("_Color", (Color)(&obj));
		}
	}

	private void FixedUpdate()
	{
		isInsideBeam = false;
	}

	private unsafe void OnTriggerStay(Collider trigger)
	{
		//IL_00e7: Expected O, but got I
		//IL_0124: Invalid comparison between O and F4
		//IL_00a7: Expected O, but got Ref
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		UnityEngine.Object obj = default(UnityEngine.Object);
		if (!obj)
		{
			isInsideBeam = true;
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ stack_10_v3 (UnityEngine.Object)+CC]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ stack_10_v3 (UnityEngine.Object)+CC]");
		object obj2 = num * 0;
		object obj4 = default(object);
		object obj3 = obj4 * obj4;
		object obj5 = obj4 * obj4;
		object obj6 = obj3 + obj2;
		object obj7 = obj6 + obj5;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.5f))
		{
			Plane[] array = new Plane[1];
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ stack_10_v3 (UnityEngine.Object)+CC]");
			_ = 0;
			Bounds bounds = m_Collider.bounds;
			Span<Plane> span = new Span<Plane>(array);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18090CA50");
			object obj8 = default(object);
			Bounds bounds2 = default(Bounds);
			bool flag = GeometryUtility.Internal_TestPlanesAABB((ReadOnlySpan<Plane>)(&obj8), ref bounds2);
			isInsideBeam = flag;
		}
		else
		{
			isInsideBeam = true;
		}
	}
}
