using UnityEngine;

namespace BlockMapperInternal
{
	public abstract class ParameterWidget : MonoBehaviour
	{
		protected bool isEditing;

		public virtual void Init(int index, object parameter)
		{
			isEditing = true;
		}

		public virtual void Pick(GameObject obj)
		{
		}

		public virtual void ResetToPool()
		{
			isEditing = false;
		}
	}
}
