using System;

namespace CodeAnimo
{
	public class SegmentEventData : EventArgs
	{
		public GridMesh segment;

		public SegmentEventData(GridMesh segment)
		{
			this.segment = segment;
		}
	}
}
