using System;

namespace FluffyUnderware.Curvy.Generator
{
	public interface IOnRequestPath : IOnRequestProcessing, IPathProvider
	{
		[Obsolete("IOnRequestPath.PathLength and CGDataRequestRasterization.SplineAbsoluteLength are no more needed. SplineInputModuleBase.getPathLength is used instead")]
		float PathLength { get; }
	}
}
