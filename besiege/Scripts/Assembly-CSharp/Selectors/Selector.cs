using System.Collections.Generic;
using UnityEngine;

namespace Selectors
{
	public abstract class Selector : MonoBehaviour
	{
		public const string CONFLICT_TEXT = "●●●";

		protected bool isEditing;

		public virtual MapperType MapperType { get; set; }

		protected virtual void OnEdit()
		{
			BlockMapper currentInstance = BlockMapper.CurrentInstance;
			if (!(currentInstance == null))
			{
				SaveableDataHolder current = BlockMapper.CurrentInstance.Current;
				BlockMapper.OnEditField(current, MapperType);
			}
		}

		protected bool InConflict()
		{
			if (!BlockMapper.CurrentInstance || !BlockMapper.CurrentInstance.Block)
			{
				return false;
			}
			List<BlockBehaviour> machineSelection = AdvancedBlockEditor.Instance.selectionController.MachineSelection;
			for (int i = 0; i < machineSelection.Count; i++)
			{
				BlockBehaviour blockBehaviour = machineSelection[i];
				MapperType mapperType = blockBehaviour.GetMapperType("bmt-" + MapperType.Key);
				if (mapperType != null && mapperType != MapperType && !mapperType.CompareValue(MapperType))
				{
					return true;
				}
			}
			return false;
		}

		protected virtual void UpdateVisual()
		{
		}

		public virtual void Init()
		{
			isEditing = true;
		}

		public virtual void ResetToPool()
		{
			isEditing = false;
		}

		public virtual void Left()
		{
		}

		public virtual void Right()
		{
		}
	}
}
