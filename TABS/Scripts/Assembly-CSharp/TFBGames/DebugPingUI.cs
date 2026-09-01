using System.Collections.Generic;
using Photon.Bolt;
using UnityEngine;
using UnityEngine.UI;

namespace TFBGames
{
	public class DebugPingUI : MonoBehaviour
	{
		private const int MaxPingToShow = 1000;

		[SerializeField]
		[Tooltip("Text for displaying the info.")]
		protected Text m_infoText;

		private INetworkService m_networkService;

		private Canvas m_canvas;

		private readonly string[] m_pingStrings = new string[1002];

		private void Awake()
		{
			if (base.transform.parent == null)
			{
				Object.DontDestroyOnLoad(base.gameObject);
			}
			m_canvas = GetComponentInParent<Canvas>();
			CreatePingStrings();
			UpdatePing();
		}

		private void Start()
		{
			m_networkService = ServiceLocator.GetService<INetworkService>();
		}

		private void Update()
		{
			UpdatePing();
		}

		private void CreatePingStrings()
		{
			int i = 0;
			for (int num = m_pingStrings.Length; i < num; i++)
			{
				if (i > 1000)
				{
					m_pingStrings[i] = $"Ping: {1000}+";
				}
				else
				{
					m_pingStrings[i] = $"Ping: {i}";
				}
			}
		}

		private void ShowCanvas(bool visible)
		{
			if (!(m_canvas == null) && m_canvas.enabled != visible)
			{
				m_canvas.enabled = visible;
			}
		}

		private void UpdatePing()
		{
			if (m_networkService == null || m_infoText == null)
			{
				ShowCanvas(visible: false);
				return;
			}
			float? num = null;
			if (m_networkService.IsConnected && m_networkService.IsClient && BoltNetwork.Server != null)
			{
				num = BoltNetwork.Server.PingNetwork;
			}
			else if (m_networkService.IsConnected && m_networkService.IsServer && BoltNetwork.Clients != null)
			{
				using (IEnumerator<BoltConnection> enumerator = BoltNetwork.Clients.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						BoltConnection current = enumerator.Current;
						num = current.PingNetwork;
					}
				}
			}
			ShowCanvas(num.HasValue);
			if (num.HasValue)
			{
				m_infoText.text = GetPingString(num.Value);
			}
		}

		private string GetPingString(float ping)
		{
			int value = (int)(ping * 1000f);
			value = Mathf.Clamp(value, 0, m_pingStrings.Length - 1);
			return m_pingStrings[value];
		}
	}
}
