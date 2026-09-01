using System.Reflection;
using BitCode.Attributes;
using BitCode.Debug.MemberWrappers;
using DdQbeCzwvEdCSCHcDJqhScymDgUBA;
using UnityEngine;

namespace BitCode.Debug.Commands
{
	public sealed class DebugPhysics
	{
		private static readonly DebugPhysics unkCMXdDaHlgFnStRuNbxzrbnMID = new DebugPhysics();

		[DebugCommand(Name = "Physics", Description = "Push the Physics context onto the stack.")]
		public static DebugPhysics PushPhysics()
		{
			return unkCMXdDaHlgFnStRuNbxzrbnMID;
		}

		[DebugCommand(Description = "Tick the physics simulation.")]
		public void Simulate(float deltaTime = 0f)
		{
			if (deltaTime <= 0f)
			{
				while (true)
				{
					int num = 2075205796;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x4B8D32B2)) % 3)
						{
						case 0u:
							break;
						case 1u:
							deltaTime = Time.fixedDeltaTime;
							num = ((int)num2 * -1365377498) ^ -16247838;
							continue;
						default:
							goto end_IL_0008;
						}
						break;
					}
					continue;
					end_IL_0008:
					break;
				}
			}
			Physics.Simulate(deltaTime);
		}

		[DebugCommand(Description = "Gets or sets Physics.gravity.")]
		public IPropertyWrapper Gravity()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(null, typeof(Physics), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, "gravity");
		}

		[DebugCommand(Description = "Gets or sets Physics.autoSimulation.")]
		public IPropertyWrapper AutoSimulation()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(null, typeof(Physics), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, "autoSimulation");
		}

		[DebugCommand(Description = "Gets or sets Physics.autoSyncTransforms.")]
		public IPropertyWrapper AutoSyncTransforms()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(null, typeof(Physics), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, "autoSyncTransforms");
		}

		[DebugCommand(Description = "Gets or sets Physics.defaultSolverIterations.")]
		public IPropertyWrapper DefaultSolverIterations()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(null, typeof(Physics), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, "defaultSolverIterations");
		}

		[DebugCommand(Description = "Gets or sets Physics.defaultSolverVelocityIterations.")]
		public IPropertyWrapper DefaultSolverVelocityIterations()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(null, typeof(Physics), BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, "defaultSolverVelocityIterations");
		}
	}
}
