using System;
using System.Text;
using Unity.Mathematics;
using UnityEngine;

namespace MagicaCloth2
{
	public class TimeManager : IManager, IDisposable, IValid
	{
		public enum UpdateLocation
		{
			AfterLateUpdate = 0,
			BeforeLateUpdate = 1
		}

		internal int simulationFrequency = 90;

		internal int maxSimulationCountPerFrame = 3;

		internal UpdateLocation updateLocation;

		private bool isValid;

		internal float GlobalTimeScale = 1f;

		internal int FixedUpdateCount { get; private set; }

		internal float SimulationDeltaTime { get; private set; }

		internal float MaxDeltaTime { get; private set; }

		internal float4 SimulationPower { get; private set; }

		public void Dispose()
		{
			isValid = true;
			GlobalTimeScale = 1f;
			FixedUpdateCount = 0;
			SimulationPower = 1f;
			MagicaManager.afterFixedUpdateDelegate = (MagicaManager.UpdateMethod)Delegate.Remove(MagicaManager.afterFixedUpdateDelegate, new MagicaManager.UpdateMethod(AfterFixedUpdate));
			MagicaManager.afterRenderingDelegate = (MagicaManager.UpdateMethod)Delegate.Remove(MagicaManager.afterRenderingDelegate, new MagicaManager.UpdateMethod(AfterRenderring));
		}

		public void EnterdEditMode()
		{
			Dispose();
		}

		public void Initialize()
		{
			GlobalTimeScale = 1f;
			FixedUpdateCount = 0;
			SimulationPower = 1f;
			MagicaManager.afterFixedUpdateDelegate = (MagicaManager.UpdateMethod)Delegate.Combine(MagicaManager.afterFixedUpdateDelegate, new MagicaManager.UpdateMethod(AfterFixedUpdate));
			MagicaManager.afterRenderingDelegate = (MagicaManager.UpdateMethod)Delegate.Combine(MagicaManager.afterRenderingDelegate, new MagicaManager.UpdateMethod(AfterRenderring));
			isValid = true;
		}

		public bool IsValid()
		{
			return isValid;
		}

		private void AfterFixedUpdate()
		{
			FixedUpdateCount++;
		}

		private void AfterRenderring()
		{
			FixedUpdateCount = 0;
		}

		internal void FrameUpdate()
		{
			simulationFrequency = Mathf.Clamp(simulationFrequency, 30, 150);
			maxSimulationCountPerFrame = Mathf.Clamp(maxSimulationCountPerFrame, 1, 5);
			GlobalTimeScale = Mathf.Clamp01(GlobalTimeScale);
			SimulationDeltaTime = 1f / (float)simulationFrequency;
			MaxDeltaTime = SimulationDeltaTime * (float)maxSimulationCountPerFrame;
			float num = 90f / (float)simulationFrequency;
			SimulationPower = new float4(num, (num > 1f) ? Mathf.Pow(num, 0.5f) : num, (num > 1f) ? Mathf.Pow(num, 0.3f) : num, Mathf.Pow(num, 1.8f));
		}

		public void InformationLog(StringBuilder allsb)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("========== Time Manager ==========");
			if (!IsValid())
			{
				stringBuilder.AppendLine("Time Manager. Invalid");
			}
			else
			{
				stringBuilder.AppendLine($"SimulationFrequency:{simulationFrequency}");
				stringBuilder.AppendLine($"MaxSimulationCountPerFrame:{maxSimulationCountPerFrame}");
				stringBuilder.AppendLine($"GlobalTimeScale:{GlobalTimeScale}");
				stringBuilder.AppendLine($"SimulationDeltaTime:{SimulationDeltaTime}");
				stringBuilder.AppendLine($"SimulationPower:{SimulationPower}");
			}
			stringBuilder.AppendLine();
			Debug.Log(stringBuilder.ToString());
			allsb.Append(stringBuilder);
		}
	}
}
