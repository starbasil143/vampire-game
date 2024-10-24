using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
            Debug.Log("in range");
            Debug.DrawRay(transform.position, playerTransform.position - new Vector3(0,.3f) - transform.position, Color.green);
            RaycastHit2D ray = Physics2D.Raycast(transform.position, playerTransform.position + new Vector3(0, -.3f, 0) - transform.position, 100f, enemy.RaycastingMask);
            if (ray.collider != null)
            {
                if (ray.collider.gameObject.CompareTag("Player"))
                {
                    Debug.Log("player found");
                    enemy.StateMachine.ChangeState(enemy.ChaseState);
                }
                else
                {
                    Debug.Log(ray.collider.gameObject.name);  
                }
            }
            else
            {
                Debug.Log("ray collider null");
            }
        }
    }
    public virtual void DoPhysicsUpdateLogic() { }
    public virtual void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }
}
