using System.Collections.Generic;
using UnityEngine;

namespace TFBGames
{
	public class SplitScreenController : IService
	{
		private class CameraInfo
		{
			public GameObject Root;

			public Camera Camera;

			public Rect CameraDefaultRect;

			public bool IsClone;
		}

		private const int MaxLocalPlayers = 2;

		private PlayerCamerasManager m_playerCameras;

		private List<CameraInfo> m_cameraInfo = new List<CameraInfo>();

		private LocalMultiplayerGameRules gameModeSettings;

		public bool IsSplitScreenActive { get; private set; }

		public void OnRegister()
		{
		}

		public void OnAwake()
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

		public void OnStart()
		{
			m_playerCameras = ServiceLocator.GetService<PlayerCamerasManager>();
			m_playerCameras.FoundPlayerCameras += OnFoundPlayerCameras;
			gameModeSettings = ServiceLocator.GetService<LocalMultiplayerGameRules>();
		}

		public void UnRegister()
		{
			m_playerCameras.FoundPlayerCameras -= OnFoundPlayerCameras;
			m_cameraInfo.Clear();
		}

		public void StartSplitScreen()
		{
			if (IsSplitScreenActive || m_cameraInfo.Count <= 1)
			{
				return;
			}
			IsSplitScreenActive = true;
			int num = Mathf.Min(m_cameraInfo.Count, 2);
			for (int i = 0; i < num; i++)
			{
				CameraInfo cameraInfo = m_cameraInfo[i];
				if (i == 0)
				{
					Rect rect = new Rect(0f, 0f, 0.5f, 1f);
					Rect rect2 = new Rect(0f, 0f, 1f, 0.5f);
					cameraInfo.Camera.rect = ((gameModeSettings.SplitScreenStyle == SplitScreenStyle.Vertical) ? rect : rect2);
					m_playerCameras.Cameras[Player.One].SetPlayer(Player.One);
				}
				else
				{
					Rect rect3 = new Rect(0.5f, 0f, 0.5f, 1f);
					Rect rect4 = new Rect(0f, 0.5f, 1f, 0.5f);
					cameraInfo.Camera.rect = ((gameModeSettings.SplitScreenStyle == SplitScreenStyle.Vertical) ? rect3 : rect4);
					m_playerCameras.Cameras[Player.Two].SetPlayer(Player.Two);
					Transform transform = m_cameraInfo[0].Root.transform;
					cameraInfo.Root.transform.SetPositionAndRotation(transform.position, transform.rotation);
				}
				cameraInfo.Root.SetActive(value: true);
			}
		}

		public void EndSplitScreen()
		{
			if (!IsSplitScreenActive || m_cameraInfo.Count <= 1)
			{
				return;
			}
			IsSplitScreenActive = false;
			int i = 0;
			for (int count = m_cameraInfo.Count; i < count; i++)
			{
				CameraInfo cameraInfo = m_cameraInfo[i];
				if (cameraInfo != null && !(cameraInfo.Root == null) && !(cameraInfo.Camera == null))
				{
					cameraInfo.Camera.rect = cameraInfo.CameraDefaultRect;
					if (cameraInfo.IsClone)
					{
						cameraInfo.Root.SetActive(value: false);
					}
				}
			}
			if (m_playerCameras.Cameras != null && m_playerCameras.Cameras.TryGetValue(Player.One, out var value))
			{
				value.SetPlayer(Player.Any);
			}
		}

		private void OnFoundPlayerCameras()
		{
			FindCameras();
		}

		private void FindCameras()
		{
			if (IsSplitScreenActive)
			{
				EndSplitScreen();
			}
			m_cameraInfo.Clear();
			int camerasCount = m_playerCameras.CamerasCount;
			for (int i = 0; i < camerasCount; i++)
			{
				PlayerCamera camera = m_playerCameras.GetCamera((Player)i);
				AddCameraInfo(camera);
			}
		}

		private void AddCameraInfo(PlayerCamera camera)
		{
			if (camera.IsClone)
			{
				camera.gameObject.SetActive(value: false);
			}
			m_cameraInfo.Add(new CameraInfo
			{
				Root = camera.gameObject,
				Camera = camera.Camera,
				CameraDefaultRect = camera.Camera.rect,
				IsClone = camera.IsClone
			});
		}
	}
}
