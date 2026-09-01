using System.Collections.Generic;
using UnityEngine;

namespace Shapes
{
	internal interface IFillableMpb
	{
		List<float> fillType { get; }

		List<float> fillSpace { get; }

		List<Vector4> fillStart { get; }

		List<Vector4> fillEnd { get; }

		List<Vector4> fillColorEnd { get; }
	}
}
