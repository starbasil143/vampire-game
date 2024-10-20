using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class EnemyScript_Skeleton : MonoBehaviour
{
    public GameObject _player;
    public float _attackRate;
    public GameObject _bone;
    private float attackTimer;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        attackTimer = _attackRate;
    }

    private void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        else
        {
            AttackPlayer();
            attackTimer = _attackRate;
        }
    }

    private void AttackPlayer()
    {
        Vector2 attackVector = (_player.transform.position - gameObject.transform.position).normalized;
        GameObject Bone = Instantiate(_bone, new Vector2(transform.position.x, transform.position.y) + attackVector, Quaternion.identity);
        Vector2 throwForce = attackVector * 5;
        Physics2D.IgnoreCollision(Bone.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Bone.GetComponent<Rigidbody2D>().AddForce(throwForce, ForceMode2D.Impulse);
    }


}
