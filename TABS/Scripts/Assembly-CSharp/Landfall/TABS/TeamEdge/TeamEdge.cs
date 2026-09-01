using System;
using UnityEngine.Rendering.PostProcessing;

namespace Landfall.TABS.TeamEdge
{
	[Serializable]
	[PostProcess(typeof(TeamEdgeRenderer), PostProcessEvent.BeforeStack, "Landfall/TeamEdge", true)]
	public sealed class TeamEdge : PostProcessEffectSettings
	{
		public BoolParameter m_DisplayWorldCords = new BoolParameter
		{
			value = false
		};

		public BoolParameter m_DisplayPerlin = new BoolParameter
		{
			value = false
		};

		public BoolParameter m_DisplayPerlinDirection = new BoolParameter
		{
			value = false
		};
	}
}
