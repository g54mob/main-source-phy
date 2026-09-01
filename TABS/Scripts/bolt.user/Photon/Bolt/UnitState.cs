using System;
using UnityEngine;

namespace Photon.Bolt
{
	internal class UnitState : NetworkState, IUnitState, IState, IDisposable
	{
		public NetworkTransform MainTransform => Storage.Values[OffsetStorage].Transform;

		public int MovementSpeed
		{
			get
			{
				return Storage.Values[OffsetStorage + 3].Int0;
			}
			set
			{
				value = Mathf.Clamp(value, -1, 1);
				int @int = Storage.Values[OffsetStorage + 3].Int0;
				Storage.Values[OffsetStorage + 3].Int0 = value;
				if (NetworkValue.Diff(@int, value))
				{
					Storage.PropertyChanged(OffsetProperties + 1);
				}
			}
		}

		public int TargetShortNetworkId
		{
			get
			{
				return Storage.Values[OffsetStorage + 4].Int0;
			}
			set
			{
				value = Mathf.Clamp(value, -32768, 32767);
				int @int = Storage.Values[OffsetStorage + 4].Int0;
				Storage.Values[OffsetStorage + 4].Int0 = value;
				if (NetworkValue.Diff(@int, value))
				{
					Storage.PropertyChanged(OffsetProperties + 2);
				}
			}
		}

		public float LookDirectionAngle
		{
			get
			{
				return Storage.Values[OffsetStorage + 5].Float0;
			}
			set
			{
				value = Mathf.Clamp(value, -181f, 181f);
				float @float = Storage.Values[OffsetStorage + 5].Float0;
				Storage.Values[OffsetStorage + 5].Float0 = value;
				if (NetworkValue.Diff(@float, value))
				{
					Storage.PropertyChanged(OffsetProperties + 3);
				}
			}
		}

		public UnitState()
			: base(UnitState_Meta.Instance)
		{
		}
	}
}
