using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Animation/MMAnimationParameter")]
	public class MMAnimationParameter : MonoBehaviour
	{
		public string ParameterName;

		public Animator TargetAnimator;

		protected int _parameter;

		protected virtual void Awake()
		{
		}

		protected virtual void Initialization()
		{
		}

		public virtual void SetTrigger()
		{
		}

		public virtual void SetInt(int value)
		{
		}

		public virtual void SetFloat(float value)
		{
		}

		public virtual void SetBool(bool value)
		{
		}
	}
}
