using GLTFast.Newtonsoft;
using GLTFast.Newtonsoft.Schema;
using UnityEngine;

namespace GLTFast.Documentation.Examples
{
	internal class MyInstantiatorAddon
	{
		private GLTFast.Newtonsoft.GltfImport m_GltfImport;

		private GameObjectInstantiator m_Instantiator;

		public MyInstantiatorAddon(GLTFast.Newtonsoft.GltfImport gltfImport, GameObjectInstantiator instantiator)
		{
			m_GltfImport = gltfImport;
			m_Instantiator = instantiator;
			m_Instantiator.NodeCreated += OnNodeCreated;
			m_Instantiator.EndSceneCompleted += delegate
			{
				m_Instantiator.NodeCreated -= OnNodeCreated;
			};
		}

		private void OnNodeCreated(uint nodeIndex, GameObject gameObject)
		{
			UnclassifiedData unclassifiedData = (m_GltfImport.GetSourceRoot().Nodes[(int)nodeIndex] as Node)?.extras;
			if (unclassifiedData != null && unclassifiedData.TryGetValue<string>("some-extra-key", out var value))
			{
				gameObject.AddComponent<ExtraData>().someExtraKey = value;
			}
		}
	}
}
