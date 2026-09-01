using UnityEngine;

namespace NaughtyAttributes
{
	public static class EColorExtensions
	{
		public static Color GetColor(this EColor color)
		{
			return color switch
			{
				EColor.Clear => new Color32(0, 0, 0, 0), 
				EColor.White => new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue), 
				EColor.Black => new Color32(0, 0, 0, byte.MaxValue), 
				EColor.Gray => new Color32(128, 128, 128, byte.MaxValue), 
				EColor.Red => new Color32(byte.MaxValue, 0, 63, byte.MaxValue), 
				EColor.Pink => new Color32(byte.MaxValue, 152, 203, byte.MaxValue), 
				EColor.Orange => new Color32(byte.MaxValue, 128, 0, byte.MaxValue), 
				EColor.Yellow => new Color32(byte.MaxValue, 211, 0, byte.MaxValue), 
				EColor.Green => new Color32(98, 200, 79, byte.MaxValue), 
				EColor.Blue => new Color32(0, 135, 189, byte.MaxValue), 
				EColor.Indigo => new Color32(75, 0, 130, byte.MaxValue), 
				EColor.Violet => new Color32(128, 0, byte.MaxValue, byte.MaxValue), 
				_ => new Color32(0, 0, 0, byte.MaxValue), 
			};
		}
	}
}
