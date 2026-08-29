using System.Collections.Generic;
using UnityEngine;

namespace FullSail
{
	[ExecuteAlways]
	[HelpURL("http://www.macspeedee.com/full-sail/")]
	public class SailGroup : MonoBehaviour
	{
		public List<Sail> sailObjs = new List<Sail>();

		public List<SailParam> sailParams = new List<SailParam>();

		public bool dirty;

		public bool randomRipple = true;

		public int seed;

		public float minAnimOffset;

		public float maxAnimOffset = 10f;

		public List<SailParamRange> randomValues = new List<SailParamRange>();

		public bool showRange;

		private void Start()
		{
			SailShaderIDs.MakeIds();
			if (sailObjs.Count == 0)
			{
				Sail[] componentsInChildren = GetComponentsInChildren<Sail>();
				sailObjs = new List<Sail>(componentsInChildren);
				SetDirty();
			}
			if (randomRipple)
			{
				Random.InitState(seed);
				for (int i = 0; i < sailObjs.Count; i++)
				{
					if ((bool)sailObjs[i])
					{
						sailObjs[i].SetAnimOffset(Random.Range(minAnimOffset, maxAnimOffset));
					}
				}
			}
			if (!Application.isPlaying)
			{
				return;
			}
			for (int j = 0; j < randomValues.Count; j++)
			{
				if (randomValues[j].id == SailParamID.None || !randomValues[j].active)
				{
					continue;
				}
				for (int k = 0; k < sailObjs.Count; k++)
				{
					if ((bool)sailObjs[k])
					{
						SailParam sailParam = new SailParam();
						sailParam.id = randomValues[j].id;
						sailParam.active = true;
						sailParam.fval = Random.Range(randomValues[j].fmin, randomValues[j].fmax);
						sailObjs[k].sailParams.Add(sailParam);
					}
				}
			}
		}

		private void OnEnable()
		{
			SetDirty();
		}

		public void SetValue(SailParamID id, float val)
		{
			SailParam param = GetParam(id);
			if (param != null)
			{
				param.fval = val;
				dirty = true;
			}
		}

		public void SetValue(SailParamID id, Vector3 val)
		{
			SailParam param = GetParam(id);
			if (param != null)
			{
				param.vval = val;
				dirty = true;
			}
		}

		public void SetValue(SailParamID id, Vector4 val)
		{
			SailParam param = GetParam(id);
			if (param != null)
			{
				param.vval = val;
				dirty = true;
			}
		}

		public void SetValue(SailParamID id, Texture2D val)
		{
			SailParam param = GetParam(id);
			if (param != null)
			{
				param.tval = val;
				dirty = true;
			}
		}

		public void SetValue(SailParamID id, int val)
		{
			SailParam param = GetParam(id);
			if (param != null)
			{
				param.ival = val;
				dirty = true;
			}
		}

		public void SetValue(SailParamID id, Color val)
		{
			SailParam param = GetParam(id);
			if (param != null)
			{
				param.cval = val;
				dirty = true;
			}
		}

		public void SetDirty(bool _dirty = true)
		{
			dirty = _dirty;
		}

		public SailParam GetParam(SailParamID id)
		{
			for (int i = 0; i < sailParams.Count; i++)
			{
				if (sailParams[i].id == id)
				{
					return sailParams[i];
				}
			}
			return null;
		}

		private void Update()
		{
			for (int i = 0; i < sailObjs.Count; i++)
			{
				if ((bool)sailObjs[i])
				{
					sailObjs[i].DoUpdate(sailParams, dirty);
				}
			}
			dirty = false;
		}
	}
}
