using EcsDotsAnimations.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace EcsDotsScripts.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(TransformSystemGroup))]
    public partial struct MoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            new MoveJob()
            {
                DeltaTime = deltaTime
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct MoveJob : IJobEntity
    {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(Entity entity, in InputData inputData, ref LocalTransform localTransform,
            in MoveData moveData)
        {
            var moveInputValue = inputData.MoveInput;
            if (math.length(moveInputValue) <= 0f) return;

            var moveDirection = DeltaTime * moveData.MoveSpeed * moveInputValue;
            localTransform.Position += moveDirection;

            var targetRotation = quaternion.LookRotation(moveDirection, new float3(0f, 1f, 0f));
            localTransform.Rotation = targetRotation;
        }
    }
}