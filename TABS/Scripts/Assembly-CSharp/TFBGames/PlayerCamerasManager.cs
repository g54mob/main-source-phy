using System;
using System.Collections.Generic;
using Landfall.TABS;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TFBGames
{
	public class PlayerCamerasManager : IService
	{
		private const int MaxLocalPlayers = 2;

		private Dictionary<Player, PlayerCamera> m_cameras = new Dictionary<Player, PlayerCamera>();

		public Dictionary<Player, PlayerCamera> Cameras => m_cameras;

		public int CamerasCount => m_cameras?.Count ?? 0;

		public event Action FoundPlayerCameras;

		public void OnAwake()
		{
		}

		public void OnStart()
		{
		}

		public void OnUpdate()
		{
		}

		public void OnFixedUpdate()
		{
		}

		public void OnLateUpdate()
		{
		}

		public void OnRegister()
		{
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		public void UnRegister()
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
			m_cameras.Clear();
		}

		public PlayerCamera GetCamera(Player player)
		{
			if (!m_cameras.ContainsKey(player))
			{
				return null;
			}
			return m_cameras[player];
		}

		public MainCam GetMainCam(Player player)
		{
			if (!m_cameras.ContainsKey(player))
			{
				return MainCam.instance;
			}
			return m_cameras[player].MainCam;
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			FindCameras();
		}

		private void FindCameras()
		{
			m_cameras.Clear();
			PlayerCamera[] array = UnityEngine.Object.FindObjectsOfType<PlayerCamera>();
			if (array == null || array.Length == 0)
			{
				this.FoundPlayerCameras?.Invoke();
				return;
			}
			int num = 0;
			int i = 0;
			PlayerCamera camera;
			for (int num2 = array.Length; i < num2; i++)
			{
				camera = array[i];
				AddCamera(camera, (Player)(num++), isClone: false);
			}
			int num3 = 2 - m_cameras.Count;
			if (num3 <= 0)
			{
				this.FoundPlayerCameras?.Invoke();
				return;
			}
			camera = m_cameras[Player.One];
			for (int j = 0; j < num3; j++)
			{
				PlayerCamera camera2 = UnityEngine.Object.Instantiate(camera, camera.transform.parent, worldPositionStays: true);
				AddCamera(camera2, (Player)(num++), isClone: true);
			}
			this.FoundPlayerCameras?.Invoke();
		}

		private void AddCamera(PlayerCamera camera, Player player, bool isClone)
		{
			camera.Initialize(isClone);
			MouseLook component = camera.GetComponent<MouseLook>();
			if (component != null)
			{
				component.enabled = true;
			}
			if (isClone)
			{
				camera.gameObject.SetActive(value: false);
				AudioListener componentInChildren = camera.gameObject.GetComponentInChildren<AudioListener>(includeInactive: true);
				if (componentInChildren != null)
				{
					UnityEngine.Object.Destroy(componentInChildren);
				}
			}
			m_cameras.Add(player, camera);
		}
	}
}
