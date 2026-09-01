using System;
using System.Xml.Serialization;

namespace Modding.Serialization
{
	[Serializable]
	[Reloadable]
	public class MeshTexturePair : Element, IReloadable
	{
		[XmlElement("Mesh")]
		[Reloadable]
		public MeshReference MeshReference;

		[XmlElement("Texture")]
		public ResourceReference TextureReference;

		[XmlIgnore]
		public ModMesh Mesh;

		[XmlIgnore]
		public ModTexture Texture;

		public void OnReload(IReloadable newObject)
		{
		}

		public void PreprocessForReloading()
		{
		}
	}
}
