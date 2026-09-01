using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	[RequireComponent(typeof(Text))]
	[AddComponentMenu("More Mountains/Tools/Time/MMCountdown")]
	public class MMCountdown : MMMonoBehaviour
	{
		[Serializable]
		public class MMCountdownFloor
		{
			public float FloorValue;

			[MMReadOnly]
			public float LastChangedAt;

			public UnityEvent FloorEvent;
		}

		public enum MMCountdownDirections
		{
			Ascending = 0,
			Descending = 1
		}

		public enum FormatMethods
		{
			Explicit = 0,
			Choices = 1
		}

		[MMInspectorGroup("Countdown", true, 18, false)]
		[MMInformation("You can define the bounds of the countdown (how much it should count down from, and to how much, the format it should be displayed in (standard Unity float ToString formatting).", MMInformationAttribute.InformationType.Info, false)]
		public float CountdownFrom;

		public float CountdownTo;

		public bool Infinite;

		[MMInspectorGroup("Display", true, 19, false)]
		public FormatMethods FormatMethod;

		[MMEnumCondition("FormatMethod", new int[] { 0 })]
		public bool FloorValues;

		[MMEnumCondition("FormatMethod", new int[] { 0 })]
		public string Format;

		[MMEnumCondition("FormatMethod", new int[] { 1 })]
		public bool Hours;

		[MMEnumCondition("FormatMethod", new int[] { 1 })]
		public bool Minutes;

		[MMEnumCondition("FormatMethod", new int[] { 1 })]
		public bool Seconds;

		[MMEnumCondition("FormatMethod", new int[] { 1 })]
		public bool Milliseconds;

		[MMInspectorGroup("Settings", true, 20, false)]
		[MMInformation("You can choose whether or not the countdown should automatically start on its Start, at what frequency (in seconds) it should refresh (0 means every frame), and the countdown's speed multiplier (2 will be twice as fast, 0.5 half normal speed, etc). Floors are used to define and trigger events when certain floors are reached. For each floor, define a floor value (in seconds). Everytime this floor gets reached, the corresponding event will be triggered.Bind events here to trigger them when the countdown reaches its To destination, or every time it gets refreshed.", MMInformationAttribute.InformationType.Info, false)]
		public bool AutoStart;

		public bool AutoReset;

		public bool PingPong;

		public float RefreshFrequency;

		public float CountdownSpeed;

		[MMInspectorGroup("Floors", true, 21, false)]
		public List<MMCountdownFloor> Floors;

		[MMInspectorGroup("Events", true, 22, false)]
		public UnityEvent CountdownCompleteEvent;

		public UnityEvent CountdownRefreshEvent;

		[MMInspectorGroup("Debug", true, 17, false)]
		[MMReadOnly]
		public float CurrentTime;

		[MMReadOnly]
		public MMCountdownDirections Direction;

		[MMInspectorButton("StopCountdown")]
		public bool StopCountdownButton;

		[MMInspectorButton("StartCountdown")]
		public bool StartCountdownButton;

		[MMInspectorButton("ResetCountdown")]
		public bool ResetCountdownButton;

		[MMInspectorButton("ChangeDirection")]
		public bool ChangeDirectionButton;

		public float DebugNewCurrentTime;

		[MMInspectorButton("DebugSetNewCurrentTime")]
		public bool DebugSetNewCurrentTimeButton;

		protected Text _text;

		protected float _lastRefreshAt;

		protected bool _countdowning;

		protected int _lastUnitValue;

		private void DebugSetNewCurrentTime()
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void UpdateTime()
		{
		}

		protected virtual void UpdateText()
		{
		}

		protected virtual void CheckForEnd()
		{
		}

		protected virtual void CheckForFloors()
		{
		}

		public virtual void StartCountdown()
		{
		}

		public virtual void StopCountdown()
		{
		}

		public virtual void ResetCountdown()
		{
		}

		public virtual void ChangeDirection()
		{
		}

		public virtual void SetCurrentTime(float newCurrentTime)
		{
		}
	}
}
