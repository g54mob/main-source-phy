using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace InternalModding.Blocks
{
	public static class BlockTypeIconCreator
	{
		internal static string GetThumbnailDirectory()
		{
			return Path.Combine(ModManager.DefaultModPath, "Thumbnails/Blocks/");
		}

		private static string GetThumbnailPath(ModdedBlock block)
		{
			string path = string.Format("{0}_{1}.png", block.Info.Mod.Info.Id, block.LocalId);
			return Path.Combine(GetThumbnailDirectory(), path);
		}

		public static IEnumerator CreateBlockThumbnail(ModdedBlock block)
		{
			string thumbnailsPath = GetThumbnailDirectory();
			if (!Directory.Exists(thumbnailsPath))
			{
				Directory.CreateDirectory(thumbnailsPath);
			}
			string thumbnailFile = GetThumbnailPath(block);
			yield return new WaitForEndOfFrame();
			yield return SingleInstanceFindOnly<BlockLoader>.Instance.StartCoroutine(SingleInstanceFindOnly<BlockLoader>.Instance.ThumbnailCreator.TakeThumbnail(block, thumbnailFile));
		}

		public static List<ModdedBlock> GetExistingThumbnails()
		{
			return SingleInstanceFindOnly<BlockLoader>.Instance.LoadedBlocks.Where((ModdedBlock b) => File.Exists(GetThumbnailPath(b))).ToList();
		}

		public static IEnumerator LoadThumbnails(IEnumerable<ModdedBlock> blocks)
		{
			foreach (ModdedBlock block in blocks)
			{
				yield return SingleInstanceFindOnly<BlockLoader>.Instance.StartCoroutine(LoadBlockThumbnail(block));
			}
		}

		private static IEnumerator LoadBlockThumbnail(ModdedBlock block)
		{
			WWW www = new WWW("file:///" + GetThumbnailPath(block));
			yield return www;
			block.BlockTypeIcon = www.texture;
			block.BlockTypeSprite = Sprite.Create(block.BlockTypeIcon, new Rect(0f, 0f, 256f, 256f), new Vector2(128f, 128f), 100f);
		}
	}
}
