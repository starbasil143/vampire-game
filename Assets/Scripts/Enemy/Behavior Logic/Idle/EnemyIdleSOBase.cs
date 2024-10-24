using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleSOBase : ScriptableObject
{
    protected Enemy enemy;
    protected Transform transform;
    protected GameObject gameObject;
    protected Transform playerTransform;

    public virtual void Initialize(GameObject gameObject, Enemy enemy)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void DoEnterLogic() { }
    public virtual void DoExitLogic() { }
    public virtual void DoFrameUpdateLogic() 
    {
        if (enemy.IsInChaseRange)
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position + (playerTransform.position - transform.position).normalized * 1.2f, playerTransform.position - transform.position, Mathf.Infinity, enemy.RaycastingMask);
            if (ray.collider != null)
            {
                if (ray.collider.gameObject.CompareTag("Player"))
                {
                    enemy.StateMachine.ChangeState(enemy.ChaseState);
                }
                else
                {
                    Debug.Log(ray.collider.gameObject.name);  
                }
            }
        }
    }
    public virtual void DoPhysicsUpdateLogic() { }
    public virtual void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}
