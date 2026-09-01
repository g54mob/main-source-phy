using System;

namespace FluffyUnderware.Curvy
{
	[Obsolete("Use CurvyInterpolatableMetadataBase class instead")]
	public interface ICurvyInterpolatableMetadata : ICurvyMetadata
	{
		object Value { get; }

		object InterpolateObject(ICurvyMetadata b, float f);
	}
	[Obsolete("Use CurvyInterpolatableMetadataBase<U> class instead")]
	public interface ICurvyInterpolatableMetadata<U> : ICurvyInterpolatableMetadata, ICurvyMetadata
	{
		U Interpolate(ICurvyMetadata b, float f);
	}
}
