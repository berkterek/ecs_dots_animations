using EcsDotsAnimations.Controllers;
using Unity.Entities;

namespace EcsDotsAnimations.Components
{
    public class AnimatorModelData : IComponentData
    {
        public AnimatorController AnimatorModelPrefab;
    }
}