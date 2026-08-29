using System;
using System.IO;
using System.Threading.Tasks;
using GLTFast.Logging;
using UnityEngine;

namespace GLTFast.Documentation.Examples
{
	internal class LoadGltfFromMemory : MonoBehaviour
	{
		public string filePath;

		private async Task Start()
		{
			await LoadGltfFile();
		}

		private async Task LoadGltfFile()
		{
			byte[] data = await File.ReadAllBytesAsync(filePath);
			GltfImport gltf = new GltfImport(null, null, null, new ConsoleLogger());
			if (await gltf.Load(data, new Uri(filePath)))
			{
				await gltf.InstantiateMainSceneAsync(base.transform);
			}
		}
	}
}
