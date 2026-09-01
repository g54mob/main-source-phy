using UnityEngine;

namespace Shapes;

public struct DiscColors
{
	public Color innerStart;

	public Color outerStart;

	public Color innerEnd;

	public Color outerEnd;

	internal DiscColors(Color innerStart, Color outerStart, Color innerEnd, Color outerEnd)
	{
		//IL_000f: Expected O, but got F4
		//IL_001e: Expected O, but got F4
		//IL_002d: Expected O, but got F4
		this.innerStart = (Color)innerStart.r;
		this.outerStart = (Color)outerStart.r;
		this.innerEnd = (Color)innerEnd.r;
		object obj = default(object);
		this.outerEnd = (Color)obj;
	}

	public unsafe static DiscColors Flat(Color color)
	{
		//IL_0012: Expected O, but got F4
		//IL_000d: Expected native int or pointer, but got O
		//IL_0024: Expected O, but got F4
		//IL_001f: Expected native int or pointer, but got O
		//IL_0036: Expected O, but got F4
		//IL_0031: Expected native int or pointer, but got O
		//IL_0048: Expected O, but got F4
		//IL_0043: Expected native int or pointer, but got O
		DiscColors discColors = default(DiscColors);
		((DiscColors*)(nint)discColors)->innerStart = (Color)color.r;
		((DiscColors*)(nint)discColors)->outerStart = (Color)color.r;
		((DiscColors*)(nint)discColors)->innerEnd = (Color)color.r;
		((DiscColors*)(nint)discColors)->outerEnd = (Color)color.r;
		return discColors;
	}

	public unsafe static DiscColors Radial(Color inner, Color outer)
	{
		//IL_0012: Expected O, but got F4
		//IL_000d: Expected native int or pointer, but got O
		//IL_0024: Expected O, but got F4
		//IL_001f: Expected native int or pointer, but got O
		//IL_0036: Expected O, but got F4
		//IL_0031: Expected native int or pointer, but got O
		//IL_0048: Expected O, but got F4
		//IL_0043: Expected native int or pointer, but got O
		DiscColors discColors = default(DiscColors);
		((DiscColors*)(nint)discColors)->innerStart = (Color)inner.r;
		((DiscColors*)(nint)discColors)->outerStart = (Color)outer.r;
		((DiscColors*)(nint)discColors)->innerEnd = (Color)inner.r;
		((DiscColors*)(nint)discColors)->outerEnd = (Color)outer.r;
		return discColors;
	}

	public unsafe static DiscColors Angular(Color start, Color end)
	{
		//IL_0012: Expected O, but got F4
		//IL_000d: Expected native int or pointer, but got O
		//IL_0024: Expected O, but got F4
		//IL_001f: Expected native int or pointer, but got O
		//IL_0036: Expected O, but got F4
		//IL_0031: Expected native int or pointer, but got O
		//IL_0048: Expected O, but got F4
		//IL_0043: Expected native int or pointer, but got O
		DiscColors discColors = default(DiscColors);
		((DiscColors*)(nint)discColors)->innerStart = (Color)start.r;
		((DiscColors*)(nint)discColors)->outerStart = (Color)start.r;
		((DiscColors*)(nint)discColors)->innerEnd = (Color)end.r;
		((DiscColors*)(nint)discColors)->outerEnd = (Color)end.r;
		return discColors;
	}

	public unsafe static DiscColors Bilinear(Color innerStart, Color outerStart, Color innerEnd, Color outerEnd)
	{
		//IL_0012: Expected O, but got F4
		//IL_000d: Expected native int or pointer, but got O
		//IL_0024: Expected O, but got F4
		//IL_001f: Expected native int or pointer, but got O
		//IL_0036: Expected O, but got F4
		//IL_0031: Expected native int or pointer, but got O
		//IL_003e: Expected native int or pointer, but got O
		DiscColors discColors = default(DiscColors);
		((DiscColors*)(nint)discColors)->innerStart = (Color)innerStart.r;
		((DiscColors*)(nint)discColors)->outerStart = (Color)outerStart.r;
		((DiscColors*)(nint)discColors)->innerEnd = (Color)innerEnd.r;
		object obj = default(object);
		((DiscColors*)(nint)discColors)->outerEnd = (Color)obj;
		return discColors;
	}

	public unsafe static implicit operator DiscColors(Color flatColor)
	{
		//IL_0012: Expected O, but got F4
		//IL_000d: Expected native int or pointer, but got O
		//IL_0024: Expected O, but got F4
		//IL_001f: Expected native int or pointer, but got O
		//IL_0036: Expected O, but got F4
		//IL_0031: Expected native int or pointer, but got O
		//IL_0048: Expected O, but got F4
		//IL_0043: Expected native int or pointer, but got O
		DiscColors discColors = default(DiscColors);
		((DiscColors*)(nint)discColors)->innerStart = (Color)flatColor.r;
		((DiscColors*)(nint)discColors)->outerStart = (Color)flatColor.r;
		((DiscColors*)(nint)discColors)->innerEnd = (Color)flatColor.r;
		((DiscColors*)(nint)discColors)->outerEnd = (Color)flatColor.r;
		return discColors;
	}
}
