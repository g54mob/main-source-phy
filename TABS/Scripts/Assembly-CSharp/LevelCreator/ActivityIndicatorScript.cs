using System;
using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public class ActivityIndicatorScript : MonoBehaviour
	{
		public Image ChunkUpdateImage;

		public Image FoliageUpdateImage;

		private void Update()
		{
			UpdateTimestamps? updateTimestamps = DMEditor.GetUpdateTimestamps();
			if (updateTimestamps.HasValue)
			{
				DateTime now = DateTime.Now;
				float num = Mathf.Sin(Time.realtimeSinceStartup * 10f) * 0.8f;
				ChunkUpdateImage.color = new Color(1f, 1f, 1f, Mathf.Clamp01(1.3f - 8f * (float)(now - updateTimestamps.Value.UpdateChunkTimestamp).TotalMilliseconds / 1000f - num));
				FoliageUpdateImage.color = new Color(1f, 1f, 1f, Mathf.Clamp01(1.3f - 8f * (float)(now - updateTimestamps.Value.UpdateFoliageTimestamp).TotalMilliseconds / 1000f - num));
			}
			else
			{
				ChunkUpdateImage.color = new Color(1f, 1f, 1f, 1f);
				FoliageUpdateImage.color = new Color(1f, 1f, 1f, 1f);
			}
		}
	}
}
