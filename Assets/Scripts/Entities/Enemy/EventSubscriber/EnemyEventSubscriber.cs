using System;
using Game.Components;
using Game.Components.Chasing;
using Game.Components.Jumping;
using Game.Components.Rotating;

namespace Game.Entities.Enemy.EventSubscriber
{
    public static class EnemyEventSubscriber
    {
        public static void SubscribeComponents(
            ChaseComponent chaseComponent,
            DetectComponent detectComponent,
            RotateComponent rotateComponent,
            JumpComponent jumpComponent)
        {
            detectComponent.OnTargetDetected += rotateComponent.LookAt;
            chaseComponent.OnNavMeshLinkFounded += jumpComponent.Jump;
        }



        public static void UnsubscribeComponents(
            ChaseComponent chaseComponent,
            DetectComponent detectComponent,
            RotateComponent rotateComponent,
            JumpComponent jumpComponent)
        {
            detectComponent.OnTargetDetected -= rotateComponent.LookAt;
            chaseComponent.OnNavMeshLinkFounded -= jumpComponent.Jump;
        }
    }
}