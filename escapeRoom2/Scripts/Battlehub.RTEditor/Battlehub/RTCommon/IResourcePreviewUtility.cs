using System;
using UnityEngine;

namespace Battlehub.RTCommon
{
	public interface IResourcePreviewUtility
	{
		Vector3 PreviewObjectScale { get; set; }

		float PreviewScale { get; set; }

		int PreviewWidth { get; set; }

		int PreviewHeight { get; set; }

		Camera Camera { get; }

		bool CanCreatePreview(UnityEngine.Object obj);

		byte[] CreatePreviewData(UnityEngine.Object obj, bool instantiate = true);

		Texture2D CreatePreview(UnityEngine.Object obj, bool instantiate = true);

		Texture2D CreatePreview(GameObject obj, bool instantiate = true);

		[Obsolete("Use CreatePreivew(GameObject) instead")]
		Texture2D TakeSnapshot(GameObject go);
	}
}
