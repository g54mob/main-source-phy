using System.Collections.Generic;
using UnityEngine;

public class BridgeSimSpeed
{
	public const int DEFAULT_SIMULATION_SPEED_INDEX = 3;

	public static List<float> m_DefaultSimulationSpeeds = new List<float> { 0.1f, 0.2f, 0.5f, 1f, 2f, 3f, 5f };

	public static List<float> m_SimulationSpeeds = new List<float>();

	public static int m_DefaultSimulationSpeedIndex = 3;

	public static int m_SimulationSpeedIndex;

	public static float m_SimulationSpeedMultiplier = 1.5f;

	private static float MIN_SIMULATION_SPEED_FOR_PITCH_INCREASE = 0.5f;

	private static float MAX_SIMULATION_SPEED_FOR_PITCH_INCREASE = 3f;

	public static void Init()
	{
		SetSimulationSpeeds(m_DefaultSimulationSpeeds);
		SetSimulationSpeedIndex(m_DefaultSimulationSpeedIndex);
	}

	public static void SetSimulationSpeeds(List<float> speeds)
	{
		m_SimulationSpeeds.Clear();
		m_SimulationSpeeds.AddRange(speeds);
	}

	public static void SetSimulationSpeedIndex(int index)
	{
		if (index >= 0 && index < m_SimulationSpeeds.Count)
		{
			m_SimulationSpeedIndex = index;
			if (GameUI.m_Instance != null && GameUI.m_Instance.m_TopBar != null)
			{
				GameUI.m_Instance.m_TopBar.SetSimSpeedLabel(GetTimeScaleForDisplay());
				GameUI.m_Instance.m_TopBar.m_BridgeSimSpeedSlider.SetValue(m_SimulationSpeedIndex + 1);
			}
		}
	}

	public static void SetSimulationSpeedAbsolute(float normalizedSpeed)
	{
		for (int i = 0; i < m_SimulationSpeeds.Count; i++)
		{
			if (Mathf.Approximately(m_SimulationSpeeds[i], normalizedSpeed))
			{
				SetSimulationSpeedIndex(i);
				break;
			}
		}
	}

	public static void SetTimeScaleForSimulation()
	{
		m_SimulationSpeedIndex = Mathf.Clamp(m_SimulationSpeedIndex, 0, m_SimulationSpeeds.Count - 1);
		Game.SetTimeScale(m_SimulationSpeeds[m_SimulationSpeedIndex] * m_SimulationSpeedMultiplier);
	}

	public static void SetPitchForSimulation()
	{
		m_SimulationSpeedIndex = Mathf.Clamp(m_SimulationSpeedIndex, 0, m_SimulationSpeeds.Count - 1);
		AudioMixerManager.ChangeSimulationPitch(Mathf.Clamp(m_SimulationSpeeds[m_SimulationSpeedIndex], MIN_SIMULATION_SPEED_FOR_PITCH_INCREASE, MAX_SIMULATION_SPEED_FOR_PITCH_INCREASE));
	}

	public static float GetTimeScaleForSimulation()
	{
		m_SimulationSpeedIndex = Mathf.Clamp(m_SimulationSpeedIndex, 0, m_SimulationSpeeds.Count - 1);
		return m_SimulationSpeeds[m_SimulationSpeedIndex] * m_SimulationSpeedMultiplier;
	}

	public static float GetTimeScaleForDisplay()
	{
		m_SimulationSpeedIndex = Mathf.Clamp(m_SimulationSpeedIndex, 0, m_SimulationSpeeds.Count - 1);
		return m_SimulationSpeeds[m_SimulationSpeedIndex];
	}
}
