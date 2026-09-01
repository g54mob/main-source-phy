using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public class TrackRealtimeChangesOnLightHD : MonoBehaviour
{
	public const string ClassName = "TrackRealtimeChangesOnLightHD";

	private VolumetricLightBeamHD m_Master;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		VolumetricLightBeamHD master = default(VolumetricLightBeamHD);
		m_Master = master;
	}

	private void Update()
	{
		if (m_Master.enabled)
		{
			m_Master.AssignPropertiesFromAttachedSpotLight();
		}
	}
}
