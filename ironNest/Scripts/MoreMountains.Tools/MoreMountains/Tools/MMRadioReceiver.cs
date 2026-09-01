using UnityEngine;

namespace MoreMountains.Tools
{
	[MMRequiresConstantRepaint]
	public class MMRadioReceiver : MMMonoBehaviour
	{
		[Header("Target")]
		public MMPropertyReceiver Receiver;

		[Header("Channel")]
		public bool CanListen;

		[MMCondition("CanListen", true)]
		public int Channel;

		[Header("Modifiers")]
		public bool RandomizeLevel;

		[MMCondition("RandomizeLevel", true)]
		public float MinRandomLevelMultiplier;

		[MMCondition("RandomizeLevel", true)]
		public float MaxRandomLevelMultiplier;

		protected bool _listeningToEvents;

		protected float _randomLevelMultiplier;

		protected float _lastLevel;

		protected virtual void Awake()
		{
		}

		public virtual void GenerateRandomLevelMultiplier()
		{
		}

		public virtual void SetLevel(float newLevel)
		{
		}

		protected virtual void OnRadioLevelEvent(int channel, float level)
		{
		}

		protected virtual void OnDestroy()
		{
		}

		public virtual void StartListening()
		{
		}

		public virtual void StopListening()
		{
		}
	}
}
