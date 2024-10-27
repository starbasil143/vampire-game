using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Run At", menuName = "Enemy Logic/Attack Logic/Run At")]
public class EnemyAttackRunAt : EnemyAttackSOBase
{
    private float _timer;
    private float _exitTimer;

    [SerializeField] private float _movementSpeed = 3f;
    [SerializeField] private float _timeUntilExit = 3f;
    [SerializeField] private float _distanceToCountExit = 3f;
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

        
        if (Vector2.Distance(playerTransform.position, enemy.transform.position) > _distanceToCountExit)
        {
            _exitTimer += Time.deltaTime;
            if (_exitTimer > _timeUntilExit)
            {
                _exitTimer = 0f;
                enemy.StateMachine.ChangeState(enemy.IdleState);
            }
        }
        else
        {
            _exitTimer = 0f;
        }

        _timer += Time.deltaTime;
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
