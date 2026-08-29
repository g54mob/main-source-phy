using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace GLTFast
{
	internal readonly struct MaterialsVariantsSlotInstances : IMaterialsVariantsSlotInstance
	{
		private readonly Renderer m_Renderer;

		private readonly IMaterialsVariantsSlot[] m_Slots;

		public MaterialsVariantsSlotInstances(Renderer renderer, IMaterialsVariantsSlot[] slots)
		{
			m_Renderer = renderer;
			m_Slots = slots;
		}

		public async Task ApplyMaterialsVariantAsync(int variantIndex, IMaterialProvider materialProvider, List<Material> materials, CancellationToken cancellationToken)
		{
			m_Renderer.GetSharedMaterials(materials);
			Dictionary<Task<Material>, int> getMaterialTasks = null;
			Task<Material> task = null;
			for (int i = 0; i < m_Slots.Length; i++)
			{
				int materialIndex = m_Slots[i].GetMaterialIndex(variantIndex);
				materials[i] = null;
				Task<Material> key;
				if (materialIndex < 0)
				{
					if (task == null)
					{
						task = materialProvider.GetDefaultMaterialAsync(cancellationToken);
					}
					key = task;
				}
				else
				{
					key = materialProvider.GetMaterialAsync(materialIndex, cancellationToken);
				}
				if (getMaterialTasks == null)
				{
					getMaterialTasks = new Dictionary<Task<Material>, int>();
				}
				getMaterialTasks[key] = i;
			}
			if (getMaterialTasks != null)
			{
				while (getMaterialTasks.Count > 0)
				{
					Task<Material> task2 = await Task.WhenAny(getMaterialTasks.Keys);
					materials[getMaterialTasks[task2]] = task2.Result;
					getMaterialTasks.Remove(task2);
				}
			}
			m_Renderer.SetSharedMaterials(materials);
		}
	}
}
