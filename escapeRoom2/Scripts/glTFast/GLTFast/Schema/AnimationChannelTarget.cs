using System;

namespace GLTFast.Schema
{
	[Serializable]
	public class AnimationChannelTarget
	{
		public int node;

		[Obsolete("Use GetPath for access.")]
		public string path;

		private AnimationChannelBase.Path m_Path;

		public AnimationChannelBase.Path GetPath()
		{
			if (m_Path != AnimationChannelBase.Path.Unknown)
			{
				return m_Path;
			}
			if (!Enum.TryParse<AnimationChannelBase.Path>(path, ignoreCase: true, out m_Path))
			{
				m_Path = AnimationChannelBase.Path.Invalid;
			}
			path = null;
			return m_Path;
		}

		internal void GltfSerialize(JsonWriter writer)
		{
			throw new NotImplementedException($"GltfSerialize missing on {GetType()}");
		}
	}
}
