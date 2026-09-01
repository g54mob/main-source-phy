namespace Poly.Physics
{
	public class CollisionFilter
	{
		public bool[,] isColliding;

		public CollisionFilter()
		{
			isColliding = new bool[56, 56];
			for (int i = 0; i < 56; i++)
			{
				if ((byte)i != 1)
				{
					EnableCollision(Layer.CollideEverything, (Layer)i);
				}
			}
			Layer[] array = new Layer[32]
			{
				Layer.CustomShape,
				Layer.CustomShape_vsNode,
				Layer.CustomShape_vsRoad,
				Layer.CustomShape_vsNodeAndRoad,
				Layer.Fixed_CustomShape,
				Layer.Fixed_CustomShape_vsNode,
				Layer.Fixed_CustomShape_vsRoad,
				Layer.Fixed_CustomShape_vsNodeAndRoad,
				Layer.CustomShape_vsRamp,
				Layer.CustomShape_vsNodeAndRamp,
				Layer.CustomShape_vsRoadAndRamp,
				Layer.CustomShape_vsNodeAndRoadAndRamp,
				Layer.Fixed_CustomShape_vsRamp,
				Layer.Fixed_CustomShape_vsNodeAndRamp,
				Layer.Fixed_CustomShape_vsRoadAndRamp,
				Layer.Fixed_CustomShape_vsNodeAndRoadAndRamp,
				Layer.CustomShape_vsVehicles,
				Layer.CustomShape_vsNodeAndVehicles,
				Layer.CustomShape_vsRoadAndVehicles,
				Layer.CustomShape_vsNodeAndRoadAndVehicles,
				Layer.Fixed_CustomShape_vsVehicles,
				Layer.Fixed_CustomShape_vsNodeAndVehicles,
				Layer.Fixed_CustomShape_vsRoadAndVehicles,
				Layer.Fixed_CustomShape_vsNodeAndRoadAndVehicles,
				Layer.CustomShape_vsRampAndVehicles,
				Layer.CustomShape_vsNodeAndRampAndVehicles,
				Layer.CustomShape_vsRoadAndRampAndVehicles,
				Layer.CustomShape_vsNodeAndRoadAndRampAndVehicles,
				Layer.Fixed_CustomShape_vsRampAndVehicles,
				Layer.Fixed_CustomShape_vsNodeAndRampAndVehicles,
				Layer.Fixed_CustomShape_vsRoadAndRampAndVehicles,
				Layer.Fixed_CustomShape_vsNodeAndRoadAndRampAndVehicles
			};
			Layer[] array2 = array;
			foreach (Layer l in array2)
			{
				EnableCollision(l, Layer.CustomShape);
				EnableCollision(l, Layer.Terrain);
				EnableCollision(l, Layer.Rock);
				Layer[] array3 = array;
				foreach (Layer l2 in array3)
				{
					EnableCollision(l, l2);
				}
			}
			EnableCollision(Layer.CustomShape_vsRamp, Layer.PlatformSurface);
			EnableCollision(Layer.CustomShape_vsNodeAndRamp, Layer.PlatformSurface);
			EnableCollision(Layer.CustomShape_vsRoadAndRamp, Layer.PlatformSurface);
			EnableCollision(Layer.CustomShape_vsNodeAndRoadAndRamp, Layer.PlatformSurface);
			EnableCollision(Layer.Fixed_CustomShape_vsRamp, Layer.PlatformSurface);
			EnableCollision(Layer.Fixed_CustomShape_vsNodeAndRamp, Layer.PlatformSurface);
			EnableCollision(Layer.Fixed_CustomShape_vsRoadAndRamp, Layer.PlatformSurface);
			EnableCollision(Layer.Fixed_CustomShape_vsNodeAndRoadAndRamp, Layer.PlatformSurface);
			EnableCollision(Layer.CustomShape_vsRampAndVehicles, Layer.PlatformSurface);
			EnableCollision(Layer.CustomShape_vsNodeAndRampAndVehicles, Layer.PlatformSurface);
			EnableCollision(Layer.CustomShape_vsRoadAndRampAndVehicles, Layer.PlatformSurface);
			EnableCollision(Layer.CustomShape_vsNodeAndRoadAndRampAndVehicles, Layer.PlatformSurface);
			EnableCollision(Layer.Fixed_CustomShape_vsRampAndVehicles, Layer.PlatformSurface);
			EnableCollision(Layer.Fixed_CustomShape_vsNodeAndRampAndVehicles, Layer.PlatformSurface);
			EnableCollision(Layer.Fixed_CustomShape_vsRoadAndRampAndVehicles, Layer.PlatformSurface);
			EnableCollision(Layer.Fixed_CustomShape_vsNodeAndRoadAndRampAndVehicles, Layer.PlatformSurface);
			EnableCollision(Layer.Vehicle, Layer.Vehicle);
			EnableCollision(Layer.Vehicle, Layer.Terrain);
			EnableCollision(Layer.Vehicle, Layer.PlatformSurface);
			EnableCollision(Layer.Vehicle, Layer.PlatformBase_unused);
			EnableCollision(Layer.Vehicle, Layer.Rock);
			EnableCollision(Layer.Vehicle, Layer.Balloon);
			EnableCollision(Layer.Vehicle, Layer.RoadEdge);
			EnableCollision(Layer.Vehicle, Layer.RoadEdgeConnectedToSplitNode);
			EnableCollision(Layer.CustomShape_vsVehicles, Layer.Vehicle);
			EnableCollision(Layer.CustomShape_vsNodeAndVehicles, Layer.Vehicle);
			EnableCollision(Layer.CustomShape_vsRoadAndVehicles, Layer.Vehicle);
			EnableCollision(Layer.CustomShape_vsNodeAndRoadAndVehicles, Layer.Vehicle);
			EnableCollision(Layer.Fixed_CustomShape_vsVehicles, Layer.Vehicle);
			EnableCollision(Layer.Fixed_CustomShape_vsNodeAndVehicles, Layer.Vehicle);
			EnableCollision(Layer.Fixed_CustomShape_vsRoadAndVehicles, Layer.Vehicle);
			EnableCollision(Layer.Fixed_CustomShape_vsNodeAndRoadAndVehicles, Layer.Vehicle);
			EnableCollision(Layer.CustomShape_vsRampAndVehicles, Layer.Vehicle);
			EnableCollision(Layer.CustomShape_vsNodeAndRampAndVehicles, Layer.Vehicle);
			EnableCollision(Layer.CustomShape_vsRoadAndRampAndVehicles, Layer.Vehicle);
			EnableCollision(Layer.CustomShape_vsNodeAndRoadAndRampAndVehicles, Layer.Vehicle);
			EnableCollision(Layer.Fixed_CustomShape_vsRampAndVehicles, Layer.Vehicle);
			EnableCollision(Layer.Fixed_CustomShape_vsNodeAndRampAndVehicles, Layer.Vehicle);
			EnableCollision(Layer.Fixed_CustomShape_vsRoadAndRampAndVehicles, Layer.Vehicle);
			EnableCollision(Layer.Fixed_CustomShape_vsNodeAndRoadAndRampAndVehicles, Layer.Vehicle);
			EnableCollision(Layer.NonRoadNode, Layer.Terrain);
			EnableCollision(Layer.NonRoadNode, Layer.Rock);
			EnableCollision(Layer.NonRoadNode, Layer.CustomShape_vsNode);
			EnableCollision(Layer.NonRoadNode, Layer.CustomShape_vsNodeAndRoad);
			EnableCollision(Layer.NonRoadNode, Layer.Fixed_CustomShape_vsNode);
			EnableCollision(Layer.NonRoadNode, Layer.Fixed_CustomShape_vsNodeAndRoad);
			EnableCollision(Layer.NonRoadNode, Layer.CustomShape_vsNodeAndRamp);
			EnableCollision(Layer.NonRoadNode, Layer.CustomShape_vsNodeAndRoadAndRamp);
			EnableCollision(Layer.NonRoadNode, Layer.Fixed_CustomShape_vsNodeAndRamp);
			EnableCollision(Layer.NonRoadNode, Layer.Fixed_CustomShape_vsNodeAndRoadAndRamp);
			EnableCollision(Layer.NonRoadNode, Layer.CustomShape_vsNodeAndVehicles);
			EnableCollision(Layer.NonRoadNode, Layer.CustomShape_vsNodeAndRoadAndVehicles);
			EnableCollision(Layer.NonRoadNode, Layer.Fixed_CustomShape_vsNodeAndVehicles);
			EnableCollision(Layer.NonRoadNode, Layer.Fixed_CustomShape_vsNodeAndRoadAndVehicles);
			EnableCollision(Layer.NonRoadNode, Layer.CustomShape_vsNodeAndRampAndVehicles);
			EnableCollision(Layer.NonRoadNode, Layer.CustomShape_vsNodeAndRoadAndRampAndVehicles);
			EnableCollision(Layer.NonRoadNode, Layer.Fixed_CustomShape_vsNodeAndRampAndVehicles);
			EnableCollision(Layer.NonRoadNode, Layer.Fixed_CustomShape_vsNodeAndRoadAndRampAndVehicles);
			EnableCollision(Layer.SplitNode, Layer.CustomShape_vsNode);
			EnableCollision(Layer.SplitNode, Layer.CustomShape_vsNodeAndRoad);
			EnableCollision(Layer.SplitNode, Layer.Fixed_CustomShape_vsNode);
			EnableCollision(Layer.SplitNode, Layer.Fixed_CustomShape_vsNodeAndRoad);
			EnableCollision(Layer.SplitNode, Layer.CustomShape_vsNodeAndRamp);
			EnableCollision(Layer.SplitNode, Layer.CustomShape_vsNodeAndRoadAndRamp);
			EnableCollision(Layer.SplitNode, Layer.Fixed_CustomShape_vsNodeAndRamp);
			EnableCollision(Layer.SplitNode, Layer.Fixed_CustomShape_vsNodeAndRoadAndRamp);
			EnableCollision(Layer.SplitNode, Layer.CustomShape_vsNodeAndVehicles);
			EnableCollision(Layer.SplitNode, Layer.CustomShape_vsNodeAndRoadAndVehicles);
			EnableCollision(Layer.SplitNode, Layer.Fixed_CustomShape_vsNodeAndVehicles);
			EnableCollision(Layer.SplitNode, Layer.Fixed_CustomShape_vsNodeAndRoadAndVehicles);
			EnableCollision(Layer.SplitNode, Layer.CustomShape_vsNodeAndRampAndVehicles);
			EnableCollision(Layer.SplitNode, Layer.CustomShape_vsNodeAndRoadAndRampAndVehicles);
			EnableCollision(Layer.SplitNode, Layer.Fixed_CustomShape_vsNodeAndRampAndVehicles);
			EnableCollision(Layer.SplitNode, Layer.Fixed_CustomShape_vsNodeAndRoadAndRampAndVehicles);
			EnableCollision(Layer.RoadNode, Layer.Terrain);
			EnableCollision(Layer.RoadNode, Layer.Rock);
			EnableCollision(Layer.RoadNode, Layer.CustomShape_vsNode);
			EnableCollision(Layer.RoadNode, Layer.Fixed_CustomShape_vsNode);
			EnableCollision(Layer.RoadNode, Layer.CustomShape_vsNodeAndRamp);
			EnableCollision(Layer.RoadNode, Layer.Fixed_CustomShape_vsNodeAndRamp);
			EnableCollision(Layer.RoadNode, Layer.CustomShape_vsNodeAndVehicles);
			EnableCollision(Layer.RoadNode, Layer.Fixed_CustomShape_vsNodeAndVehicles);
			EnableCollision(Layer.RoadNode, Layer.CustomShape_vsNodeAndRampAndVehicles);
			EnableCollision(Layer.RoadNode, Layer.Fixed_CustomShape_vsNodeAndRampAndVehicles);
			EnableCollision(Layer.SplitRoadNode, Layer.CustomShape_vsNode);
			EnableCollision(Layer.SplitRoadNode, Layer.Fixed_CustomShape_vsNode);
			EnableCollision(Layer.SplitRoadNode, Layer.CustomShape_vsNodeAndRamp);
			EnableCollision(Layer.SplitRoadNode, Layer.Fixed_CustomShape_vsNodeAndRamp);
			EnableCollision(Layer.SplitRoadNode, Layer.CustomShape_vsNodeAndVehicles);
			EnableCollision(Layer.SplitRoadNode, Layer.Fixed_CustomShape_vsNodeAndVehicles);
			EnableCollision(Layer.SplitRoadNode, Layer.CustomShape_vsNodeAndRampAndVehicles);
			EnableCollision(Layer.SplitRoadNode, Layer.Fixed_CustomShape_vsNodeAndRampAndVehicles);
			EnableCollision(Layer.RoadEdge, Layer.CustomShape_vsRoad);
			EnableCollision(Layer.RoadEdge, Layer.CustomShape_vsNodeAndRoad);
			EnableCollision(Layer.RoadEdge, Layer.Fixed_CustomShape_vsRoad);
			EnableCollision(Layer.RoadEdge, Layer.Fixed_CustomShape_vsNodeAndRoad);
			EnableCollision(Layer.RoadEdge, Layer.CustomShape_vsRoadAndRamp);
			EnableCollision(Layer.RoadEdge, Layer.CustomShape_vsNodeAndRoadAndRamp);
			EnableCollision(Layer.RoadEdge, Layer.Fixed_CustomShape_vsRoadAndRamp);
			EnableCollision(Layer.RoadEdge, Layer.Fixed_CustomShape_vsNodeAndRoadAndRamp);
			EnableCollision(Layer.RoadEdge, Layer.CustomShape_vsRoadAndVehicles);
			EnableCollision(Layer.RoadEdge, Layer.CustomShape_vsNodeAndRoadAndVehicles);
			EnableCollision(Layer.RoadEdge, Layer.Fixed_CustomShape_vsRoadAndVehicles);
			EnableCollision(Layer.RoadEdge, Layer.Fixed_CustomShape_vsNodeAndRoadAndVehicles);
			EnableCollision(Layer.RoadEdge, Layer.CustomShape_vsRoadAndRampAndVehicles);
			EnableCollision(Layer.RoadEdge, Layer.CustomShape_vsNodeAndRoadAndRampAndVehicles);
			EnableCollision(Layer.RoadEdge, Layer.Fixed_CustomShape_vsRoadAndRampAndVehicles);
			EnableCollision(Layer.RoadEdge, Layer.Fixed_CustomShape_vsNodeAndRoadAndRampAndVehicles);
			EnableCollision(Layer.RoadEdgeConnectedToSplitNode, Layer.CustomShape_vsRoad);
			EnableCollision(Layer.RoadEdgeConnectedToSplitNode, Layer.CustomShape_vsNodeAndRoad);
			EnableCollision(Layer.RoadEdgeConnectedToSplitNode, Layer.Fixed_CustomShape_vsRoad);
			EnableCollision(Layer.RoadEdgeConnectedToSplitNode, Layer.Fixed_CustomShape_vsNodeAndRoad);
			EnableCollision(Layer.RoadEdgeConnectedToSplitNode, Layer.CustomShape_vsRoadAndRamp);
			EnableCollision(Layer.RoadEdgeConnectedToSplitNode, Layer.CustomShape_vsNodeAndRoadAndRamp);
			EnableCollision(Layer.RoadEdgeConnectedToSplitNode, Layer.Fixed_CustomShape_vsRoadAndRamp);
			EnableCollision(Layer.RoadEdgeConnectedToSplitNode, Layer.Fixed_CustomShape_vsNodeAndRoadAndRamp);
			EnableCollision(Layer.RoadEdgeConnectedToSplitNode, Layer.CustomShape_vsRoadAndVehicles);
			EnableCollision(Layer.RoadEdgeConnectedToSplitNode, Layer.CustomShape_vsNodeAndRoadAndVehicles);
			EnableCollision(Layer.RoadEdgeConnectedToSplitNode, Layer.Fixed_CustomShape_vsRoadAndVehicles);
			EnableCollision(Layer.RoadEdgeConnectedToSplitNode, Layer.Fixed_CustomShape_vsNodeAndRoadAndVehicles);
			EnableCollision(Layer.RoadEdgeConnectedToSplitNode, Layer.CustomShape_vsRoadAndRampAndVehicles);
			EnableCollision(Layer.RoadEdgeConnectedToSplitNode, Layer.CustomShape_vsNodeAndRoadAndRampAndVehicles);
			EnableCollision(Layer.RoadEdgeConnectedToSplitNode, Layer.Fixed_CustomShape_vsRoadAndRampAndVehicles);
			EnableCollision(Layer.RoadEdgeConnectedToSplitNode, Layer.Fixed_CustomShape_vsNodeAndRoadAndRampAndVehicles);
			EnableCollision(Layer.DebrisNonRoadNode, Layer.Terrain);
			EnableCollision(Layer.DebrisNonRoadNode, Layer.Rock);
			EnableCollision(Layer.DebrisNonRoadNode, Layer.Fixed_CustomShape_vsNode);
			EnableCollision(Layer.DebrisNonRoadNode, Layer.Fixed_CustomShape_vsNodeAndRoad);
			EnableCollision(Layer.DebrisNonRoadNode, Layer.Fixed_CustomShape_vsNodeAndRamp);
			EnableCollision(Layer.DebrisNonRoadNode, Layer.Fixed_CustomShape_vsNodeAndRoadAndRamp);
			EnableCollision(Layer.DebrisNonRoadNode, Layer.Fixed_CustomShape_vsNodeAndVehicles);
			EnableCollision(Layer.DebrisNonRoadNode, Layer.Fixed_CustomShape_vsNodeAndRoadAndVehicles);
			EnableCollision(Layer.DebrisNonRoadNode, Layer.Fixed_CustomShape_vsNodeAndRampAndVehicles);
			EnableCollision(Layer.DebrisNonRoadNode, Layer.Fixed_CustomShape_vsNodeAndRoadAndRampAndVehicles);
			EnableCollision(Layer.DebrisRoadNode, Layer.Terrain);
			EnableCollision(Layer.DebrisRoadNode, Layer.Fixed_CustomShape_vsNode);
			EnableCollision(Layer.DebrisRoadNode, Layer.Fixed_CustomShape_vsNodeAndRamp);
			EnableCollision(Layer.DebrisRoadNode, Layer.Fixed_CustomShape_vsNodeAndVehicles);
			EnableCollision(Layer.DebrisRoadNode, Layer.Fixed_CustomShape_vsNodeAndRampAndVehicles);
			EnableCollision(Layer.DebrisRoadNode, Layer.Rock);
			EnableCollision(Layer.DebrisRoadEdge, Layer.Fixed_CustomShape_vsRoad);
			EnableCollision(Layer.DebrisRoadEdge, Layer.Fixed_CustomShape_vsNodeAndRoad);
			EnableCollision(Layer.DebrisRoadEdge, Layer.Fixed_CustomShape_vsRoadAndRamp);
			EnableCollision(Layer.DebrisRoadEdge, Layer.Fixed_CustomShape_vsNodeAndRoadAndRamp);
			EnableCollision(Layer.DebrisRoadEdge, Layer.Fixed_CustomShape_vsRoadAndVehicles);
			EnableCollision(Layer.DebrisRoadEdge, Layer.Fixed_CustomShape_vsNodeAndRoadAndVehicles);
			EnableCollision(Layer.DebrisRoadEdge, Layer.Fixed_CustomShape_vsRoadAndRampAndVehicles);
			EnableCollision(Layer.DebrisRoadEdge, Layer.Fixed_CustomShape_vsNodeAndRoadAndRampAndVehicles);
			EnableCollision(Layer.WaterBlock_Trigger, Layer.NonRoadNode);
			EnableCollision(Layer.WaterBlock_Trigger, Layer.RoadNode);
			EnableCollision(Layer.WaterBlock_Trigger, Layer.SplitNode);
			EnableCollision(Layer.WaterBlock_Trigger, Layer.DebrisNonRoadNode);
			EnableCollision(Layer.WaterBlock_Trigger, Layer.DebrisRoadNode);
			array2 = array;
			foreach (Layer l3 in array2)
			{
				EnableCollision(Layer.WaterBlock_Trigger, l3);
			}
			EnableCollision(Layer.WaterBlock_Trigger, Layer.Vehicle);
			EnableCollision(Layer.VisibilityArea_Trigger, Layer.Vehicle);
		}

		private void EnableCollision(Layer l0, Layer l1, bool value = true)
		{
			isColliding[(uint)l0, (uint)l1] = value;
			isColliding[(uint)l1, (uint)l0] = value;
		}
	}
}
