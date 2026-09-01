using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration.API
{
	public static class Timeline
	{
		public static class Client
		{
			public static readonly List<TimelineEventData> TimelineEvents;

			private static readonly Dictionary<ulong, TimelineEventDataArguments> MTimelineEventDataArguments;

			private static CallResult<SteamTimelineEventRecordingExists_t> _mSteamTimelineEventRecordingExistsT;

			private static CallResult<SteamTimelineGamePhaseRecordingExists_t> _mSteamTimelineGamePhaseRecordingExistsT;

			[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
			private static void RunTimeInit()
			{
			}

			public static TimelineEventDataArguments GetArguments(TimelineEventData timelineEvent)
			{
				return default(TimelineEventDataArguments);
			}

			public static void SetTimelineTooltip(string description, float timeDelta)
			{
			}

			public static void ClearTimelineTooltip(float timeDelta)
			{
			}

			public static TimelineEventData AddInstantaneousTimelineEvent(string title, string description, string icon, uint priority, float startOffsetSeconds, ETimelineEventClipPriority possibleClip)
			{
				return default(TimelineEventData);
			}

			public static TimelineEventData AddRangeTimelineEvent(string title, string description, string icon, uint priority, float startOffsetSeconds, float durationSeconds, ETimelineEventClipPriority possibleClip)
			{
				return default(TimelineEventData);
			}

			public static TimelineEventData StartRangeTimelineEvent(string title, string description, string icon, uint priority, float startOffsetSeconds, ETimelineEventClipPriority possibleClip)
			{
				return default(TimelineEventData);
			}

			public static void UpdateRangeTimelineEvent(TimelineEventData timelineEvent, string title, string description, string icon, uint priority, ETimelineEventClipPriority possibleClip)
			{
			}

			public static void EndRangeTimelineEvent(TimelineEventData timelineEvent, float endOffsetSeconds)
			{
			}

			public static void RemoveTimelineEvent(TimelineEventData timelineEvent)
			{
			}

			public static void DoesEventRecordingExist(TimelineEventData timelineEvent, Action<bool> callback)
			{
			}

			public static void StartGamePhase()
			{
			}

			public static void EndGamePhase()
			{
			}

			public static void SetGamePhaseId(string id)
			{
			}

			public static void DoesGamePhaseRecordingExist(string id, Action<SteamTimelineGamePhaseRecordingExists_t> callback)
			{
			}

			public static void AddGamePhaseTag(string tagName, string tagIcon, string tagGroup, uint priority)
			{
			}

			public static void SetGamePhaseAttribute(string attributeGroup, string attributeValue, uint priority)
			{
			}

			public static void SetTimelineGameMode(ETimelineGameMode mode)
			{
			}

			public static void OpenOverlayToGamePhase(string phaseId)
			{
			}

			public static void OpenOverlayToTimelineEvent(TimelineEventData timelineEvent)
			{
			}
		}
	}
}
