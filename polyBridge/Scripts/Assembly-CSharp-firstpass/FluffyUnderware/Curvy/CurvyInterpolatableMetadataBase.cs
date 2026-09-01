using System;
using UnityEngine;

namespace FluffyUnderware.Curvy
{
	[ExecuteInEditMode]
	[Obsolete("Use CurvyInterpolatableMetadataBase<T> class instead")]
	public abstract class CurvyInterpolatableMetadataBase : CurvyMetadataBase, ICurvyInterpolatableMetadata, ICurvyMetadata
	{
		[Obsolete("Use CurvyInterpolatableMetadataBase<T>.MetaDataValue instead")]
		public abstract object Value { get; }

		[Obsolete("Use CurvyInterpolatableMetadataBase<T>.Interpolate instead")]
		public abstract object InterpolateObject(ICurvyMetadata b, float f);
	}
	[ExecuteInEditMode]
	public abstract class CurvyInterpolatableMetadataBase<T> : CurvyInterpolatableMetadataBase, ICurvyInterpolatableMetadata<T>, ICurvyInterpolatableMetadata, ICurvyMetadata
	{
		public abstract T MetaDataValue { get; }

		[Obsolete("Use MetaDataValue instead")]
		public override object Value => MetaDataValue;

		public abstract T Interpolate(CurvyInterpolatableMetadataBase<T> nextMetadata, float interpolationTime);

		[Obsolete("Use Interpolate(CurvyInterpolatableMetadataBase<T>, float) instead")]
		public override object InterpolateObject(ICurvyMetadata b, float f)
		{
			return Interpolate((CurvyInterpolatableMetadataBase<T>)b, f);
		}

		[Obsolete("Use Interpolate(CurvyInterpolatableMetadataBase<T>, float) instead")]
		public T Interpolate(ICurvyMetadata b, float f)
		{
			return Interpolate((CurvyInterpolatableMetadataBase<T>)b, f);
		}
	}
}
