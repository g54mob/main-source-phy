using System;
using System.Collections.Generic;
using UnityEngine;

namespace Poly.Determinism
{
	[Serializable]
	public class FrameData
	{
		public float timeElapsed;

		public float deltaTime;

		public List<EventData> events = new List<EventData>();

		public List<NodeData> nodes = new List<NodeData>();

		public List<MotionData> motions = new List<MotionData>();

		public bool IsIdenticalTo(FrameData other)
		{
			bool flag = true;
			flag &= timeElapsed == other.timeElapsed;
			flag &= deltaTime == other.deltaTime;
			flag &= events.Count == other.events.Count;
			flag &= nodes.Count == other.nodes.Count;
			flag &= motions.Count == other.motions.Count;
			if (flag)
			{
				for (int i = 0; i < events.Count; i++)
				{
					flag &= events[i] == other.events[i];
				}
				for (int j = 0; j < nodes.Count; j++)
				{
					flag &= nodes[j] == other.nodes[j];
				}
				for (int k = 0; k < motions.Count; k++)
				{
					flag &= motions[k] == other.motions[k];
				}
			}
			return flag;
		}

		public void LogDifferences(FrameData other, List<string> log)
		{
			if (deltaTime != other.deltaTime)
			{
				log.Add("Delta time difference: " + deltaTime + " vs " + other.deltaTime);
			}
			if (timeElapsed != other.timeElapsed)
			{
				log.Add("Elapsed time difference: " + timeElapsed + " vs " + other.timeElapsed);
			}
			if (events.Count != other.events.Count)
			{
				log.Add("Num events different: " + events.Count + " vs " + other.events.Count);
			}
			int num = System.Math.Max(events.Count, other.events.Count);
			for (int i = 0; i < num; i++)
			{
				EventData eventData = ((i < events.Count) ? events[i] : default(EventData));
				EventData eventData2 = ((i < other.events.Count) ? other.events[i] : default(EventData));
				if (eventData != eventData2)
				{
					log.Add("Event #" + i + " different: " + eventData.dataString + " vs " + eventData2.dataString);
				}
			}
			if (nodes.Count != other.nodes.Count)
			{
				log.Add("Num nodes different: " + nodes.Count + " vs " + other.nodes.Count);
			}
			System.Math.Max(nodes.Count, other.nodes.Count);
			for (int j = 0; j < nodes.Count; j++)
			{
				NodeData nodeData = ((j < nodes.Count) ? nodes[j] : default(NodeData));
				NodeData nodeData2 = ((j < other.nodes.Count) ? other.nodes[j] : default(NodeData));
				if (nodeData != nodeData2)
				{
					Vector2s vector2s = nodeData.pos - nodeData2.pos;
					bool flag = nodeData.invMass == nodeData2.invMass;
					log.Add($"Node @{j} at {nodeData.dataString} offset from #{nodeData2.objectId} " + ((vector2s != Vector2.zero) ? vector2s.ToString(10) : "[zero]") + " invMass: " + (flag ? "[identical]" : (nodeData.invMass + " vs " + nodeData2.invMass)));
				}
			}
			if (motions.Count != other.motions.Count)
			{
				log.Add("Num motions different: " + motions.Count + " vs " + other.motions.Count);
			}
			System.Math.Max(motions.Count, other.motions.Count);
			for (int k = 0; k < motions.Count; k++)
			{
				MotionData motionData = ((k < motions.Count) ? motions[k] : default(MotionData));
				MotionData motionData2 = ((k < other.motions.Count) ? other.motions[k] : default(MotionData));
				if (motionData != motionData2)
				{
					log.Add("Motion @" + k + " at " + motionData.dataString + " offset from #" + motionData2.objectId + " " + (motionData.pos - motionData2.pos).ToString(8));
				}
			}
		}

		public void PrepForComparison()
		{
			events.Sort(EventData.Comparison);
		}
	}
}
