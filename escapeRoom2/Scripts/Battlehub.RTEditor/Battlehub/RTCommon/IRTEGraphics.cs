using UnityEngine;
using UnityEngine.Rendering;

namespace Battlehub.RTCommon
{
	public interface IRTEGraphics
	{
		void RegisterCamera(Camera camera);

		void UnregisterCamera(Camera camera);

		IRTECamera GetOrCreateCamera(Camera camera, CameraEvent cameraEvent, bool meshesCache = true, bool renderersCache = true);

		IRTECamera CreateCamera(Camera camera, CameraEvent cameraEvent, bool meshesCache = false, bool renderersCache = false);

		IMeshesCache CreateSharedMeshesCache(CameraEvent cameraEvent);

		IRenderersCache CreateSharedRenderersCache(CameraEvent cameraEvent);

		void DestroySharedMeshesCache(IMeshesCache cache);

		void DestroySharedRenderersCache(IRenderersCache cache);
	}
}
