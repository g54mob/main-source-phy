using UnityEngine;

[ExecuteInEditMode]
public class CuttingController_TwoPlanes : MonoBehaviour
{
	public GameObject plane1;

	public GameObject plane2;

	public Renderer rend;

	private Vector3 m_Normal_1;

	private Vector3 m_Position_1;

	private Vector3 m_Normal_2;

	private Vector3 m_Position_2;

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
		if ((bool)plane1)
		{
			m_Normal_1 = plane1.transform.TransformVector(new Vector3(0f, 0f, -1f));
			m_Position_1 = plane1.transform.position;
		}
		else
		{
			m_Normal_1 = Vector3.forward;
			m_Position_1 = new Vector3(0f, 0f, 1000f);
		}
		if ((bool)plane2)
		{
			m_Normal_2 = plane2.transform.TransformVector(new Vector3(0f, 0f, -1f));
			m_Position_2 = plane2.transform.position;
		}
		else
		{
			m_Normal_2 = -Vector3.forward;
			m_Position_2 = new Vector3(0f, 0f, -1000f);
		}
		if (m_MaterialPropertyBlock != null && rend != null)
		{
			m_MaterialPropertyBlock.SetVector(ShaderVariables_Common.CUTTING_PLANE_NORMAL_1, m_Normal_1);
			m_MaterialPropertyBlock.SetVector(ShaderVariables_Common.CUTTING_PLANE_POSITION_1, m_Position_1);
			m_MaterialPropertyBlock.SetVector(ShaderVariables_Common.CUTTING_PLANE_NORMAL_2, m_Normal_2);
			m_MaterialPropertyBlock.SetVector(ShaderVariables_Common.CUTTING_PLANE_POSITION_2, m_Position_2);
			rend.SetPropertyBlock(m_MaterialPropertyBlock);
		}
	}
}
