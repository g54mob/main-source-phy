using System;
using Steamworks;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	public struct TimelineEventDataArguments
	{
		public string title;

		public string description;

		public string icon;

		public uint priority;

		public float startSeconds;

		public float durationSeconds;

		public ETimelineEventClipPriority possibleClip;
	}
}
