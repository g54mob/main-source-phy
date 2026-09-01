using System.Collections.Generic;
using DM;
using Landfall.TABS;
using Landfall.TABS.GameState;
using TFBGames;
using UnityEngine;
using UnityEngine.Events;

public class SecretUnlock : GameStateListener
{
	[SerializeField]
	private string m_secret_key = "";

	[SerializeField]
	private string m_secretDescription = "";

	[SerializeField]
	private Sprite m_secretIcon;

	[SerializeField]
	private float m_distanceToUnlock = 5f;

	private RotationShake m_rotationShake;

	private Rigidbody m_secretObject;

	private float m_lookValue;

	private float m_unlockValue;

	public AudioClip hitClip;

	private AudioSource loopSource;

	private Transform m_mainCamTransform;

	public UnityEvent unlockEvent;

	public UnityEvent hideEvent;

	private bool done;

	public Color glowColor;

	public GameObject unlockSparkEffect;

	protected override void Awake()
	{
		base.Awake();
		if (m_mainCamTransform == null)
		{
			OnEnterNewScene();
		}
	}

	private void Update()
	{
		if (!(m_mainCamTransform != null) || !m_secretObject || done)
		{
			return;
		}
		loopSource.volume = ((m_unlockValue <= 0f) ? 0f : Mathf.Pow(m_unlockValue * 0.25f, 1.3f));
		if (float.IsNaN(loopSource.volume))
		{
			loopSource.volume = 0f;
		}
		float num = 1f + 1f * m_unlockValue;
		loopSource.pitch = ((num >= 0f) ? num : 0f);
		if (m_unlockValue > 0f || m_lookValue > 10f)
		{
			SetColor();
		}
		float num2 = Vector3.Distance(m_secretObject.worldCenterOfMass, m_mainCamTransform.position);
		if (num2 > m_distanceToUnlock)
		{
			m_unlockValue -= Time.unscaledDeltaTime * 0.2f;
			return;
		}
		float num3 = Vector3.Angle(m_mainCamTransform.forward, m_secretObject.worldCenterOfMass - m_mainCamTransform.position);
		m_lookValue = 1000f / (num2 * num3);
		if (m_lookValue > 8f)
		{
			float num4 = 0.2f;
			m_unlockValue += num4 * Time.unscaledDeltaTime;
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

	private void UnlockProgressFeedback()
	{
		if ((bool)m_rotationShake)
		{
			if (m_unlockValue <= 0f)
			{
				m_rotationShake.AddForce(Random.onUnitSphere * 2f);
				m_unlockValue = 0f;
			}
			m_rotationShake.enabled = true;
			m_rotationShake.AddForce(Random.onUnitSphere * m_unlockValue * Time.deltaTime * 50f);
		}
	}

	private void SetColor()
	{
		m_unlockValue = Mathf.Clamp(m_unlockValue, 0f, float.PositiveInfinity);
		Renderer[] componentsInChildren = m_secretObject.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Material[] materials = componentsInChildren[i].materials;
			foreach (Material material in materials)
			{
				if (!(material.shader.name == "TFBG/EmitVertexColor") && material.HasProperty("_EmissionColor"))
				{
					material.EnableKeyword("_EMISSION");
					material.SetColor("_EmissionColor", glowColor * m_unlockValue * 2f);
				}
			}
			componentsInChildren[i].materials = materials;
		}
	}

	private void UnlockSecret()
	{
		if (!base.enabled || string.IsNullOrWhiteSpace(m_secret_key) || ServiceLocator.GetService<ISaveLoaderService>().HasUnlockedSecret(m_secret_key))
		{
			return;
		}
		if ((bool)ScreenShake.Instance)
		{
			ScreenShake.Instance.AddForce(Vector3.up * 8f, m_secretObject.transform.position);
		}
		if ((bool)unlockSparkEffect)
		{
			GameObject gameObject = Object.Instantiate(unlockSparkEffect, m_secretObject.transform.position, m_secretObject.transform.rotation);
			gameObject.AddComponent<RemoveAfterSeconds>().seconds = 5f;
			MeshRenderer componentInChildren = m_secretObject.GetComponentInChildren<MeshRenderer>();
			if ((bool)componentInChildren)
			{
				ParticleSystem.ShapeModule shape = gameObject.GetComponent<ParticleSystem>().shape;
				shape.meshRenderer = componentInChildren;
			}
		}
		m_secretObject.gameObject.SetActive(value: false);
		unlockEvent?.Invoke();
		loopSource.Stop();
		loopSource.volume = 1f;
		loopSource.PlayOneShot(hitClip);
		done = true;
		if (string.IsNullOrWhiteSpace(m_secret_key))
		{
			return;
		}
		List<SecretUnlockCondition> list = ServiceLocator.GetService<ISaveLoaderService>().UnlockSecret(m_secret_key);
		CheckAchievements();
		ServiceLocator.GetService<ModalPanel>().OpenUnlockPanel(m_secretDescription, m_secretIcon);
		if (list != null && list.Count > 0)
		{
			foreach (SecretUnlockCondition item in list)
			{
				ServiceLocator.GetService<ModalPanel>().OpenUnlockPanel(item.m_unlockDescription, item.m_unlockImage);
			}
		}
		PlacementUI placementUI = Object.FindObjectOfType<PlacementUI>();
		if (placementUI != null)
		{
			placementUI.RedrawUI(m_secret_key);
		}
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
		if (!string.IsNullOrWhiteSpace(m_secret_key) && ServiceLocator.GetService<ISaveLoaderService>().HasUnlockedSecret(m_secret_key))
		{
			if ((bool)m_secretObject)
			{
				m_secretObject.gameObject.SetActive(value: false);
			}
			base.enabled = false;
			hideEvent?.Invoke();
		}
		MainCam mainCam = ServiceLocator.GetService<PlayerCamerasManager>()?.GetMainCam(TFBGames.Player.One);
		m_mainCamTransform = ((mainCam != null) ? mainCam.transform : null);
	}

	public override void OnEnterPlacementState()
	{
	}

	public override void OnEnterBattleState()
	{
	}

	public static void CheckAchievements()
	{
		AchievementService service = ServiceLocator.GetService<AchievementService>();
		ISaveLoaderService secretService = ServiceLocator.GetService<ISaveLoaderService>();
		if (HasUnlockedFaction(874593522))
		{
			service.UnlockAchievement("UNLOCKED_ALL_SECRET");
		}
		if (HasUnlockedFaction(673578412))
		{
			service.UnlockAchievement("UNLOCKED_ALL_LEGACY");
		}
		bool HasUnlockedFaction(int factionId)
		{
			UnitBlueprint[] units = ContentDatabase.Instance().GetFaction(new DatabaseID(-1, factionId)).Units;
			for (int i = 0; i < units.Length; i++)
			{
				string unlockKey = units[i].Entity.UnlockKey;
				if (!string.IsNullOrEmpty(unlockKey) && !secretService.HasUnlockedSecret(unlockKey))
				{
					return false;
				}
			}
			return true;
		}
	}
}
