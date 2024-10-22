using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Direct Chase", menuName = "Enemy Logic/Chase Logic/Direct Chase")]
public class EnemyChaseDirectToPlayer : EnemyChaseSOBase
{
    [SerializeField] private float _movementSpeed = 1.5f;

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        

        Vector2 moveDirection = (playerTransform.position - enemy.transform.position).normalized;
        enemy.MoveEnemy(moveDirection * _movementSpeed);

        if (enemy.IsInAttackRange)
        {
            enemy.StateMachine.ChangeState(enemy.AttackState);
        }
        if (!enemy.IsInChaseRange)
        {
            enemy.StateMachine.ChangeState(enemy.IdleState);
        }
    }
    public override void DoPhysicsUpdateLogic()
    {
        base.DoPhysicsUpdateLogic();
    }
    public override void ResetValues()
    {
        base.ResetValues();
    }
}
