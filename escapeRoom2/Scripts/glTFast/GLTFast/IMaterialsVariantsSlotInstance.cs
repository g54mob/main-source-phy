using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace GLTFast
{
	internal interface IMaterialsVariantsSlotInstance
	{
		Task ApplyMaterialsVariantAsync(int variantIndex, IMaterialProvider materialProvider, List<Material> materials, CancellationToken cancellationToken);
	}
}
