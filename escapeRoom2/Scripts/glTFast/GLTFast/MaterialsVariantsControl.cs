using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace GLTFast
{
	public class MaterialsVariantsControl : IMaterialsVariantsProvider
	{
		private IMaterialProvider m_MaterialProvider;

		private IReadOnlyCollection<IMaterialsVariantsSlotInstance> m_Slots;

		private int m_CurrentVariantIndex;

		public int MaterialsVariantsCount => m_MaterialProvider.MaterialsVariantsCount;

		internal MaterialsVariantsControl(IMaterialProvider materialProvider, IReadOnlyCollection<IMaterialsVariantsSlotInstance> slots)
		{
			m_MaterialProvider = materialProvider;
			m_Slots = slots;
		}

		public async Task ApplyMaterialsVariantAsync(int variantIndex, CancellationToken cancellationToken = default(CancellationToken))
		{
			List<Material> materials = new List<Material>();
			List<Task> list = new List<Task>();
			foreach (IMaterialsVariantsSlotInstance slot in m_Slots)
			{
				list.Add(slot.ApplyMaterialsVariantAsync(variantIndex, m_MaterialProvider, materials, cancellationToken));
			}
			await Task.WhenAll(list);
			m_CurrentVariantIndex = variantIndex;
		}

		public string GetMaterialsVariantName(int index)
		{
			return m_MaterialProvider.GetMaterialsVariantName(index);
		}
	}
}
