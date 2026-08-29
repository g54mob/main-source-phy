using GLTFast.Loading;
using GLTFast.Logging;
using GLTFast.Materials;
using GLTFast.Schema;

namespace GLTFast
{
	public class GltfImport : GltfImportBase<Root>
	{
		private static GltfJsonUtilityParser s_Parser;

		public GltfImport(IDownloadProvider downloadProvider = null, IDeferAgent deferAgent = null, IMaterialGenerator materialGenerator = null, ICodeLogger logger = null)
			: base(downloadProvider, deferAgent, materialGenerator, logger)
		{
		}

		protected override RootBase ParseJson(string json)
		{
			if (s_Parser == null)
			{
				s_Parser = new GltfJsonUtilityParser();
			}
			return s_Parser.ParseJson(json);
		}
	}
}
