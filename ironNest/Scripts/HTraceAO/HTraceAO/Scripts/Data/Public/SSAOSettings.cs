using System;
using HTraceAO.Scripts.Extensions;
using HTraceAO.Scripts.Globals;
using UnityEngine;

namespace HTraceAO.Scripts.Data.Public
{
	[Serializable]
	public class SSAOSettings
	{
		public DebugModeSSAO DebugModeSSAO;

		[SerializeField]
		private float _thickness;

		[SerializeField]
		private int _radius;

		[HExtensions.HRange(0f, 1f)]
		public float Thickness
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[HExtensions.HRange(1, 4)]
		public int Radius
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}
	}
}
