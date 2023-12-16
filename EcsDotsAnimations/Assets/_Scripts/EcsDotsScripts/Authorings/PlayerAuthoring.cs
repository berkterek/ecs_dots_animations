using EcsDotsAnimations.Components;
using Unity.Entities;
using UnityEngine;

namespace EcsDotsScripts.Authorings
{
    public class PlayerAuthoring : MonoBehaviour
    {
        private class PlayerAuthoringBaker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent<InputData>(entity);
            }
        }
    }
}