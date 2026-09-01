using System.Collections.Generic;
using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	[AddComponentMenu("Surface Waves/Simulation Flow")]
	public class SimulationFlow : SimulationStep
	{
		[SerializeField]
		[ReorderableList]
		protected List<SimulationStep> m_steps = new List<SimulationStep>();

		public bool loadStepsOnStart;

		public bool runStepsOnUpdate;

		public override void LoadData()
		{
			for (int i = 0; i < m_steps.Count; i++)
			{
				m_steps[i].LoadData();
			}
		}

		public override void RunStep()
		{
			for (int i = 0; i < m_steps.Count; i++)
			{
				m_steps[i].RunStep();
			}
		}

		public void AddStep(SimulationStep step)
		{
			m_steps.Add(step);
		}

		public void RemoveStep(SimulationStep step)
		{
			m_steps.Remove(step);
		}

		protected void Start()
		{
			if (loadStepsOnStart)
			{
				LoadData();
			}
		}

		protected void Update()
		{
			if (runStepsOnUpdate)
			{
				RunStep();
			}
		}
	}
}
