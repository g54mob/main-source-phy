using System.Collections.Generic;
using UnityEngine;

namespace Koenigz.PerfectCulling
{
	public abstract class PerfectCullingBakeData : ScriptableObject
	{
		[SerializeField]
		public string strRendererName;

		[SerializeField]
		public int bakeDataVersion;

		[SerializeField]
		public bool bakeCompleted;

		[SerializeField]
		public int bakeHash;

		[SerializeField]
		public string strBakeDate;

		[SerializeField]
		public long bakeDurationMilliseconds;

		public virtual void SetRawData(int index, ushort[] indices, uint mergeBitmask, bool validateData = true)
		{
		}

		public virtual void SampleAtIndex(int index, List<ushort> indices)
		{
		}

		public virtual int SearchIndexForClosestNonEmptyCell(int index)
		{
			return 0;
		}

		public virtual void PrepareForBake(PerfectCullingBakingBehaviour bakingBehaviour)
		{
		}

		public virtual void CompleteBake()
		{
		}

		public virtual void DrawInspectorGUI()
		{
		}
	}
}
