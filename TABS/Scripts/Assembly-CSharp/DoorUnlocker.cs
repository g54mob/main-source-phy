using System;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using TFBGames;
using UnityEngine;

public class DoorUnlocker : GameStateListener
{
	[Serializable]
	public class AnimatedMaterial
	{
		public float minValue;

		public float maxValue = 1f;

		public Material material;

		public string parameterName;

		public void SetValue(float v)
		{
			float value = Map(v, 0f, 1f, minValue, maxValue);
			material.SetFloat(parameterName, value);
		}

		public float Map(float value, float from1, float to1, float from2, float to2)
		{
			return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
		}
	}

	private RotationShake m_rotationShake;

	private Rigidbody m_secretObject;

	private bool m_restrictInGameMode;

	private float m_lookValue;

	private float m_unlockValue;

	public AudioClip hitClip;

	public Light light;

	private AudioSource loopSource;

	private Transform m_mainCamTransform;

	private bool done;

	public AnimatedMaterial[] animatedMaterials;

	public CaveDoorAnimation caveDoorAnimation;

	public GameObject unlockSparkEffect;

	protected override void Awake()
	{
		base.Awake();
		m_restrictInGameMode = ServiceLocator.GetService<GameModeService>().IsGameModeRestricted();
		if (m_mainCamTransform == null)
		{
			OnEnterNewScene();
		}
		for (int i = 0; i < animatedMaterials.Length; i++)
		{
			animatedMaterials[i].SetValue(0f);
		}
	}

	private void Update()
	{
		if (!(m_mainCamTransform != null) || !m_secretObject || done || m_restrictInGameMode)
		{
			return;
		}
		loopSource.volume = ((m_unlockValue <= 0f) ? 0f : Mathf.Pow(m_unlockValue * 0.25f, 1.3f));
		if (float.IsNaN(loopSource.volume))
		{
			loopSource.volume = 0f;
		}
		loopSource.pitch = 1f + 1.4f * m_unlockValue;
		if (m_unlockValue > 0f || m_lookValue > 10f)
		{
			SetColor();
		}
		float num = Vector3.Distance(m_secretObject.worldCenterOfMass, m_mainCamTransform.position);
		if (num > 5f)
		{
			m_unlockValue -= Time.unscaledDeltaTime * 0.2f;
			return;
		}
		float num2 = Vector3.Angle(m_mainCamTransform.forward, m_secretObject.worldCenterOfMass - m_mainCamTransform.position);
		m_lookValue = 1000f / (num * num2);
		if (m_lookValue > 8f)
		{
			float num3 = 0.1f;
			m_unlockValue += num3 * Time.unscaledDeltaTime;
			UnlockProgressFeedback();
			if (m_unlockValue > 1f)
			{
				UnlockSecret();
			}
		}
		else
		{
			m_unlockValue -= Time.unscaledDeltaTime * 0.2f;
		}
	}

	private void UnlockSecret()
	{
		if (!base.enabled || m_restrictInGameMode)
		{
			return;
		}
		if ((bool)ScreenShake.Instance)
		{
			ScreenShake.Instance.AddForce(Vector3.up * 8f, m_secretObject.transform.position);
		}
		if ((bool)unlockSparkEffect)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(unlockSparkEffect, m_secretObject.transform.position, m_secretObject.transform.rotation);
			gameObject.AddComponent<RemoveAfterSeconds>().seconds = 5f;
			MeshRenderer componentInChildren = m_secretObject.GetComponentInChildren<MeshRenderer>();
			if ((bool)componentInChildren)
			{
				ParticleSystem.ShapeModule shape = gameObject.GetComponent<ParticleSystem>().shape;
				shape.meshRenderer = componentInChildren;
			}
		}
		MeshRenderer[] componentsInChildren = GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = false;
		}
		loopSource.Stop();
		loopSource.volume = 1f;
		loopSource.PlayOneShot(hitClip);
		done = true;
		light.enabled = false;
		caveDoorAnimation.Animate();
		ServiceLocator.GetService<MusicHandler>().MuteMusic();
	}

	private void UnlockProgressFeedback()
	{
		if ((bool)m_rotationShake)
		{
			if (m_unlockValue <= 0f)
			{
				m_rotationShake.AddForce(UnityEngine.Random.onUnitSphere * 2f);
				m_unlockValue = 0f;
			}
			m_rotationShake.enabled = true;
			m_rotationShake.AddForce(UnityEngine.Random.onUnitSphere * m_unlockValue * Time.deltaTime * 50f);
		}
	}

	private void SetColor()
	{
		m_unlockValue = Mathf.Clamp(m_unlockValue, 0f, float.PositiveInfinity);
		for (int i = 0; i < animatedMaterials.Length; i++)
		{
			animatedMaterials[i].SetValue(m_unlockValue);
		}
		light.enabled = m_unlockValue > 0.01f;
		light.intensity = m_unlockValue;
	}

	public override void OnEnterNewScene()
	{
		base.OnEnterNewScene();
		loopSource = GetComponent<AudioSource>();
		if ((bool)loopSource)
		{
			loopSource.volume = 0f;
		}
		m_rotationShake = GetComponentInChildren<RotationShake>();
		m_secretObject = GetComponentInChildren<Rigidbody>();
		if ((bool)m_secretObject)
		{
			m_secretObject.isKinematic = true;
		}
		MainCam mainCam = ServiceLocator.GetService<PlayerCamerasManager>()?.GetMainCam(TFBGames.Player.One);
		m_mainCamTransform = ((mainCam != null) ? mainCam.transform : null);
		for (int i = 0; i < animatedMaterials.Length; i++)
		{
			animatedMaterials[i].SetValue(0f);
		}
	}

	public override void OnEnterBattleState()
	{
	}

	public override void OnEnterPlacementState()
	{
	}
}
