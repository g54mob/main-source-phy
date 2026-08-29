using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct PaintableEdit
{
	public Color32 brushColor;

	public Vector2 brushCenterViewport;

	public float brushSizeViewport;

	public Vector3 hitPointInLocalSpace;

	public Vector3 cameraPosition;

	public int packedCameraRotation;

	public ushort screenWidth;

	public ushort screenHeight;

	public byte pass;

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine($"brushColor: {brushColor}");
		stringBuilder.AppendLine($"brushCenterViewport: {brushCenterViewport}");
		stringBuilder.AppendLine($"brushSizeViewport: {brushSizeViewport}");
		stringBuilder.AppendLine($"hitPointInLocalSpace: {hitPointInLocalSpace}");
		stringBuilder.AppendLine($"cameraPosition: {cameraPosition}");
		stringBuilder.AppendLine($"cameraRotation: {QuaternionCompressor.decompress(packedCameraRotation).eulerAngles}");
		stringBuilder.AppendLine($"screenWidth: {screenWidth}");
		stringBuilder.AppendLine($"screenHeight: {screenHeight}");
		stringBuilder.AppendLine($"pass: {pass}");
		return stringBuilder.ToString();
	}
}
