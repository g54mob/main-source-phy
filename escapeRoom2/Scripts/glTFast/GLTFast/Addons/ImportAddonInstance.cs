using System;

namespace GLTFast.Addons
{
	public abstract class ImportAddonInstance : IDisposable
	{
		public abstract bool SupportsGltfExtension(string extensionName);

		public abstract void Inject(GltfImportBase gltfImport);

		public abstract void Inject(IInstantiator instantiator);

		public abstract void Dispose();
	}
}
