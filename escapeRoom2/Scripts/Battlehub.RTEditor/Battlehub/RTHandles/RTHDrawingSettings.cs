using Battlehub.RTCommon;
using UnityEngine;

namespace Battlehub.RTHandles
{
	public class RTHDrawingSettings
	{
		public Vector3 Position;

		public Quaternion Rotation;

		public Vector3 Scale;

		public RuntimeHandleAxis SelectedAxis;

		public LockObject LockObject;

		public bool DrawLocked;

		internal MaterialPropertyBlock[] PropertyBlocks;

		public RTHDrawingSettings()
		{
			Position = Vector3.zero;
			Rotation = Quaternion.identity;
			Scale = Vector3.one;
			SelectedAxis = RuntimeHandleAxis.None;
			LockObject = null;
			DrawLocked = true;
		}

		internal void Init(int propertyBlocksCount)
		{
			if (PropertyBlocks == null)
			{
				PropertyBlocks = new MaterialPropertyBlock[propertyBlocksCount];
				for (int i = 0; i < propertyBlocksCount; i++)
				{
					PropertyBlocks[i] = new MaterialPropertyBlock();
				}
			}
		}
	}
}
