using System.Collections.Generic;
using Battlehub.RTCommon;
using UnityEngine;
using UnityEngine.Rendering;

namespace Battlehub
{
	public static class CameraExt
	{
		private static readonly Dictionary<Camera, Dictionary<CameraEvent, List<CommandBuffer>>> s_commandBuffers = new Dictionary<Camera, Dictionary<CameraEvent, List<CommandBuffer>>>();

		private static readonly IList<CommandBuffer> s_empty = new CommandBuffer[0];

		public static bool HasCmdBuffers(this Camera camera)
		{
			if (RenderPipelineInfo.Type == RPType.Standard)
			{
				return camera.commandBufferCount > 0;
			}
			return s_commandBuffers.ContainsKey(camera);
		}

		public static void AddCmdBuffer(this Camera camera, CameraEvent cameraEvent, CommandBuffer commandBuffer)
		{
			if (RenderPipelineInfo.Type == RPType.Standard)
			{
				camera.AddCommandBuffer(cameraEvent, commandBuffer);
				return;
			}
			if (!s_commandBuffers.TryGetValue(camera, out var value))
			{
				value = new Dictionary<CameraEvent, List<CommandBuffer>>();
				s_commandBuffers.Add(camera, value);
			}
			if (!value.TryGetValue(cameraEvent, out var value2))
			{
				value2 = new List<CommandBuffer>();
				value.Add(cameraEvent, value2);
			}
			value2.Add(commandBuffer);
		}

		public static void RemoveCmdBuffer(this Camera camera, CameraEvent cameraEvent, CommandBuffer commandBuffer)
		{
			if (RenderPipelineInfo.Type == RPType.Standard)
			{
				camera.RemoveCommandBuffer(cameraEvent, commandBuffer);
			}
			else
			{
				if (!s_commandBuffers.TryGetValue(camera, out var value) || !value.TryGetValue(cameraEvent, out var value2))
				{
					return;
				}
				value2.Remove(commandBuffer);
				if (value2.Count == 0)
				{
					value.Remove(cameraEvent);
					if (value.Count == 0)
					{
						s_commandBuffers.Remove(camera);
					}
				}
			}
		}

		public static void RemoveAllCmdBuffers(this Camera camera)
		{
			Dictionary<CameraEvent, List<CommandBuffer>> value;
			if (RenderPipelineInfo.Type == RPType.Standard)
			{
				camera.RemoveAllCommandBuffers();
			}
			else if (s_commandBuffers.TryGetValue(camera, out value))
			{
				s_commandBuffers.Remove(camera);
			}
		}

		public static IList<CommandBuffer> GetCmdBuffers(this Camera camera, CameraEvent cameraEvent)
		{
			if (RenderPipelineInfo.Type == RPType.Standard)
			{
				return camera.GetCommandBuffers(cameraEvent);
			}
			if (!s_commandBuffers.TryGetValue(camera, out var value))
			{
				return s_empty;
			}
			if (!value.TryGetValue(cameraEvent, out var value2))
			{
				return s_empty;
			}
			return value2;
		}
	}
}
