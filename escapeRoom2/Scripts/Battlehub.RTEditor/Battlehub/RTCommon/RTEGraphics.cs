using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Battlehub.RTCommon
{
	[DefaultExecutionOrder(-60)]
	public class RTEGraphics : MonoBehaviour, IRTEGraphics
	{
		private class Data
		{
			public MonoBehaviour MonoBehaviour;

			public List<RTECamera> RTECameras;

			public CameraEvent Event;

			public Data(MonoBehaviour behaviour, CameraEvent cameraEvent, List<RTECamera> cameras)
			{
				MonoBehaviour = behaviour;
				Event = cameraEvent;
				RTECameras = cameras;
			}
		}

		private Dictionary<Camera, Dictionary<CameraEvent, RTECamera>> m_cameras = new Dictionary<Camera, Dictionary<CameraEvent, RTECamera>>();

		private readonly Dictionary<IMeshesCache, Data> m_meshesCache = new Dictionary<IMeshesCache, Data>();

		private readonly Dictionary<IRenderersCache, Data> m_renderersCache = new Dictionary<IRenderersCache, Data>();

		private void Awake()
		{
			IOC.RegisterFallback((IRTEGraphics)this);
		}

		private void OnDestroy()
		{
			IOC.UnregisterFallback((IRTEGraphics)this);
		}

		public void RegisterCamera(Camera camera)
		{
			foreach (KeyValuePair<IMeshesCache, Data> item in m_meshesCache)
			{
				IMeshesCache key = item.Key;
				Data value = item.Value;
				CreateRTECamera(camera.gameObject, value.Event, key, value.RTECameras);
			}
			foreach (KeyValuePair<IRenderersCache, Data> item2 in m_renderersCache)
			{
				IRenderersCache key2 = item2.Key;
				Data value2 = item2.Value;
				CreateRTECamera(camera.gameObject, value2.Event, key2, value2.RTECameras);
			}
			if (!m_cameras.ContainsKey(camera))
			{
				m_cameras.Add(camera, new Dictionary<CameraEvent, RTECamera>());
			}
		}

		public void UnregisterCamera(Camera camera)
		{
			foreach (KeyValuePair<IMeshesCache, Data> item in m_meshesCache)
			{
				Data value = item.Value;
				DestroyRTECameras(camera, value);
			}
			foreach (KeyValuePair<IRenderersCache, Data> item2 in m_renderersCache)
			{
				Data value2 = item2.Value;
				DestroyRTECameras(camera, value2);
			}
			if (!m_cameras.TryGetValue(camera, out var value3))
			{
				return;
			}
			foreach (RTECamera value4 in value3.Values)
			{
				((IRTECamera)value4).Destroy();
			}
			m_cameras.Remove(camera);
		}

		public IRTECamera GetOrCreateCamera(Camera camera, CameraEvent cameraEvent, bool meshesCache = true, bool renderersCache = true)
		{
			if (!m_cameras.TryGetValue(camera, out var value))
			{
				return null;
			}
			if (!value.TryGetValue(cameraEvent, out var value2))
			{
				value2 = _CreateCamera(camera, cameraEvent, meshesCache, renderersCache);
				value.Add(cameraEvent, value2);
			}
			return value2;
		}

		public IRTECamera CreateCamera(Camera camera, CameraEvent cameraEvent, bool createMeshesCache = false, bool createRenderersCache = false)
		{
			return _CreateCamera(camera, cameraEvent, createMeshesCache, createRenderersCache);
		}

		private RTECamera _CreateCamera(Camera camera, CameraEvent cameraEvent, bool createMeshesCache, bool createRenderersCache)
		{
			bool activeSelf = camera.gameObject.activeSelf;
			camera.gameObject.SetActive(value: false);
			RTECamera rTECamera = camera.gameObject.AddComponent<RTECamera>();
			rTECamera.Event = cameraEvent;
			if (createMeshesCache)
			{
				rTECamera.CreateMeshesCache();
			}
			if (createRenderersCache)
			{
				rTECamera.CreateRenderersCache();
			}
			camera.gameObject.SetActive(activeSelf);
			return rTECamera;
		}

		public IMeshesCache CreateSharedMeshesCache(CameraEvent cameraEvent)
		{
			MeshesCache meshesCache = base.gameObject.AddComponent<MeshesCache>();
			meshesCache.RefreshMode = CacheRefreshMode.Manual;
			List<RTECamera> list = new List<RTECamera>();
			foreach (Camera key in m_cameras.Keys)
			{
				CreateRTECamera(key.gameObject, cameraEvent, meshesCache, list);
			}
			m_meshesCache.Add(meshesCache, new Data(meshesCache, cameraEvent, list));
			return meshesCache;
		}

		public IRenderersCache CreateSharedRenderersCache(CameraEvent cameraEvent)
		{
			RenderersCache renderersCache = base.gameObject.AddComponent<RenderersCache>();
			List<RTECamera> list = new List<RTECamera>();
			foreach (Camera key in m_cameras.Keys)
			{
				CreateRTECamera(key.gameObject, cameraEvent, renderersCache, list);
			}
			m_renderersCache.Add(renderersCache, new Data(renderersCache, cameraEvent, list));
			return renderersCache;
		}

		public void DestroySharedMeshesCache(IMeshesCache cache)
		{
			if (m_meshesCache.TryGetValue(cache, out var value))
			{
				Object.Destroy(value.MonoBehaviour);
				for (int i = 0; i < value.RTECameras.Count; i++)
				{
					Object.Destroy(value.RTECameras[i]);
				}
				m_meshesCache.Remove(cache);
			}
		}

		public void DestroySharedRenderersCache(IRenderersCache cache)
		{
			if (m_renderersCache.TryGetValue(cache, out var value))
			{
				Object.Destroy(value.MonoBehaviour);
				for (int i = 0; i < value.RTECameras.Count; i++)
				{
					Object.Destroy(value.RTECameras[i]);
				}
				m_renderersCache.Remove(cache);
			}
		}

		private static void DestroyRTECameras(Camera camera, Data data)
		{
			List<RTECamera> rTECameras = data.RTECameras;
			for (int num = rTECameras.Count - 1; num >= 0; num--)
			{
				RTECamera rTECamera = rTECameras[num];
				if (rTECamera != null && rTECamera.gameObject == camera.gameObject)
				{
					Object.Destroy(rTECameras[num]);
					rTECameras.RemoveAt(num);
				}
			}
		}

		private static void CreateRTECamera(GameObject camera, CameraEvent cameraEvent, IMeshesCache cache, List<RTECamera> rteCameras)
		{
			bool activeSelf = camera.gameObject.activeSelf;
			camera.SetActive(value: false);
			RTECamera rTECamera = camera.AddComponent<RTECamera>();
			rTECamera.Event = cameraEvent;
			rTECamera.MeshesCache = cache;
			rteCameras.Add(rTECamera);
			camera.SetActive(activeSelf);
		}

		private static void CreateRTECamera(GameObject camera, CameraEvent cameraEvent, IRenderersCache cache, List<RTECamera> rteCameras)
		{
			bool activeSelf = camera.gameObject.activeSelf;
			camera.SetActive(value: false);
			RTECamera rTECamera = camera.AddComponent<RTECamera>();
			rTECamera.Event = cameraEvent;
			rTECamera.RenderersCache = cache;
			rteCameras.Add(rTECamera);
			camera.SetActive(activeSelf);
		}
	}
}
