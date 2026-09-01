using Cpp2ILInjected;
using UnityEngine;

public class ClipboardToolSlot : MonoBehaviour
{
	public GameObject markerPrefab;

	public Transform restPose;

	public bool captureRestPoseOnAwake = true;

	public GameObject[] hoverVisualsOn;

	public GameObject[] hoverVisualsOff;

	public GameObject[] selectedVisualsOn;

	public GameObject[] selectedVisualsOff;

	public bool overrideMapCursorWhileSelected;

	public Texture2D selectedCursorTexture;

	public bool debugLogs;

	private Vector3 _003CCapturedRestLocalPosition_003Ek__BackingField;

	private Quaternion _003CCapturedRestLocalRotation_003Ek__BackingField;

	private Vector3 _003CCapturedRestLocalScale_003Ek__BackingField;

	public unsafe Vector3 CapturedRestLocalPosition
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			//IL_0024: Expected F4, but got I
			//IL_001f: Expected native int or pointer, but got O
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = (float)_003CCapturedRestLocalPosition_003Ek__BackingField;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (ClipboardToolSlot)+74]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		private set
		{
			//IL_000f: Expected O, but got F4
			_003CCapturedRestLocalPosition_003Ek__BackingField = (Vector3)value.x;
			_ = value.z;
		}
	}

	public unsafe Quaternion CapturedRestLocalRotation
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			Quaternion quaternion = default(Quaternion);
			((Quaternion*)(nint)quaternion)->x = (float)_003CCapturedRestLocalRotation_003Ek__BackingField;
			return quaternion;
		}
		private set
		{
			//IL_000f: Expected O, but got F4
			_003CCapturedRestLocalRotation_003Ek__BackingField = (Quaternion)value.x;
		}
	}

	public unsafe Vector3 CapturedRestLocalScale
	{
		get
		{
			//IL_000f: Expected F4, but got O
			//IL_000a: Expected native int or pointer, but got O
			//IL_0024: Expected F4, but got I
			//IL_001f: Expected native int or pointer, but got O
			Vector3 vector = default(Vector3);
			((Vector3*)(nint)vector)->x = (float)_003CCapturedRestLocalScale_003Ek__BackingField;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (ClipboardToolSlot)+90]");
			((Vector3*)(nint)vector)->z = 0f;
			return vector;
		}
		private set
		{
			//IL_000f: Expected O, but got F4
			_003CCapturedRestLocalScale_003Ek__BackingField = (Vector3)value.x;
			_ = value.z;
		}
	}

	public bool HasRestPoseTransform => restPose != null;

	private void Awake()
	{
		//IL_0165: Expected O, but got F4
		//IL_0192: Expected O, but got F4
		//IL_0078: Expected O, but got F4
		//IL_01c6: Expected O, but got F4
		//IL_00ad: Expected O, but got F4
		//IL_00e9: Expected O, but got F4
		string text;
		string text2;
		if (restPose == null && captureRestPoseOnAwake)
		{
			Transform transform = base.transform;
			Vector3 localPosition = transform.localPosition;
			_003CCapturedRestLocalPosition_003Ek__BackingField = (Vector3)localPosition.x;
			_ = localPosition.z;
			Transform transform2 = base.transform;
			_003CCapturedRestLocalRotation_003Ek__BackingField = (Quaternion)transform2.localRotation.x;
			Transform transform3 = base.transform;
			Vector3 localScale = transform3.localScale;
			bool flag = !debugLogs;
			_003CCapturedRestLocalScale_003Ek__BackingField = (Vector3)localScale.x;
			_ = localScale.z;
			if (!flag)
			{
				text = base.name;
				text2 = "] Captured rest pose from current local transform.";
				goto IL_0226;
			}
		}
		else if (restPose != null)
		{
			Vector3 localPosition2 = restPose.localPosition;
			_003CCapturedRestLocalPosition_003Ek__BackingField = (Vector3)localPosition2.x;
			_ = localPosition2.z;
			_003CCapturedRestLocalRotation_003Ek__BackingField = (Quaternion)restPose.localRotation.x;
			Vector3 localScale2 = restPose.localScale;
			bool flag2 = !debugLogs;
			_003CCapturedRestLocalScale_003Ek__BackingField = (Vector3)localScale2.x;
			_ = localScale2.z;
			if (!flag2)
			{
				text = base.name;
				text2 = "] Using assigned Rest Pose transform.";
				goto IL_0226;
			}
		}
		goto IL_0205;
		IL_0205:
		ApplyHover(hovered: false);
		ApplySelected(selected: false);
		return;
		IL_0226:
		string message = "[ClipboardToolSlot:" + text + text2;
		Debug.Log(message, this);
		goto IL_0205;
	}

	public void ApplyHover(bool hovered)
	{
		SetActiveBatch(hoverVisualsOn, hovered);
		bool active = (byte)((hovered ? 1u : 0u) ^ 1u) != 0;
		SetActiveBatch(hoverVisualsOff, active);
		if (debugLogs)
		{
			string text = base.name;
			string text2 = "ON";
			if (!hovered)
			{
				text2 = "OFF";
			}
			string message = "[ClipboardToolSlot:" + text + "] Hover=" + text2;
			Debug.Log(message, this);
		}
	}

	public void ApplySelected(bool selected)
	{
		SetActiveBatch(selectedVisualsOn, selected);
		bool active = (byte)((selected ? 1u : 0u) ^ 1u) != 0;
		SetActiveBatch(selectedVisualsOff, active);
		if (debugLogs)
		{
			string text = base.name;
			string text2 = "ON";
			if (!selected)
			{
				text2 = "OFF";
			}
			string message = "[ClipboardToolSlot:" + text + "] Selected=" + text2;
			Debug.Log(message, this);
		}
	}

	public unsafe void GetRestPose(out Vector3 localPos, out Quaternion localRot, out Vector3 localScale)
	{
		ref Vector3 reference = ref *(Vector3*)_003CCapturedRestLocalPosition_003Ek__BackingField;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ClipboardToolSlot)+74]");
		_ = 0;
		ref Quaternion reference2 = ref *(Quaternion*)_003CCapturedRestLocalRotation_003Ek__BackingField;
		ref Vector3 reference3 = ref *(Vector3*)_003CCapturedRestLocalScale_003Ek__BackingField;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ClipboardToolSlot)+90]");
		_ = 0;
	}

	private static void SetActiveBatch(GameObject[] gos, bool active)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_001c: Expected O, but got I4
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		if (gos == null)
		{
			return;
		}
		object obj = gos + 32;
		object obj2 = 0;
		while ((nint)obj2 < gos.Length)
		{
			if ((Object)obj != null)
			{
				((GameObject)obj).SetActive(active);
			}
			obj2++;
			obj += 8;
		}
	}
}
