using UnityEngine;

namespace MoreMountains.Tools
{
	[MMRequiresConstantRepaint]
	public class MMRadioBroadcaster : MMMonoBehaviour
	{
		public delegate void OnValueChangeDelegate();

		[Header("Source")]
		public MMPropertyEmitter Emitter;

		[Header("Destinations")]
		public MMRadioReceiver[] Receivers;

		[Header("Channel Broadcasting")]
		public bool BroadcastOnChannel;

		[MMCondition("BroadcastOnChannel", true)]
		public int Channel;

		[MMCondition("BroadcastOnChannel", true)]
		public bool OnlyBroadcastOnValueChange;

		public OnValueChangeDelegate OnValueChange;

		protected float _levelLastFrame;

		protected virtual void Awake()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void ProcessBroadcast()
		{
		}
	}
}
