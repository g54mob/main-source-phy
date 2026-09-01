using UnityEngine;

public class CuttingPlanes : MonoBehaviour
{
	public MeshRenderer m_North;

	public MeshRenderer m_South;

	public MeshRenderer m_Floor;

	public static CuttingPlanes m_Instance;

	private MaterialPropertyBlock m_MaterialPropertyBlock;

	private readonly string COLOR_SHADER_ID = "Color_b791af217fb748d389c16c2b4c3d4591";

	private void Awake()
	{
		m_Instance = this;
		m_MaterialPropertyBlock = new MaterialPropertyBlock();
	}

	public void PositionCuttingPlanes()
	{
		m_North.transform.position = new Vector3(m_North.transform.position.x, m_North.transform.position.y, ZedAxisVehicles.DEFAULT_SPAWN_IN_Z);
		m_South.transform.position = new Vector3(m_South.transform.position.x, m_North.transform.position.y, ZedAxisVehicles.DEFAULT_SPAWN_OUT_Z);
		m_MaterialPropertyBlock.SetColor(COLOR_SHADER_ID, HeightFog.GetStartColor());
		m_North.SetPropertyBlock(m_MaterialPropertyBlock);
		m_South.SetPropertyBlock(m_MaterialPropertyBlock);
	}
}
