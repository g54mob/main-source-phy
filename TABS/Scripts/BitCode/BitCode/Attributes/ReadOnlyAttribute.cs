using System.Runtime.CompilerServices;
using UnityEngine;

namespace BitCode.Attributes
{
	public class ReadOnlyAttribute : PropertyAttribute
	{
		[CompilerGenerated]
		private bool COqOtpSbVuIeZpOhFuaPQyWLtxRL;

		public bool CanChangeInEditMode
		{
			[CompilerGenerated]
			get
			{
				return COqOtpSbVuIeZpOhFuaPQyWLtxRL;
			}
			[CompilerGenerated]
			private set
			{
				COqOtpSbVuIeZpOhFuaPQyWLtxRL = cOqOtpSbVuIeZpOhFuaPQyWLtxRL;
			}
		}

		public ReadOnlyAttribute(bool canChangeInEditMode = false)
		{
			while (true)
			{
				int num = -707539907;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -747723957)) % 3)
					{
					case 2u:
						break;
					default:
						return;
					case 1u:
						goto IL_0028;
					case 0u:
						return;
					}
					break;
					IL_0028:
					CanChangeInEditMode = canChangeInEditMode;
					num = (int)(num2 * 394019627) ^ -435009439;
				}
			}
		}
	}
}
