using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class AnimationSampler
	{
		public int input;

		[Obsolete("Use GetInterpolationType for access.")]
		public string interpolation;

		private InterpolationType m_Interpolation;

		public int output;

		public InterpolationType GetInterpolationType()
		{
			if (m_Interpolation != InterpolationType.Unknown)
			{
				return m_Interpolation;
			}
			if (!Enum.TryParse<InterpolationType>(interpolation, ignoreCase: true, out m_Interpolation))
			{
				m_Interpolation = InterpolationType.Linear;
			}
			interpolation = null;
			return m_Interpolation;
		}
	}
}
