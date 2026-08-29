using System.Collections.Generic;
using UnityEngine;

namespace MagicaCloth2
{
	internal interface ITransform
	{
		void GetUsedTransform(HashSet<Transform> transformSet);

		void ReplaceTransform(Dictionary<int, Transform> replaceDict);
	}
}
