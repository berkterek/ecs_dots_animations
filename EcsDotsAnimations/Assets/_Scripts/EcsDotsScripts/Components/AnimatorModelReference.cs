using EcsDotsAnimations.Controllers;
using Unity.Entities;

namespace EcsDotsAnimations.Components
{
    public class AnimatorModelReference : ICleanupComponentData
    {
        public AnimatorController AnimatorReference;
    }
}