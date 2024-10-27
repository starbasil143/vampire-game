
using UnityEngine;

public interface ITriggerCheckable
{
    bool IsInChaseRange { get; set; }
    bool IsInAttackRange { get; set; }

    void SetChaseRangeStatus(bool isInChaseRange);
    void SetAttackRangeStatus(bool isInAttackRange);
}
