using System;
using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	[Obsolete("This component is obsolete, it's recommended to use MMProgressBar instead", true)]
	public class MMRadialProgressBar : MonoBehaviour
	{
		public float StartValue;

		public float EndValue;

		public float Tolerance;

		public string PlayerID;

		protected Image _radialImage;

		protected float _newPercent;

		protected virtual void Awake()
		{
		}

		public virtual void UpdateBar(float currentValue, float minValue, float maxValue)
		{
		}
	}
}
