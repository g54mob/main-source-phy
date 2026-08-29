using System;

namespace GLTFast.FakeSchema
{
	[Serializable]
	internal class Mesh : NamedObject
	{
		public MeshPrimitive[] primitives;
	}
}
