using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class RaycastVisualizer : MonoBehaviour
{
	public Camera targetCamera;

	public float rayLength;

	public Color rayColor;

	private void Start()
	{
		if (targetCamera == null)
		{
			Camera main = Camera.main;
			targetCamera = main;
		}
	}

	private unsafe void Update()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0047: Expected O, but got Ref
		//IL_009f: Expected O, but got Ref
		//IL_00bc: Expected O, but got Ref
		//IL_00ca: Expected O, but got Ref
		//IL_0133: Expected O, but got Ref
		//IL_0171: Expected O, but got Ref
		//IL_019e: Expected O, but got Ref
		//IL_01b6: Expected O, but got Ref
		//IL_01ce: Expected O, but got Ref
		//IL_0201: Expected O, but got Ref
		//IL_022e: Expected O, but got Ref
		//IL_023c: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		if (targetCamera != null)
		{
			Vector3 mousePosition = Input.mousePosition;
			Vector3 pos = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 121));
			_ = mousePosition.x;
			_ = mousePosition.z;
			Ray ray = targetCamera.ScreenPointToRay(pos);
			object obj3 = default(object);
			float num = rayLength * (float)obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ rax_v9 (UnityEngine.Ray)+10]");
			_ = 0;
			Color color = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
			Vector3 dir = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 121));
			Vector3 start = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
			_ = rayColor;
			_ = ray.m_Origin;
			_ = ray.m_Origin;
			Debug.DrawRay(start, dir, color);
			_ = ray.m_Origin;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ rax_v9 (UnityEngine.Ray)+10]");
			_ = 0;
			ref RaycastHit hitInfo = ref System.Runtime.CompilerServices.Unsafe.As<object, RaycastHit>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
			Ray ray2 = (Ray)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
			if (Physics.Raycast(ray2, out hitInfo, rayLength))
			{
				RaycastHit raycastHit = (RaycastHit)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
				Vector3 point = ((RaycastHit*)raycastHit)->point;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C40]");
				_ = 0;
				Color color2 = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
				_ = point.x;
				Vector3 end = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
				_ = point.z;
				Vector3 start2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 121));
				_ = ray.m_Origin;
				_ = ray.m_Origin;
				Debug.DrawLine(start2, end, color2);
				RaycastHit raycastHit2 = (RaycastHit)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C40]");
				_ = 0;
				Vector3 point2 = ((RaycastHit*)raycastHit2)->point;
				Color color3 = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 89));
				Vector3 position = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 105));
				_ = point2.x;
				_ = point2.z;
				DebugExtension.DrawPoint(position, color3, 0.1f);
			}
		}
	}

	public RaycastVisualizer()
	{
		//IL_0012: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206D90]");
		rayColor = (Color)0;
		rayLength = 100f;
		base._002Ector();
	}
}
