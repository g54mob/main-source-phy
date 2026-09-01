using UnityEngine;
using dycJggssKJBbYomRwEcQasvEaFIib;

namespace BitCode.Performance
{
	internal class FrameTimeCounter : lXPACrJRvYzCXOSgnaIzgQcePWHg
	{
		public FrameTimeCounter(int historySize)
			: base(historySize)
		{
		}

		protected override bool GetSample(out double retrievedSample)
		{
			retrievedSample = Time.unscaledDeltaTime * 1000f;
			return true;
		}
	}
}
