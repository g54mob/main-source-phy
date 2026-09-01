using System.Collections.Generic;
using Poly.Base;
using Poly.Physics;
using UnityEngine;

namespace Poly.Game
{
	[RequireComponent(typeof(Poly.Physics.Vehicle))]
	public class VehicleWithStuckRoadListener : TemplateForAudioListener, IWorldListener
	{
		public List<Poly.Physics.Rigidbody> allBodies = new List<Poly.Physics.Rigidbody>();

		private Dictionary<Transform, List<NormalAndDistance>> normalsPerRoad = new Dictionary<Transform, List<NormalAndDistance>>();

		public VehicleWithStuckRoadListener()
		{
			trackNormals = true;
		}

		internal void OnEnable()
		{
			if (!SingletonBehaviour<World>.instance || !SingletonBehaviour<World>.instance.areEdgesBreakable)
			{
				return;
			}
			foreach (Poly.Physics.Rigidbody allBody in allBodies)
			{
				allBody.collisionListeners.Add(this);
			}
			SingletonBehaviour<World>.instance.worldListeners.Add(this);
		}

		internal void OnDisable()
		{
			if ((bool)SingletonBehaviour<World>.instance && SingletonBehaviour<World>.instance.areEdgesBreakable)
			{
				foreach (Poly.Physics.Rigidbody allBody in allBodies)
				{
					allBody.collisionListeners.Remove(this);
				}
				if ((bool)SingletonBehaviour<World>.instance && SingletonBehaviour<World>.instance.worldListeners != null)
				{
					SingletonBehaviour<World>.instance.worldListeners.Remove(this);
				}
				Clear();
			}
			else
			{
				VerifyReset();
			}
		}

		public void BeforeStep()
		{
		}

		public void AfterWorldCleared()
		{
		}

		public void AfterWorldFrameUpdate()
		{
		}

		public void AfterWorldFixedUpdate()
		{
			Process();
		}

		protected override void Clear()
		{
			base.Clear();
		}

		private void Process()
		{
			SingletonBehaviour<World>.instance.areStuckRoadsBroken = false;
			normalsPerRoad.Clear();
			foreach (ContactData value3 in datas.Values)
			{
				if (value3.otherLayer != Layer.RoadEdge && value3.otherLayer != Layer.RoadEdgeConnectedToSplitNode)
				{
					continue;
				}
				normalsPerRoad.TryGetValue(value3.otherObject, out var value);
				if (value == null)
				{
					value = new List<NormalAndDistance>();
					normalsPerRoad.Add(value3.otherObject, value);
				}
				for (int i = 0; i < 2; i++)
				{
					NormalAndDistance normal = value3.GetNormal(i);
					if (normal.normal != Vec2.zero)
					{
						value.Add(normal);
					}
				}
			}
			foreach (KeyValuePair<Transform, List<NormalAndDistance>> item in normalsPerRoad)
			{
				Transform key = item.Key;
				List<NormalAndDistance> value2 = item.Value;
				for (int j = 0; j < value2.Count - 1; j++)
				{
					for (int k = j + 1; k < value2.Count; k++)
					{
						NormalAndDistance normalAndDistance = value2[j];
						ref Vec2 normal2 = ref normalAndDistance.normal;
						NormalAndDistance normalAndDistance2 = value2[k];
						float num = Vec2.Dot(in normal2, in normalAndDistance2.normal);
						float num2 = value2[j].distance + value2[k].distance;
						if (!(num < -0.9396926f) || !(num2 < -0.15f))
						{
							continue;
						}
						Edge component = key.GetComponent<Edge>();
						if ((bool)component && (bool)component.handle)
						{
							Debug.Log("Breaking road inside vehicle");
							EdgeHandle handle = component.handle;
							List<IEdgeBreakListener> edgeBreakListeners = SingletonBehaviour<World>.instance.edgeBreakListeners;
							bool flag = true;
							for (int l = 0; l < edgeBreakListeners.Count; l++)
							{
								IEdgeBreakListener edgeBreakListener = edgeBreakListeners[l];
								flag &= edgeBreakListener.OnEdgeBroken(handle);
							}
							SingletonBehaviour<World>.instance.areStuckRoadsBroken = true;
						}
					}
				}
			}
		}
	}
}
