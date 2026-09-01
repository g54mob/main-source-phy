using Poly.Physics;
using UnityEngine;

public class BridgeMaterial : MonoBehaviour
{
	public BridgeMaterialType m_MaterialType;

	public float m_PricePerMeter;

	public float m_MaxLength;

	public EdgeMaterial m_EdgeMaterial;

	public Material m_Material;

	public bool HasUnlimitedLength()
	{
		return m_MaxLength > 1000f;
	}
}
