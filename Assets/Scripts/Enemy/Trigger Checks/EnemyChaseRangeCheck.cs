
using UnityEngine;

public class EnemyChaseRangeCheck : MonoBehaviour
{
    public GameObject PlayerTarget { get; set; }
    private Enemy _enemy;

    private void Awake()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");
        _enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == PlayerTarget)
        {
            _enemy.SetChaseRangeStatus(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == PlayerTarget)
        {
            _enemy.SetChaseRangeStatus(false);
        }
    }
}
