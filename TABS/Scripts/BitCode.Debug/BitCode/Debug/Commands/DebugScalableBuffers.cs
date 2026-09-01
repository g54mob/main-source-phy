using System.Reflection;
using BitCode.Attributes;
using BitCode.Debug.MemberWrappers;
using DdQbeCzwvEdCSCHcDJqhScymDgUBA;
using UnityEngine;

namespace BitCode.Debug.Commands
{
	public sealed class DebugScalableBuffers
	{
		private static readonly DebugScalableBuffers unkCMXdDaHlgFnStRuNbxzrbnMID = new DebugScalableBuffers();

		public float X
		{
			get
			{
				return ScalableBufferManager.widthScaleFactor;
			}
			set
			{
				ScalableBufferManager.ResizeBuffers(value, ScalableBufferManager.heightScaleFactor);
			}
		}

		public float Y
		{
			get
			{
				return ScalableBufferManager.heightScaleFactor;
			}
			set
			{
				ScalableBufferManager.ResizeBuffers(ScalableBufferManager.widthScaleFactor, value);
			}
		}

		[DebugCommand(Name = "DynamicResolution", Description = "Push the dynamic resolution context onto the stack.")]
		public static DebugScalableBuffers PushBuffers()
		{
			return unkCMXdDaHlgFnStRuNbxzrbnMID;
		}

		[DebugCommand(Description = "Force dynamic resolution on or off on all cameras.")]
		public void AllCamerasEnabled(bool enabled = true)
		{
			Camera[] array = Resources.FindObjectsOfTypeAll<Camera>();
			int num = 0;
			while (true)
			{
				int num2 = 2024312534;
				while (true)
				{
					uint num3;
					switch ((num3 = (uint)(num2 ^ 0x20DDB081)) % 6)
					{
					case 0u:
						break;
					default:
						return;
					case 3u:
						num2 = (int)(num3 * 418339963) ^ -790449064;
						continue;
					case 5u:
						array[num].allowDynamicResolution = enabled;
						num2 = 309067285;
						continue;
					case 2u:
						num++;
						num2 = (int)(num3 * 1950524859) ^ -252753527;
						continue;
					case 4u:
					{
						int num4;
						if (num >= array.Length)
						{
							num2 = 1936747976;
							num4 = num2;
						}
						else
						{
							num2 = 1009686502;
							num4 = num2;
						}
						continue;
					}
					case 1u:
						return;
					}
					break;
				}
			}
		}

		[DebugCommand(Description = "Set the current scaling value")]
		public void SetScaling(float x, float y = -1f)
		{
			if (y <= 0f)
			{
				goto IL_0008;
			}
			goto IL_0040;
			IL_0008:
			int num = 506337543;
			goto IL_000d;
			IL_000d:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x6AA4FDF6)) % 4)
				{
				case 0u:
					break;
				default:
					return;
				case 1u:
					y = x;
					num = ((int)num2 * -2056407049) ^ 0x5E0DA386;
					continue;
				case 3u:
					goto IL_0040;
				case 2u:
					return;
				}
				break;
			}
			goto IL_0008;
			IL_0040:
			ScalableBufferManager.ResizeBuffers(x, y);
			num = 556096456;
			goto IL_000d;
		}

		[DebugCommand(Description = "Gets or sets the width scale.")]
		public IPropertyWrapper Width()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(this, typeof(DebugScalableBuffers), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, "X");
		}

		[DebugCommand(Description = "Gets or sets the height scale.")]
		public IPropertyWrapper Height()
		{
			return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(this, typeof(DebugScalableBuffers), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, "Y");
		}
	}
}
