using UnityEngine;

[ExecuteInEditMode]
public class CuttingController_OnePlane : MonoBehaviour
{
	public GameObject m_Plane;

	public Renderer m_Renderer;

	private Vector3 m_Normal;

	private Vector3 m_Position;

	private MaterialPropertyBlock m_MaterialPropertyBlock;

	private void Awake()
	{
		m_MaterialPropertyBlock = new MaterialPropertyBlock();
	}

	private void Start()
	{
		UpdateShaderProperties();
	}

	public void UpdateShaderProperties()
	{
		if ((bool)m_Plane)
		{
			m_Normal = m_Plane.transform.up;
			m_Position = m_Plane.transform.position;
		}
		else
		{
			m_Normal = -Vector3.forward;
			m_Position = new Vector3(0f, 0f, -1000f);
		}
		if (m_MaterialPropertyBlock != null)
		{
			m_MaterialPropertyBlock.SetVector(ShaderVariables_Common.CUTTING_PLANE_NORMAL_1, m_Normal);
			m_MaterialPropertyBlock.SetVector(ShaderVariables_Common.CUTTING_PLANE_POSITION_1, m_Position);
			m_Renderer.SetPropertyBlock(m_MaterialPropertyBlock);
		}
	}
}
