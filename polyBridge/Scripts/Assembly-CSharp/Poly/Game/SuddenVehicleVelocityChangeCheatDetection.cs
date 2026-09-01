using System.Collections.Generic;
using Pb;
using Poly.Physics;
using UnityEngine;

namespace Poly.Game
{
	public class SuddenVehicleVelocityChangeCheatDetection : ListenerBase, IActionListener, IWorldListener
	{
		[Range(2f, 50f)]
		public int historyLength = 15;

		[Range(1f, 100f)]
		public float speedDiffTolerance = 7f;

		private List<Poly.Physics.Vehicle> vehicles = new List<Poly.Physics.Vehicle>();

		private Dictionary<Poly.Physics.Rigidbody, FloatHistory> bodySpeedHistory = new Dictionary<Poly.Physics.Rigidbody, FloatHistory>();

		private bool isCheated;

		private int historyIdx;

		public void BeforeStep()
		{
		}

		public void AfterWorldCleared()
		{
			vehicles.Clear();
			bodySpeedHistory.Clear();
			isCheated = false;
			historyIdx = -1;
		}

		public void AfterWorldFrameUpdate()
		{
		}

		public void AfterWorldFixedUpdate()
		{
			if (isCheated)
			{
				return;
			}
			int num = 0;
			while (!isCheated && num < vehicles.Count)
			{
				WheelJoint[] allJoints = vehicles[num].allJoints;
				for (int i = 0; i < allJoints.Length; i++)
				{
					if (allJoints[i].isBroken)
					{
						if (GameManager.CurrentLevelHasLeaderboards() || BridgeCheat.m_AllowPhysicsCheatFlagsInSandbox)
						{
							BridgeCheat.SetCheated(CheatReason.VEHICLE_JOINT_BROKEN);
							isCheated = true;
						}
						break;
					}
				}
				num++;
			}
			if (isCheated)
			{
				return;
			}
			historyIdx = (historyIdx + 1) % historyLength;
			foreach (KeyValuePair<Poly.Physics.Rigidbody, FloatHistory> item in bodySpeedHistory)
			{
				Poly.Physics.Rigidbody key = item.Key;
				item.Value.Add(key.linearVelocity.magnitude);
			}
			foreach (Poly.Physics.Vehicle vehicle in vehicles)
			{
				FallingRoadCheatDetectionListener component = vehicle.GetComponent<FallingRoadCheatDetectionListener>();
				if ((bool)component && component.fallingRoadDetectedInLast10Frames)
				{
					Poly.Physics.Rigidbody[] chassis = vehicle.chassis;
					foreach (Poly.Physics.Rigidbody key2 in chassis)
					{
						FloatHistory speedHistory = bodySpeedHistory[key2];
						float num2 = CalcMaxVelChange_Method02(speedHistory);
						if (speedDiffTolerance < num2)
						{
							if (GameManager.CurrentLevelHasLeaderboards() || BridgeCheat.m_AllowPhysicsCheatFlagsInSandbox)
							{
								BridgeCheat.SetCheated(CheatReason.VEHICLE_HIGH_ACCELERATION);
								isCheated = true;
							}
							break;
						}
					}
				}
				if (isCheated)
				{
					break;
				}
			}
		}

		private float CalcMaxVelChange_Method01(FloatHistory speedHistory)
		{
			float num = float.MaxValue;
			float num2 = float.MinValue;
			float[] array = new float[historyLength];
			float[] array2 = new float[historyLength];
			for (int i = 0; i < historyLength; i++)
			{
				int num3 = historyLength - 1 - i;
				num = Pb.Mathf.Min(num, speedHistory[i]);
				num2 = Pb.Mathf.Max(num2, speedHistory[num3]);
				array[i] = num;
				array2[num3] = num2;
			}
			float num4 = 0f;
			for (int j = 0; j < historyLength; j++)
			{
				float b = array2[j] - array[j];
				num4 = Pb.Mathf.Max(num4, b);
			}
			return num4;
		}

		private float CalcMaxVelChange_Method02(FloatHistory speedHistory)
		{
			float num = 0f;
			float num2 = 0f;
			for (int i = 0; i < historyLength / 3; i++)
			{
				num = Pb.Mathf.Max(num, speedHistory[i]);
			}
			for (int j = historyLength * 2 / 3; j < historyLength; j++)
			{
				num2 = Pb.Mathf.Max(num2, speedHistory[j]);
			}
			return Pb.Mathf.Max(0f, num2 - num);
		}

		public void OnActionAdded(Action a)
		{
			Poly.Physics.Vehicle vehicle = a as Poly.Physics.Vehicle;
			if ((bool)vehicle)
			{
				vehicles.Add(vehicle);
				Poly.Physics.Rigidbody[] chassis = vehicle.chassis;
				foreach (Poly.Physics.Rigidbody key in chassis)
				{
					bodySpeedHistory.Add(key, new FloatHistory(historyLength));
				}
			}
		}

		public void OnActionRemoved(Action a)
		{
			Poly.Physics.Vehicle vehicle = a as Poly.Physics.Vehicle;
			if ((bool)vehicle)
			{
				vehicles.Remove(a as Poly.Physics.Vehicle);
				Poly.Physics.Rigidbody[] chassis = vehicle.chassis;
				foreach (Poly.Physics.Rigidbody key in chassis)
				{
					bodySpeedHistory.Remove(key);
				}
			}
		}
	}
}
