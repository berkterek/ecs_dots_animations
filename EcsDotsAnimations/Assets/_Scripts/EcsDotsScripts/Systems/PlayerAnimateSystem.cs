using EcsDotsAnimations.Components;
using EcsDotsAnimations.Controllers;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace EcsDotsScripts.Systems
{
    [UpdateInGroup(typeof(PresentationSystemGroup), OrderFirst = true)]
    public partial struct PlayerAnimateSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            foreach (var (animatorModelData, entity) in SystemAPI.Query<AnimatorModelData>()
                         .WithNone<AnimatorModelReference>().WithEntityAccess())
            {
                var newCompanionGameObject = Object.Instantiate(animatorModelData.AnimatorModelPrefab);

                var newAnimatorModelReference = new AnimatorModelReference()
                {
                    AnimatorReference = newCompanionGameObject.GetComponent<AnimatorController>()
                };

                ecb.AddComponent(entity, newAnimatorModelReference);
            }

            foreach (var (moveDataRO, animatorModelReference, localTransformRO) in SystemAPI.Query<RefRO<MoveData>, AnimatorModelReference, RefRO<LocalTransform>>())
            {
                animatorModelReference.AnimatorReference.MoveAnimation(moveDataRO.ValueRO.Velecity);
                animatorModelReference.AnimatorReference.Transform.position = localTransformRO.ValueRO.Position;
                animatorModelReference.AnimatorReference.Transform.rotation = localTransformRO.ValueRO.Rotation;
            }

            foreach (var (animatorModelReference, entity) in SystemAPI.Query<AnimatorModelReference>().WithNone<AnimatorModelData,LocalTransform>().WithEntityAccess())
            {
                Object.Destroy(animatorModelReference.AnimatorReference.gameObject);
                ecb.RemoveComponent<AnimatorModelReference>(entity);
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}