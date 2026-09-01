using System;
using UnityEngine;

namespace MoreMountains.Tools
{
	[Serializable]
	public class MMLayer
	{
		[SerializeField]
		protected int _layerIndex;

		public virtual int LayerIndex => 0;

		public virtual int Mask => 0;

		public virtual void Set(int _layerIndex)
		{
		}
	}
}
