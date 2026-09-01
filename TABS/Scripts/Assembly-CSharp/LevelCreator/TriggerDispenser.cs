using System;
using TFBGames;
using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public class TriggerDispenser : MonoBehaviour, ITriggerable
	{
		[SerializeField]
		private GameObject m_projectilePrefab;

		[SerializeField]
		private Sprite m_icon;

		[SerializeField]
		private Transform m_spawnPosition;

		[SerializeField]
		private GameObject m_model;

		[SerializeField]
		private ParticleSystem m_fireEffect;

		[SerializeField]
		private string m_fireSoundRef;

		private ProjectilesSpawnManager m_spawnManager;

		private Image m_iconImage;

		private Vector3 m_lastPos;

		private bool m_animguard;

		private void Awake()
		{
			m_spawnManager = ServiceLocator.GetService<ProjectilesSpawnManager>();
			m_iconImage = GetComponentInChildren<Image>();
			m_iconImage.sprite = m_icon;
		}

		public void Trigger()
		{
			m_spawnManager.SpawnProjectile(m_projectilePrefab, m_spawnPosition.position, m_spawnPosition.rotation);
			if (m_animguard)
			{
				return;
			}
			m_animguard = true;
			m_model.transform.LeanMoveLocalZ(-0.2f, 0.01f).setOnComplete((System.Action)delegate
			{
				m_model.transform.LeanMoveLocalZ(0f, 0.15f).setOnComplete((System.Action)delegate
				{
					m_animguard = false;
				});
			});
			if (m_fireEffect != null)
			{
				m_fireEffect.Play();
			}
			Utility.PlaySound(m_fireSoundRef, 1f, base.transform);
		}
	}
}
