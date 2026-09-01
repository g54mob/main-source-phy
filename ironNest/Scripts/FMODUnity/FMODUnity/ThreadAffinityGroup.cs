using System;
using System.Collections.Generic;

namespace FMODUnity
{
	[Serializable]
	public class ThreadAffinityGroup
	{
		public List<ThreadType> threads;

		public ThreadAffinity affinity;

		public ThreadAffinityGroup()
		{
		}

		public ThreadAffinityGroup(ThreadAffinityGroup other)
		{
		}

		public ThreadAffinityGroup(ThreadAffinity affinity, params ThreadType[] threads)
		{
		}
	}
}
