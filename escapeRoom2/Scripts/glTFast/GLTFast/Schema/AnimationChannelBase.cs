using System;

namespace GLTFast.Schema
{
	[Serializable]
	public abstract class AnimationChannelBase<TTarget> : AnimationChannelBase where TTarget : AnimationChannelTarget
	{
		public TTarget target;

		public override AnimationChannelTarget Target => target;
	}
	[Serializable]
	public abstract class AnimationChannelBase
	{
		public enum Path
		{
			Unknown = 0,
			Invalid = 1,
			Translation = 2,
			Rotation = 3,
			Scale = 4,
			Weights = 5,
			Pointer = 6
		}

		public int sampler;

		public abstract AnimationChannelTarget Target { get; }

		internal void GltfSerialize(JsonWriter writer)
		{
			throw new NotImplementedException($"GltfSerialize missing on {GetType()}");
		}
	}
}
