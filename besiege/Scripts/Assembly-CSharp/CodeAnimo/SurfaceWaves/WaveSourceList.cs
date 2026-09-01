using System.Collections.Generic;
using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	[AddComponentMenu("Surface Waves/Wave Sources/Wave Source List")]
	public class WaveSourceList : SimulationStep
	{
		[HideInInspector]
		public GameObject waveSourceSettings;

		[HideInInspector]
		public GameObject waveDrainSettings;

		[SerializeField]
		[TextureDebug(inputBox = false)]
		private RenderTexture m_outputData;

		public Dimensions simulationSize;

		[ReorderableList]
		[SerializeField]
		protected List<WaveSource> m_sourceList = new List<WaveSource>();

		protected bool m_dataLoaded;

		public RenderTexture outputData
		{
			get
			{
				return m_outputData;
			}
		}

		public void AddStep(WaveSource source)
		{
			m_sourceList.Add(source);
			if (m_dataLoaded && Application.isPlaying)
			{
				source.LoadData();
			}
		}

		public void RemoveStep(WaveSource source)
		{
			m_sourceList.Remove(source);
		}

		protected void Awake()
		{
			m_dataLoaded = false;
		}

		public override void LoadData()
		{
			int count = m_sourceList.Count;
			for (int i = 0; i < count; i++)
			{
				m_sourceList[i].LoadData();
			}
			m_dataLoaded = true;
		}

		public override void RunStep()
		{
			int count = m_sourceList.Count;
			for (int i = 0; i < count; i++)
			{
				WaveSource waveSource = m_sourceList[i];
				if (waveSource == null)
				{
					m_sourceList.RemoveAt(i);
					i--;
					count = m_sourceList.Count;
					continue;
				}
				if (i > 0)
				{
					waveSource.previousInput = m_sourceList[i - 1];
				}
				else
				{
					waveSource.previousInput = null;
				}
				waveSource.RunStep();
			}
			if (count > 0)
			{
				m_outputData = m_sourceList[count - 1].outputData;
			}
		}
	}
}
