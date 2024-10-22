using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Straight-Single Projectile", menuName = "Enemy Logic/Attack Logic/Straight Single Projectile")]
public class EnemyAttackSingleStraightProjectile : EnemyAttackSOBase
{
    [SerializeField] private Rigidbody2D ProjectilePrefab;
    private float _timer;
    private float _exitTimer;

    [SerializeField] private float _timeBetweenShots = 1f;
    [SerializeField] private float _projectileSpeed = 5f;
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

        enemy.MoveEnemy(Vector2.zero);

        if (_timer > _timeBetweenShots)
        {
            _timer = 0f;
            
            Vector2 attackDir = (playerTransform.position - enemy.transform.position).normalized;
            Rigidbody2D projectile = GameObject.Instantiate(ProjectilePrefab, enemy.transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = attackDir * _projectileSpeed;  
        }

        if (Vector2.Distance(playerTransform.position, enemy.transform.position) > _distanceToCountExit)
        {
            _exitTimer += Time.deltaTime;
            if (_exitTimer > _timeUntilExit)
            {
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
