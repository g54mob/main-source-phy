using GLTFast.Export;
using UnityEngine;

namespace GLTFast.Documentation.Examples
{
	internal class SimpleExport : MonoBehaviour
	{
		[SerializeField]
		private string destinationFilePath;

		private async void Start()
		{
			GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("ExportMe");
			GameObjectExport gameObjectExport = new GameObjectExport();
			gameObjectExport.AddScene(gameObjects);
			if (!(await gameObjectExport.SaveToFileAndDispose(destinationFilePath)))
			{
				Debug.LogError("Something went wrong exporting a glTF");
			}
		}
	}
}
