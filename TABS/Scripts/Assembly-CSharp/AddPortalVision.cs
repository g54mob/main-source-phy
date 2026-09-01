using UnityEngine;

public class AddPortalVision : MonoBehaviour
{
	public AnimationCurve m_distanceCuve;

	public Transform m_portal;

	public Material m_portalVisionMaterial;

	private void Start()
	{
		MainCam.instance.gameObject.AddComponent<PortalVision>().Init(m_portalVisionMaterial, m_portal, m_distanceCuve);
	}
}
