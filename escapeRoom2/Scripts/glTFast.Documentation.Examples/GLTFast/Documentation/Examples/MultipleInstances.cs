using System;
using GLTFast.Newtonsoft;
using UnityEngine;

namespace GLTFast.Documentation.Examples
{
	internal class MultipleInstances : MonoBehaviour
	{
		public string Uri;

		[Range(1f, 10f)]
		public int quantity = 3;

		private async void Start()
		{
			_ = 2;
			try
			{
				GLTFast.Newtonsoft.GltfImport gltfImport = new GLTFast.Newtonsoft.GltfImport();
				await gltfImport.Load(Uri);
				for (int i = 0; i < quantity; i++)
				{
					GameObject obj = new GameObject($"glTF-{i}");
					obj.transform.localPosition = new Vector3(0f, 0f, (float)i * 0.13f);
					GameObject go = obj;
					GameObjectInstantiator instantiator = new GameObjectInstantiator(gltfImport, go.transform);
					await gltfImport.InstantiateMainSceneAsync(instantiator);
					MaterialsVariantsControl materialsVariantsControl = instantiator.SceneInstance.MaterialsVariantsControl;
					if (materialsVariantsControl != null)
					{
						go.AddComponent<MaterialsVariantsComponent>().Control = materialsVariantsControl;
						await materialsVariantsControl.ApplyMaterialsVariantAsync(i % gltfImport.MaterialsVariantsCount);
					}
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}
	}
}
