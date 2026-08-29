using System;
using System.Collections.Generic;

namespace GLTFast.Schema
{
	[Serializable]
	public abstract class AnimationBase<TChannel, TSampler> : AnimationBase where TChannel : AnimationChannelBase where TSampler : AnimationSampler
	{
		public TChannel[] channels;

		public TSampler[] samplers;

		public override IReadOnlyList<AnimationChannelBase> Channels => channels;

		public override IReadOnlyList<AnimationSampler> Samplers => samplers;
	}
	[Serializable]
	public abstract class AnimationBase : NamedObject
	{
		public abstract IReadOnlyList<AnimationChannelBase> Channels { get; }

		public abstract IReadOnlyList<AnimationSampler> Samplers { get; }

		internal void GltfSerialize(JsonWriter writer)
		{
			writer.AddObject();
			GltfSerializeName(writer);
			writer.Close();
			throw new NotImplementedException($"GltfSerialize missing on {GetType()}");
		}
	}
}
