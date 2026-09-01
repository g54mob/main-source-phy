using System;
using FMOD;

namespace FMODUnity
{
	public class EventNotFoundException : Exception
	{
		public GUID Guid;

		public string Path;

		public EventNotFoundException(string path)
		{
		}

		public EventNotFoundException(GUID guid)
		{
		}

		public EventNotFoundException(EventReference eventReference)
		{
		}
	}
}
