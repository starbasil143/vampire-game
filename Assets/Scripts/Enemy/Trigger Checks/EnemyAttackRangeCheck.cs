
using UnityEngine;

public class EnemyAttackRangeCheck : MonoBehaviour
{
    public GameObject PlayerTarget { get; set; }

    private Enemy _enemy;

    private void Awake()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");
        _enemy = gameObject.GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == PlayerTarget)
        {
            _enemy.SetAttackRangeStatus(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == PlayerTarget)
        {
            _enemy.SetAttackRangeStatus(false);
        }
    }
}
