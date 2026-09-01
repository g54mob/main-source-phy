using System.Linq;
using Poly.File;
using UnityEngine;

namespace Poly.Physics.Test
{
	public class ExportNodesAndEdgesOnStart : Action
	{
		public string saveFile = "NodesAndEdgesSnapshot.json";

		public override void OnAddedToWorld()
		{
			SaveOnceIfAnyNodesInWorld();
		}

		public override void Execute()
		{
			SaveOnceIfAnyNodesInWorld();
		}

		public void SaveOnceIfAnyNodesInWorld()
		{
			if (base.world.nodeHandles.Count > 0)
			{
				NodesAndEdgesSnapshot objectToWrite = CreateSnapshot(base.world);
				Serialize.WriteToJsonFile(saveFile, objectToWrite, append: false, prettyPrint: true);
				Object.Destroy(this);
			}
		}

		public static NodesAndEdgesSnapshot CreateSnapshot(World world)
		{
			NodesAndEdgesSnapshot result = default(NodesAndEdgesSnapshot);
			result.nodes = world.nodeHandles.Select((NodeHandle n) => new NodeDef
			{
				posX = n.solverNode.pos.x,
				posY = n.solverNode.pos.y,
				invMass = n.solverNode.invMass
			}).ToArray();
			foreach (EdgeHandle edgeHandle in world.edgeHandles)
			{
				_ = edgeHandle;
			}
			result.edges = world.edgeHandles.Select((EdgeHandle e) => new EdgeDef
			{
				nodeIdx0 = e.solverEdge.nodeIdxA,
				nodeIdx1 = e.solverEdge.nodeIdxB
			}).ToArray();
			return result;
		}
	}
}
