using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FracturePattern", menuName = "Besiege/Build Surface Fracture Pattern")]
public class FracturePattern : ScriptableObject
{
	public const int ColliderCountU = 3;

	public const int ColliderCountV = 3;

	[Header("Quad Surface")]
	public FracturePiece QuadBottomRight;

	public FracturePiece QuadBottomLeft;

	public FracturePiece QuadTopLeft;

	public FracturePiece QuadTopRight;

	public FracturePiece QuadMiddle;

	[Header("Tri Surface")]
	public FracturePiece TriTop;

	public FracturePiece TriBottomRight;

	public FracturePiece TriBottomLeft;

	public FracturePiece TriMiddle;

	[HideInInspector]
	[SerializeField]
	public int[] ColliderFragmentMapping;

	public int GetCount(bool quad)
	{
		return (!quad) ? 4 : 5;
	}

	public FracturePiece Get(int index, bool quad)
	{
		if (quad)
		{
			switch (index)
			{
			case 0:
				return QuadBottomLeft;
			case 1:
				return QuadBottomRight;
			case 2:
				return QuadTopRight;
			case 3:
				return QuadTopLeft;
			case 4:
				return QuadMiddle;
			default:
				throw new IndexOutOfRangeException();
			}
		}
		switch (index)
		{
		case 0:
			return TriTop;
		case 1:
			return TriBottomLeft;
		case 2:
			return TriBottomRight;
		case 3:
			return TriMiddle;
		default:
			throw new IndexOutOfRangeException();
		}
	}
}
