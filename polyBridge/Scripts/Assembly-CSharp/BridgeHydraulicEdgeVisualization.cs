using Poly.Extension;
using UnityEngine;

public class BridgeHydraulicEdgeVisualization : MonoBehaviour
{
	public Transform basePart;

	private static MaterialPropertyBlock m_MaterialPropertyBlock;

	private MeshRenderer[] m_MeshRenderers;

	public float basePartLength { get; internal set; }

	public bool isReversed { get; private set; }

	private void Awake()
	{
		if (m_MaterialPropertyBlock == null)
		{
			m_MaterialPropertyBlock = new MaterialPropertyBlock();
		}
		m_MeshRenderers = GetComponentsInChildren<MeshRenderer>();
	}

	public void Init(BridgeEdge bridgeEdge)
	{
		if ((bool)bridgeEdge.m_PhysicsEdge)
		{
			Object.Destroy(basePart.gameObject);
			return;
		}
		Piston pistonOnEdge = Pistons.GetPistonOnEdge(bridgeEdge);
		if ((bool)pistonOnEdge)
		{
			pistonOnEdge.m_Slider.GetNormalizedValue();
			float num = (float)Mathf.RoundToInt((pistonOnEdge.GetTargetLengthScale() - 1f) * 100f) / 100f;
			float length = bridgeEdge.GetLength();
			basePartLength = length * ((num < 0f) ? (1f + num) : 1f);
		}
	}

	public void UpdateTransform_Manual(BridgeEdge bridgeEdge)
	{
		if (!bridgeEdge.m_PhysicsEdge)
		{
			Init(bridgeEdge);
		}
		if (1E-12f < basePart.parent.lossyScale.x * basePart.parent.lossyScale.x)
		{
			basePart.SetLocalScaleX(basePartLength / basePart.parent.lossyScale.x);
			if (isReversed)
			{
				basePart.localPosition = Vector3.right * 0.5f;
				basePart.localRotation = Quaternion.Euler(0f, 0f, 180f);
			}
			else
			{
				basePart.localPosition = Vector3.left * 0.5f;
				basePart.localRotation = Quaternion.identity;
			}
		}
	}

	public void SetStressColorForEdge(BridgeEdge edge, Color stressColor)
	{
		m_MaterialPropertyBlock.SetColor(BridgeEdges.STRESS_COLOR_SHADER_ID, stressColor);
		MeshRenderer[] meshRenderers = m_MeshRenderers;
		for (int i = 0; i < meshRenderers.Length; i++)
		{
			meshRenderers[i].SetPropertyBlock(m_MaterialPropertyBlock);
		}
	}

	public void Desaturate(BridgeEdge edge, bool desaturate)
	{
		m_MaterialPropertyBlock.SetFloat(BridgeEdges.DESATURATE_SHADER_ID, desaturate ? 1f : 0f);
		MeshRenderer[] meshRenderers = m_MeshRenderers;
		for (int i = 0; i < meshRenderers.Length; i++)
		{
			meshRenderers[i].SetPropertyBlock(m_MaterialPropertyBlock);
		}
	}

	public void SetColor(Color c)
	{
		MeshRenderer[] meshRenderers = m_MeshRenderers;
		for (int i = 0; i < meshRenderers.Length; i++)
		{
			meshRenderers[i].material.color = c;
		}
	}
}
