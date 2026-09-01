using UnityEngine;

public class BridgeEffects
{
	private static ParticleSystem m_ErrorParticleSystem;

	public static void Init()
	{
		if (m_ErrorParticleSystem == null)
		{
			m_ErrorParticleSystem = InstantiateErrorFX();
			m_ErrorParticleSystem.Stop(withChildren: true, ParticleSystemStopBehavior.StopEmittingAndClear);
		}
	}

	public static void PlayErrorEffectAtFirstIllegalNodePosition()
	{
		Vector3 firstIllegalNodeOrEdgePosition = WorkshopSubmit.GetFirstIllegalNodeOrEdgePosition();
		if (!Mathf.Approximately(firstIllegalNodeOrEdgePosition.x, float.MaxValue))
		{
			MaybePlayErrorEffectAtPosition(firstIllegalNodeOrEdgePosition);
		}
		else
		{
			StopErrorFX();
		}
	}

	public static void StopErrorFX()
	{
		if ((bool)m_ErrorParticleSystem)
		{
			m_ErrorParticleSystem.Stop(withChildren: true, ParticleSystemStopBehavior.StopEmittingAndClear);
		}
	}

	public static void LoopErrorEffectAtPosition(Vector3 pos)
	{
		StopErrorFX();
		m_ErrorParticleSystem.transform.position = pos;
		m_ErrorParticleSystem.Play();
		ParticleSystem.MainModule main = m_ErrorParticleSystem.main;
		main.loop = true;
	}

	public static void MaybePlayErrorEffectAtPosition(Vector3 pos)
	{
		if (!m_ErrorParticleSystem.isPlaying && !Mathf.Approximately(pos.x, float.MaxValue))
		{
			m_ErrorParticleSystem.transform.position = pos;
			m_ErrorParticleSystem.Play();
			ParticleSystem.MainModule main = m_ErrorParticleSystem.main;
			main.loop = false;
		}
	}

	private static ParticleSystem InstantiateErrorFX()
	{
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_FillError);
		Object.DontDestroyOnLoad(gameObject);
		ParticleSystem component = gameObject.GetComponent<ParticleSystem>();
		ParticleSystem.MainModule main = component.main;
		main.useUnscaledTime = true;
		return component;
	}
}
