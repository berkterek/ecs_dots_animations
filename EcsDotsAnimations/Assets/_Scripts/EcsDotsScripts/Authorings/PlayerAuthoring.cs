using EcsDotsAnimations.Components;
using EcsDotsAnimations.Controllers;
using Unity.Entities;
using UnityEngine;

namespace EcsDotsScripts.Authorings
{
    public class PlayerAuthoring : MonoBehaviour
    {
        [SerializeField] AnimatorController Prefab;
        
        private class PlayerAuthoringBaker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                AddComponent<InputData>(entity);

                AddComponentObject(entity, new AnimatorModelData()
                {
                    AnimatorModelPrefab = authoring.Prefab
                });
            }
        }
    }
}