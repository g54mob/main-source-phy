using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	public class MMSceneLoadingTextProgress : MonoBehaviour
	{
		[Tooltip("the value to which the progress' zero value should be remapped to")]
		public float RemapMin;

		[Tooltip("the value to which the progress' one value should be remapped to")]
		public float RemapMax;

		[Tooltip("the amount of decimals to display")]
		public int NumberOfDecimals;

		protected Text _text;

		protected virtual void Awake()
		{
		}

		public virtual void SetProgress(float newValue)
		{
		}
	}
}
