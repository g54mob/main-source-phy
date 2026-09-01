using UnityEngine;

public class PortalShaderDataFeeder : MonoBehaviour
{
	public Material portalMaterial;

	private Transform m_cam;

	public void Update()
	{
		if (m_cam == null)
		{
			if (MainCam.instance == null)
			{
				return;
			}
			m_cam = MainCam.instance.transform;
		}
		Vector3 vector = m_cam.position - base.transform.position;
		float num = Vector3.Distance(m_cam.position, base.transform.position);
		num /= 80f;
		num = Mathf.Clamp01(1f - num);
		vector.Normalize();
		portalMaterial.SetVector("_CamForwardDir", vector);
		portalMaterial.SetFloat("_CamDistance", num);
	}
}
