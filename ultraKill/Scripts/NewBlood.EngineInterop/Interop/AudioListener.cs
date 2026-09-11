using System.Runtime.InteropServices;
using Interop.Unity;
using Microsoft.CodeAnalysis;
using UnityEngine;

namespace Interop
{
	[NonCopyable]
	[SupportsInheritance]
	public struct AudioListener : AudioListener.Interface, IUpCastable<AudioListener>, AudioBehaviour.Interface, IUpCastable<AudioBehaviour>, Behaviour.Interface, IUpCastable<Behaviour>, Interop.Unity.Component.Interface, IUpCastable<Interop.Unity.Component>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
	{
		public interface Interface : IUpCastable<AudioListener>, AudioBehaviour.Interface, IUpCastable<AudioBehaviour>, Behaviour.Interface, IUpCastable<Behaviour>, Interop.Unity.Component.Interface, IUpCastable<Interop.Unity.Component>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
		{
		}

		[BaseField]
		private AudioBehaviour __AudioBehaviour;

		[NativeTypeName("Vector3f")]
		public Vector3 m_LastPosition;

		[NativeTypeName("Vector3f")]
		public Vector3 m_Velocity;

		public int m_VelocityUpdateMode;

		public PPtr<Transform> m_AltTransform;

		[NativeTypeName("Matrix4x4f")]
		public Matrix4x4 m_InverseMatrix;

		public ListNode<AudioListener> m_Node;

		[NativeTypeName("bool")]
		public byte m_ConnectToAudioManager;

		ref AudioListener IUpCastable<AudioListener>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref AudioBehaviour IUpCastable<AudioBehaviour>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __AudioBehaviour, 1));
		}

		ref Behaviour IUpCastable<Behaviour>.Cast()
		{
			return ref CastOperations.UpCast<AudioBehaviour, Behaviour>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __AudioBehaviour, 1)));
		}

		ref Interop.Unity.Component IUpCastable<Interop.Unity.Component>.Cast()
		{
			return ref CastOperations.UpCast<AudioBehaviour, Interop.Unity.Component>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __AudioBehaviour, 1)));
		}

		ref EditorExtension IUpCastable<EditorExtension>.Cast()
		{
			return ref CastOperations.UpCast<AudioBehaviour, EditorExtension>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __AudioBehaviour, 1)));
		}

		ref Object IUpCastable<Object>.Cast()
		{
			return ref CastOperations.UpCast<AudioBehaviour, Object>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __AudioBehaviour, 1)));
		}
	}
}
