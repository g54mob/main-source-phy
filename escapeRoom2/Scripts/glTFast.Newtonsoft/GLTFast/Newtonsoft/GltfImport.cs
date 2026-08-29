using GLTFast.Loading;
using GLTFast.Logging;
using GLTFast.Materials;
using GLTFast.Newtonsoft.Schema;
using GLTFast.Schema;
using Newtonsoft.Json;

namespace GLTFast.Newtonsoft
{
	public class GltfImport : GltfImportBase<GLTFast.Newtonsoft.Schema.Root>
	{
		public GltfImport(IDownloadProvider downloadProvider = null, IDeferAgent deferAgent = null, IMaterialGenerator materialGenerator = null, ICodeLogger logger = null)
			: base(downloadProvider, deferAgent, materialGenerator, logger)
		{
		}

		protected override RootBase ParseJson(string json)
		{
			return JsonConvert.DeserializeObject<GLTFast.Newtonsoft.Schema.Root>(json);
		}
	}
}
