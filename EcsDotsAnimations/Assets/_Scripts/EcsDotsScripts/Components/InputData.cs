using Unity.Entities;
using Unity.Mathematics;

namespace EcsDotsAnimations.Components
{
    public struct InputData : IComponentData
    {
        public float3 MoveInput;
    }
}