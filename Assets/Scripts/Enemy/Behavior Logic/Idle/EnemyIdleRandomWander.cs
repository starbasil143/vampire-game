using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Random Wander", menuName = "Enemy Logic/Idle Logic/Random Wander")]
public class EnemyIdleRandomWander : EnemyIdleSOBase
{
    [SerializeField] private float RandomMovementRange = 5f;
    [SerializeField] private float RandomMovementSpeed = 1f;
    
    private Vector3 _targetPos;
    private Vector3 _direction; 
    private float currentMoveTimerMax;
    private float currentMoveTimer = 0;

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }
    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        
        _targetPos = GetRandomPointInCircle();
        currentMoveTimerMax = 2 * Mathf.Abs((_targetPos - transform.position).magnitude);
    }
    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }
    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
    }
    public override void DoPhysicsUpdateLogic()
    {
        base.DoPhysicsUpdateLogic();

        currentMoveTimer += Time.deltaTime;

        _direction = (_targetPos - enemy.transform.position).normalized;

        enemy.MoveEnemy(_direction * RandomMovementSpeed);

        if((enemy.transform.position - _targetPos).sqrMagnitude < 0.01f || currentMoveTimer >= currentMoveTimerMax)
        {
            _targetPos = GetRandomPointInCircle();
            currentMoveTimerMax = Mathf.Abs((_targetPos - transform.position).magnitude);
            currentMoveTimer = 0;
        }
    }
    public override void ResetValues()
    {
        base.ResetValues();
    }
    private Vector3 GetRandomPointInCircle()
    {
        return enemy.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * RandomMovementRange; 
    }
}
