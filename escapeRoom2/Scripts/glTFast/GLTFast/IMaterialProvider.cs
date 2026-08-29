using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace GLTFast
{
	public interface IMaterialProvider : IMaterialsVariantsProvider
	{
		Task<Material> GetMaterialAsync(int index);

		Task<Material> GetMaterialAsync(int index, CancellationToken cancellationToken);

		Task<Material> GetDefaultMaterialAsync();

		Task<Material> GetDefaultMaterialAsync(CancellationToken cancellationToken);

		IMaterialsVariantsSlot[] GetMaterialsVariantsSlots(int meshIndex, int meshNumeration);
	}
}
