using UnityEngine;

namespace LevelCreator
{
	public class VolumeData
	{
		public float[,,] voxels;

		public void Init(Vector3Int noOfVoxels)
		{
			voxels = new float[noOfVoxels.z, noOfVoxels.y, noOfVoxels.x];
		}
	}
}
