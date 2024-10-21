using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private GameObject _player;
    private float _timer;
    private float _timeBetweenShots = 1f;

    private float _exitTimer;
    private float _timeUntilExit = 3f;
    private float _distanceToCountExit = 3f;
    private float _projectileSpeed = 5f;
    public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        enemy.MoveEnemy(Vector2.zero);

        if (_timer > _timeBetweenShots)
        {
            _timer = 0f;
            
            Vector2 attackDir = (_player.transform.position - enemy.transform.position).normalized;
            Rigidbody2D projectile = GameObject.Instantiate(enemy.ProjectilePrefab, enemy.transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = attackDir * _projectileSpeed;  
        }

        if (Vector2.Distance(_player.transform.position, enemy.transform.position) > _distanceToCountExit)
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

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void AttackPlayer()
    {
        Vector2 attackVector = (_player.transform.position - enemy.transform.position).normalized;
        Rigidbody2D Projectile = GameObject.Instantiate(enemy.ProjectilePrefab, new Vector2(enemy.transform.position.x, enemy.transform.position.y) + attackVector, Quaternion.identity);
        //Vector2 throwForce = attackVector * _projectileSpeed;
        Physics2D.IgnoreCollision(Projectile.GetComponent<Collider2D>(), enemy.GetComponent<Collider2D>());
        Projectile.GetComponent<Rigidbody2D>().velocity = attackVector * _projectileSpeed;
    }
}
