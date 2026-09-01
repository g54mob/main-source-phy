using Cpp2ILInjected;
using UnityEngine;
using VLB;

namespace VLB_Samples;

public class CameraToggleBeamVisibility : MonoBehaviour
{
	private KeyCode m_KeyCode = KeyCode.Space;

	private void Update()
	{
		if (Input.GetKeyDownInt(m_KeyCode))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Config instance = Config.Instance;
			int num = 1 << instance.geometryLayerID;
			Camera camera = default(Camera);
			int cullingMask = camera.cullingMask;
			int num2 = cullingMask & num;
			if (num2 != num)
			{
				int cullingMask2 = camera.cullingMask;
				int cullingMask3 = cullingMask2 | num;
				camera.cullingMask = cullingMask3;
			}
			else
			{
				int cullingMask4 = camera.cullingMask;
				int num3 = ~num;
				int cullingMask5 = num3 & cullingMask4;
				camera.cullingMask = cullingMask5;
			}
		}
	}
}
