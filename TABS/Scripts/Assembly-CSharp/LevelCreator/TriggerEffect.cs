using System.Collections.Generic;
using UnityEngine;

namespace LevelCreator
{
	public class TriggerEffect : MonoBehaviour, ITriggerable, ITriggerConnected, ITriggerDisconnected
	{
		private ParticleSystem[] m_particleSystems;

		private float m_startDelay;

		private float m_timer;

		private bool m_shouldLoop = true;

		private List<TriggerBox> m_connectedTriggers = new List<TriggerBox>();

		private void Awake()
		{
			m_particleSystems = GetComponentsInChildren<ParticleSystem>();
			m_startDelay = Random.Range(0f, 1f);
		}

		private void Update()
		{
			if (m_particleSystems == null || m_particleSystems.Length == 0 || !m_shouldLoop)
			{
				return;
			}
			bool flag = false;
			ParticleSystem[] particleSystems = m_particleSystems;
			for (int i = 0; i < particleSystems.Length; i++)
			{
				if (particleSystems[i].isPlaying)
				{
					flag = true;
					break;
				}
			}
			if (!flag && m_timer >= m_startDelay)
			{
				particleSystems = m_particleSystems;
				foreach (ParticleSystem particleSystem in particleSystems)
				{
					if (particleSystem != null)
					{
						particleSystem.Stop(withChildren: true);
						particleSystem.Play(withChildren: true);
					}
				}
				flag = true;
			}
			else
			{
				m_timer += Time.deltaTime;
			}
		}

		public void Trigger()
		{
			ParticleSystem[] particleSystems = m_particleSystems;
			foreach (ParticleSystem particleSystem in particleSystems)
			{
				if (particleSystem != null)
				{
					particleSystem.Play(withChildren: true);
				}
			}
		}

		public void OnTriggerConnected(TriggerBox trigger)
		{
			m_connectedTriggers.Add(trigger);
			m_shouldLoop = false;
		}

		public void OnTriggerDisconnected(TriggerBox trigger)
		{
			if (m_connectedTriggers.Contains(trigger))
			{
				m_connectedTriggers.Remove(trigger);
			}
			if (m_connectedTriggers.Count == 0)
			{
				m_shouldLoop = true;
			}
		}
	}
}
