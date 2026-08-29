using System;
using UnityEngine;

namespace FullSail
{
	[Serializable]
	public class SailParam
	{
		public SailParamID id;

		public bool active;

		public float fval;

		public Color cval;

		public Texture2D tval;

		public int ival;

		public Vector4 vval;
	}
}
