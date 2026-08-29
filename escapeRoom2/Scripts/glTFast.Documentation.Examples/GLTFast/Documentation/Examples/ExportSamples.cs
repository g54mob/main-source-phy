using System.Threading.Tasks;
using GLTFast.Export;
using GLTFast.Logging;
using UnityEngine;
using UnityEngine.Serialization;

namespace GLTFast.Documentation.Examples
{
	internal class ExportSamples : MonoBehaviour
	{
		[FormerlySerializedAs("path")]
		[SerializeField]
		private string destinationFilePath;

		private async Task AdvancedExport()
		{
			CollectingLogger logger = new CollectingLogger();
			ExportSettings exportSettings = new ExportSettings
			{
				Format = GltfFormat.Binary,
				FileConflictResolution = FileConflictResolution.Overwrite,
				ComponentMask = ~(ComponentType.Animation | ComponentType.Camera),
				LightIntensityFactor = 100f,
				PreservedVertexAttributes = (VertexAttributeUsage.AllTexCoords | VertexAttributeUsage.Color)
			};
			GameObjectExportSettings gameObjectExportSettings = new GameObjectExportSettings();
			gameObjectExportSettings.OnlyActiveInHierarchy = false;
			gameObjectExportSettings.DisabledComponents = true;
			gameObjectExportSettings.LayerMask = LayerMask.GetMask("Default", "MyCustomLayer");
			GameObjectExportSettings gameObjectExportSettings2 = gameObjectExportSettings;
			GameObjectExport gameObjectExport = new GameObjectExport(exportSettings, gameObjectExportSettings2, null, null, logger);
			GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("ExportMe");
			gameObjectExport.AddScene(gameObjects, "My new glTF scene");
			if (!(await gameObjectExport.SaveToFileAndDispose(destinationFilePath)))
			{
				Debug.LogError("Something went wrong exporting a glTF");
				logger.LogAll();
			}
		}

		private void ExportSettingsDraco()
		{
			new ExportSettings
			{
				Compression = Compression.Draco,
				DracoSettings = new DracoExportSettings
				{
					positionQuantization = 12
				}
			};
		}

		private async void Start()
		{
			await LocalTransform();
		}

		private async Task LocalTransform()
		{
			GameObjectExport gameObjectExport = new GameObjectExport();
			gameObjectExport.AddScene(new GameObject[1] { base.gameObject }, base.gameObject.transform.worldToLocalMatrix, "Node at origin glTF scene");
			if (!(await gameObjectExport.SaveToFileAndDispose(destinationFilePath)))
			{
				Debug.LogError("Something went wrong exporting a glTF");
			}
		}
	}
}
