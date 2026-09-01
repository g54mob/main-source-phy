using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[CreateAssetMenu(menuName = "MoreMountains/Sequencer/MMSequence")]
	public class MMSequence : ScriptableObject
	{
		[Header("Sequence")]
		[Tooltip("the length (in seconds) of the sequence")]
		[MMFReadOnly]
		public float Length;

		[Tooltip("the original sequence (as outputted by the input sequence recorder)")]
		public MMSequenceList OriginalSequence;

		[Tooltip("the duration in seconds to apply after the last input")]
		public float EndSilenceDuration;

		[Header("Sequence Contents")]
		[Tooltip("the list of tracks for this sequence")]
		public List<MMSequenceTrack> SequenceTracks;

		[Header("Quantizing")]
		[Tooltip("whether this sequence should be used in quantized form or not")]
		public bool Quantized;

		[Tooltip("the target BPM for this sequence")]
		public int TargetBPM;

		[Tooltip("the contents of the quantized sequence")]
		public List<MMSequenceList> QuantizedSequence;

		[Space]
		[Header("Controls")]
		[MMFInspectorButton("RandomizeTrackColors")]
		public bool RandomizeTrackColorsButton;

		protected float[] _quantizedBeats;

		protected List<MMSequenceNote> _deleteList;

		private static int SortByTimestamp(MMSequenceNote p1, MMSequenceNote p2)
		{
			return 0;
		}

		public virtual void SortOriginalSequence()
		{
		}

		public virtual void QuantizeOriginalSequence()
		{
		}

		public virtual void ComputeLength()
		{
		}

		public virtual void QuantizeSequenceToBPM(List<MMSequenceNote> baseSequence)
		{
		}

		protected virtual void OnValidate()
		{
		}

		protected virtual void RandomizeTrackColors()
		{
		}

		public static Color RandomSequenceColor()
		{
			return default(Color);
		}

		public static float RoundFloatToArray(float value, float[] array)
		{
			return 0f;
		}
	}
}
