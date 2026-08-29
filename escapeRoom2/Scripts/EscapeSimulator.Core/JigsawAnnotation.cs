using System;
using UnityEngine;

public class JigsawAnnotation : InteractiveAnnotation
{
	public enum AnnotationDirection
	{
		Left = 0,
		Right = 1
	}

	public AnnotationDirection direction;

	public Vector3 forward;

	[NonSerialized]
	public JigsawPiece piece;

	private Vector3 localPosition;

	private Quaternion worldRotation;

	private Collider annotationCollider;

	private void Start()
	{
		piece = base.transform.parent.GetComponent<JigsawPiece>();
		annotationCollider = base.transform.GetComponent<Collider>();
		localPosition = base.transform.position - piece.transform.position;
		worldRotation = base.transform.rotation;
	}

	private void LateUpdate()
	{
		annotationCollider.enabled = piece.gameObject.activeInHierarchy;
	}

	public override bool isAnnotationCompatible(Game.ScreenTargetType screenTargetType)
	{
		return screenTargetType == Game.ScreenTargetType.JigsawPiece;
	}
}
