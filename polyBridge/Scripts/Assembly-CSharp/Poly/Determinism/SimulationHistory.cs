using System;
using System.Collections.Generic;
using System.Linq;
using Poly.Base;
using Poly.Physics;

namespace Poly.Determinism
{
	[Serializable]
	public class SimulationHistory
	{
		public List<FrameData> frames = new List<FrameData>();

		public FrameData currentFrame => frames.Last();

		public SimulationHistory()
		{
			CreateNewFrame();
		}

		public void CreateNewFrame()
		{
			if (frames.Count > 0)
			{
				currentFrame.timeElapsed = World.timeElapsedSafe;
				currentFrame.deltaTime = SingletonBehaviour<World>.instance.settings.frameDeltaTime;
			}
			frames.Add(new FrameData());
		}

		public void Clear()
		{
			frames.Clear();
			CreateNewFrame();
		}
	}
}
