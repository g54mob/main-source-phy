namespace GLTFast
{
	public interface IMaterialsVariantsProvider
	{
		int MaterialsVariantsCount { get; }

		string GetMaterialsVariantName(int index);
	}
}
