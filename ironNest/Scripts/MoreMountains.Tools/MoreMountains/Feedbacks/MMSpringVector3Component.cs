using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	public abstract class MMSpringVector3Component<T> : MMSpringComponentBase, MMEventListener<MMSpringVector3Event>, MMEventListenerBase where T : Component
	{
		[MMInspectorGroup("Target", true, 17, false)]
		public T Target;

		[MMInspectorGroup("Channel & TimeScale", true, 16, true)]
		[Tooltip("whether this spring should run on scaled time (and be impacted by time scale changes) or unscaled time (and not be impacted by time scale changes)")]
		public TimeScaleModes TimeScaleMode;

		[Tooltip("whether to listen on a channel defined by an int or by a MMChannel scriptable object. Ints are simple to setup but can get messy and make it harder to remember what int corresponds to what. MMChannel scriptable objects require you to create them in advance, but come with a readable name and are more scalable")]
		public MMChannelModes ChannelMode;

		[Tooltip("the channel to listen to - has to match the one on the feedback")]
		[MMEnumCondition("ChannelMode", new int[] { 0 })]
		public int Channel;

		[Tooltip("the MMChannel definition asset to use to listen for events. The feedbacks targeting this shaker will have to reference that same MMChannel definition to receive events - to create a MMChannel, right click anywhere in your project (usually in a Data folder) and go MoreMountains > MMChannel, then name it with some unique name")]
		[MMEnumCondition("ChannelMode", new int[] { 1 })]
		public MMChannel MMChannelDefinition;

		[MMInspectorGroup("Spring Settings", true, 18, false)]
		[Header("SpringVector3")]
		public MMSpringVector3 SpringVector3;

		[MMInspectorGroup("Randomness", true, 12, true)]
		[Header("Move To Random")]
		[Tooltip("the minimum vector from which to pick a random value when calling MoveToRandom()")]
		public Vector3 MoveToRandomValueMin;

		[Tooltip("the maximum vector from which to pick a random value when calling MoveToRandom()")]
		public Vector3 MoveToRandomValueMax;

		[Header("Bump Random")]
		[Tooltip("the minimum vector from which to pick a random value when calling BumpRandom()")]
		public Vector3 BumpAmountRandomValueMin;

		[Tooltip("the maximum vector from which to pick a random value when calling BumpRandom()")]
		public Vector3 BumpAmountRandomValueMax;

		[MMInspectorGroup("Test", true, 20, true)]
		[Tooltip("the value to move this spring to when interacting with any of the MoveTo debug buttons in its inspector")]
		public Vector3 TestMoveToValue;

		[MMInspectorButtonBar(new string[] { "MoveTo", "MoveToAdditive", "MoveToSubtractive", "MoveToRandom", "MoveToInstant" }, new string[] { "TestMoveTo", "TestMoveToAdditive", "TestMoveToSubtractive", "TestMoveToRandom", "TestMoveToInstant" }, new bool[] { true, true, true, true, true }, new string[] { "main-call-to-action", null, null, null, null })]
		public bool MoveToToolbar;

		[Tooltip("the amount by which to bump this spring when interacting with the Bump debug button in its inspector")]
		public Vector3 TestBumpAmount;

		[MMInspectorButtonBar(new string[] { "Bump", "BumpRandom" }, new string[] { "TestBump", "TestBumpRandom" }, new bool[] { true, true }, new string[] { "main-call-to-action", null })]
		public bool BumpToToolbar;

		[MMInspectorButtonBar(new string[] { "Stop", "Finish", "RestoreInitialValue", "ResetInitialValue" }, new string[] { "Stop", "Finish", "RestoreInitialValue", "ResetInitialValue" }, new bool[] { true, true, true, true }, new string[] { null, null, null, null })]
		public bool OtherControlsToToolbar;

		public override bool LowVelocity => false;

		public float DeltaTime => 0f;

		public virtual Vector3 TargetVector3 { get; set; }

		public virtual void MoveTo(Vector3 newValue)
		{
		}

		public virtual void MoveToAdditive(Vector3 newValue)
		{
		}

		public virtual void MoveToSubtractive(Vector3 newValue)
		{
		}

		public virtual void MoveToRandom()
		{
		}

		public virtual void MoveToInstant(Vector3 newValue)
		{
		}

		public virtual void MoveToRandom(Vector3 min, Vector3 max)
		{
		}

		public virtual void Bump(Vector3 bumpAmount)
		{
		}

		public virtual void BumpRandom()
		{
		}

		public virtual void BumpRandom(Vector3 min, Vector3 max)
		{
		}

		public override void Stop()
		{
		}

		public override void RestoreInitialValue()
		{
		}

		public override void ResetInitialValue()
		{
		}

		protected override void UpdateSpringValue()
		{
		}

		public override void Finish()
		{
		}

		protected override void Initialization()
		{
		}

		protected virtual void ApplyValue(Vector3 newValue)
		{
		}

		protected override void GrabCurrentValue()
		{
		}

		public void OnMMEvent(MMSpringVector3Event springEvent)
		{
		}

		protected override void Awake()
		{
		}

		protected void OnDestroy()
		{
		}

		protected override void TestMoveTo()
		{
		}

		protected override void TestMoveToAdditive()
		{
		}

		protected override void TestMoveToSubtractive()
		{
		}

		protected override void TestMoveToRandom()
		{
		}

		protected override void TestMoveToInstant()
		{
		}

		protected override void TestBump()
		{
		}

		protected override void TestBumpRandom()
		{
		}
	}
}
