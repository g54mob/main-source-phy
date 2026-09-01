using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	public class MMSceneLoadingImageProgress : MonoBehaviour
	{
		protected Image _image;

		protected virtual void Awake()
		{
		}

		public virtual void SetProgress(float newValue)
		{
		}
	}
}
