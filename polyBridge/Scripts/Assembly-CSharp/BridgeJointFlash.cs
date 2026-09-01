using System.Collections.Generic;
using UnityEngine;

public class BridgeJointFlash : MonoBehaviour
{
	private List<MeshRenderer> m_MeshRenderers = new List<MeshRenderer>();

	private MaterialPropertyBlock m_MaterialPropertyBlock;

	private float m_FlashingElapsedSeconds;

	private const float FLASH_DURATION_SECONDS = 0.4f;

	public void Awake()
	{
		MeshRenderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<MeshRenderer>(includeInactive: true);
		foreach (MeshRenderer item in componentsInChildren)
		{
			m_MeshRenderers.Add(item);
		}
		m_MaterialPropertyBlock = new MaterialPropertyBlock();
	}

	public void UpdateManual()
	{
		m_FlashingElapsedSeconds += Time.deltaTime;
		if (m_FlashingElapsedSeconds > 0.4f)
		{
			StopFlashing();
			return;
		}
		float t = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(m_FlashingElapsedSeconds / 0.4f));
		SetFlashColor(Color.Lerp(Color.yellow, Color.black, t));
	}

	public void Flash()
	{
		m_FlashingElapsedSeconds = 0f;
		if (!BridgeJoints.m_FlashingJoints.Contains(this))
		{
			BridgeJoints.m_FlashingJoints.Add(this);
			SimAudio.Play("sfx_simulation_hydraulic_connect", base.transform.position);
		}
	}

	public bool IsFlashing()
	{
		return BridgeJoints.m_FlashingJoints.Contains(this);
	}

	public void StopFlashing()
	{
		SetFlashColor(Color.black);
		if (BridgeJoints.m_FlashingJoints.Contains(this))
		{
			BridgeJoints.m_FlashingJoints.Remove(this);
		}
	}

	private void SetFlashColor(Color color)
	{
		m_MaterialPropertyBlock.SetColor("_EmissionColor", color);
		foreach (MeshRenderer meshRenderer in m_MeshRenderers)
		{
			meshRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
		}
	}
}
