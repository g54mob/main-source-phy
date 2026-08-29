using System;
using GLTFast.Addons;
using GLTFast.Newtonsoft;
using UnityEngine;

namespace GLTFast.Documentation.Examples
{
	internal class CustomGltfImport : MonoBehaviour
	{
		public class MyAddon : ImportAddon<MyAddonInstance>
		{
		}

		public class MyAddonInstance : ImportAddonInstance
		{
			private GLTFast.Newtonsoft.GltfImport m_GltfImport;

			public override void Dispose()
			{
			}

			public override void Inject(GltfImportBase gltfImport)
			{
				if (gltfImport is GLTFast.Newtonsoft.GltfImport gltfImport2)
				{
					m_GltfImport = gltfImport2;
					gltfImport2.AddImportAddonInstance(this);
				}
			}

			public override void Inject(IInstantiator instantiator)
			{
				if (instantiator is GameObjectInstantiator instantiator2)
				{
					new MyInstantiatorAddon(m_GltfImport, instantiator2);
				}
			}

			public override bool SupportsGltfExtension(string extensionName)
			{
				return false;
			}
		}

		public string Uri;

		private async void Start()
		{
			_ = 1;
			try
			{
				ImportAddonRegistry.RegisterImportAddon(new MyAddon());
				GLTFast.Newtonsoft.GltfImport gltfImport = new GLTFast.Newtonsoft.GltfImport();
				await gltfImport.Load(Uri);
				await gltfImport.InstantiateMainSceneAsync(base.transform);
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}
	}
}
