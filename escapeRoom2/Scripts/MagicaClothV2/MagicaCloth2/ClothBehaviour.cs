using UnityEngine;

namespace MagicaCloth2
{
	public abstract class ClothBehaviour : MonoBehaviour
	{
		public bool IsGizmoVisible { get; set; }

		public virtual int GetMagicaHashCode()
		{
			return 0;
		}

		protected virtual void OnDrawGizmos()
		{
			IsGizmoVisible = true;
		}
	}
}
