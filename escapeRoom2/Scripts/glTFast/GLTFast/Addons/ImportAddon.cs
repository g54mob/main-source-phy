namespace GLTFast.Addons
{
	public abstract class ImportAddon
	{
		public abstract void CreateImportInstance(GltfImportBase gltfImport);
	}
	public abstract class ImportAddon<TInstance> : ImportAddon where TInstance : ImportAddonInstance, new()
	{
		public override void CreateImportInstance(GltfImportBase gltfImport)
		{
			new TInstance().Inject(gltfImport);
		}
	}
}
