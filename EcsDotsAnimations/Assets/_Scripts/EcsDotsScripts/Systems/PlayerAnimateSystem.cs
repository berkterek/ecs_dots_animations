using EcsDotsAnimations.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace EcsDotsScripts.Systems
{
    [UpdateInGroup(typeof(PresentationSystemGroup), OrderFirst = true)]
    public partial struct PlayerAnimateSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            foreach (var (animatorModelData, entity) in SystemAPI.Query<AnimatorModelData>()
                         .WithNone<AnimatorModelReference>().WithEntityAccess())
            {
                var newCompanionGameObject = Object.Instantiate(animatorModelData.AnimatorModelPrefab);

                var newAnimatorModelReference = new AnimatorModelReference()
                {
                    AnimatorReference = newCompanionGameObject
                };

                ecb.AddComponent(entity, newAnimatorModelReference);
            }

            foreach (var (inputDataRO, animatorModelReference, localTransformRO) in SystemAPI.Query<RefRO<InputData>, AnimatorModelReference, RefRO<LocalTransform>>())
            {
                float length = math.length(inputDataRO.ValueRO.MoveInput);
                animatorModelReference.AnimatorReference.MoveAnimation(length);
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