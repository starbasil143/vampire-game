using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerScript : MonoBehaviour
{

    
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _projectile;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private bool isChargingSpell;
    public float spellChargeSpeed = .02f;
    private float spellCharge;
    public float maxSoulAmount = 100f;
    public float soulAmount = 100f;

    public enum PlayerState
    {
        Idle,
        Walking,
        Dashing,
        Defeated
    }

    public PlayerState currentPlayerState;
    public bool isGuarding;

    private void Awake()
    {
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentPlayerState = PlayerState.Idle;
    }

    void Update()
    {
        #region CASTING
        /*
        if(InputManager.Casting)
        {
            if(!isChargingSpell)
            {
                isChargingSpell = true;
                Debug.Log("casting begin!"); //this is where the animation would start
                spellCharge += Time.deltaTime * spellChargeSpeed;
            }
            if(isChargingSpell)
            {
                spellCharge += Time.deltaTime * spellChargeSpeed;
            }
        }*/


        if (InputManager.CastingUp)
        {
            SacredFlame();
        }



        #endregion
    
    
    }

    void SacredFlame()
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x - Screen.width/2, Input.mousePosition.y - Screen.height/2);
        float castAngle = Vector2.SignedAngle(Vector2.right, mousePos);
        GameObject Flame = Instantiate(_projectile, new Vector2(_player.position.x, _player.position.y) + mousePos.normalized, Quaternion.Euler(0, 0, castAngle));
        Vector2 shootForce = mousePos.normalized * 11;
        Physics2D.IgnoreCollision(Flame.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        Flame.GetComponent<Rigidbody2D>().AddForce(shootForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Harm") && collision.gameObject.GetComponent<HarmfulObjectScript>().canDamagePlayer)
        {
            if (isGuarding && collision.gameObject.GetComponent<HarmfulObjectScript>().isBlockable)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = 
                collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude 
                * (collision.gameObject.transform.position - transform.position).normalized;

                if (collision.gameObject.GetComponent<RotateScript>())
                {
                    collision.gameObject.GetComponent<RotateScript>().rotationSpeed *= -1;
                }
                
                if (collision.gameObject.GetComponent<HarmfulObjectScript>().Source)
                {
                    Physics2D.IgnoreCollision(
                    collision.gameObject.GetComponent<Collider2D>(), 
                    collision.gameObject.GetComponent<HarmfulObjectScript>().Source.GetComponent<Collider2D>(), 
                    false);
                }
            }
            else
            {
                Damage(collision.gameObject.GetComponent<HarmfulObjectScript>().damageAmount);
                if (collision.gameObject.GetComponent<HarmfulObjectScript>().destroyOnContact)
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }

    private void Damage(float damage)
    {
        soulAmount -= damage;
        _animator.Play("Damage", -1, 0f);
        Debug.Log(soulAmount);
        if(soulAmount <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.transform.localScale = new Vector3(1,.1f,1);
    }
}
