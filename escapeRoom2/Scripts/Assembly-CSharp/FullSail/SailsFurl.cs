using System.Collections.Generic;
using UnityEngine;

namespace FullSail
{
	[HelpURL("http://www.macspeedee.com/full-sail/")]
	[ExecuteAlways]
	public class SailsFurl : MonoBehaviour
	{
		public class FurlVal
		{
			public float furl;

			public float cfurl;

			public float vel;

			public int order;

			public SailParam sp;
		}

		[Range(0f, 1f)]
		public float furl;

		public float speed = 1f;

		public SailGroup group;

		private int furlCount;

		private List<FurlVal> furlVals = new List<FurlVal>();

		private void Start()
		{
			if (!group)
			{
				group = GetComponent<SailGroup>();
			}
			if (!group)
			{
				return;
			}
			furlVals.Clear();
			SailParam param = group.GetParam(SailParamID.Furl);
			if (param != null)
			{
				SailParam param2 = group.GetParam(SailParamID.FurlOrder);
				if (param2 != null)
				{
					furlCount = param2.ival;
					FurlVal furlVal = new FurlVal();
					furlVal.order = param2.ival;
					furlVal.sp = param;
					furlVals.Add(furlVal);
				}
			}
			for (int i = 0; i < group.sailObjs.Count; i++)
			{
				if (!group.sailObjs[i])
				{
					continue;
				}
				param = group.sailObjs[i].GetParam(SailParamID.Furl);
				if (param == null)
				{
					continue;
				}
				SailParam param3 = group.sailObjs[i].GetParam(SailParamID.FurlOrder);
				if (param3 != null)
				{
					if (param3.ival > furlCount)
					{
						furlCount = param3.ival;
					}
					FurlVal furlVal2 = new FurlVal();
					furlVal2.order = param3.ival;
					furlVal2.sp = param;
					furlVals.Add(furlVal2);
				}
			}
		}

		private void Update()
		{
			if (!group)
			{
				return;
			}
			float num = furl * (float)(furlCount + 1);
			for (int i = 0; i < furlVals.Count; i++)
			{
				if ((float)furlVals[i].order < num)
				{
					furlVals[i].furl = 2f;
				}
				else
				{
					furlVals[i].furl = 0f;
				}
				if (Application.isPlaying)
				{
					furlVals[i].sp.fval = Mathf.SmoothDamp(furlVals[i].sp.fval, furlVals[i].furl, ref furlVals[i].vel, speed);
				}
				else
				{
					furlVals[i].sp.fval = furlVals[i].furl;
				}
			}
			group.SetDirty();
		}
	}
}
