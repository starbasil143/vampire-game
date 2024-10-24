using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    public Image healthBar;
    public bool onPause;

    public AudioSource FireSoundSource;
    public AudioSource DashSoundSource;
    public AudioSource DamageSoundSource;
    public AudioSource GuardSoundSource;
    public AudioSource ParrySoundSource;
    public AudioSource HealSoundSource;

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
        
        healthBar.fillAmount = soulAmount / 100f;
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
            if( !onPause )
            {
                SacredFlame();
            }
        }



        #endregion
    
    
    }

    void SacredFlame()
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x - Screen.width/2, Input.mousePosition.y - Screen.height/2);
        float castAngle = Vector2.SignedAngle(Vector2.right, mousePos);
        if (!Physics2D.Raycast(new Vector2(_player.position.x, _player.position.y) + mousePos.normalized*.5f, mousePos.normalized, .5f, 1<<LayerMask.NameToLayer("Collisions")))
        {
            FireSoundSource.Play();
            GameObject Flame = Instantiate(_projectile, new Vector2(_player.position.x, _player.position.y) + mousePos.normalized, Quaternion.Euler(0, 0, castAngle));
            Vector2 shootForce = mousePos.normalized * 11;
            Physics2D.IgnoreCollision(Flame.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
            Flame.GetComponent<Rigidbody2D>().AddForce(shootForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Harm") && collision.gameObject.GetComponent<HarmfulObjectScript>().canDamagePlayer)
        {
            if (isGuarding && collision.gameObject.GetComponent<HarmfulObjectScript>().isBlockable)
            {
                ParrySoundSource.Play();
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
   
        if (collision.gameObject.CompareTag("HealPickup"))
        {
            Heal(50f);
            Destroy(collision.gameObject);
        }
    }

    private void Damage(float damage)
    {
        soulAmount -= damage;
        healthBar.fillAmount = soulAmount / 100f;
        _animator.Play("Damage", -1, 0f);
        DamageSoundSource.Play();
        Debug.Log(soulAmount);
        if(soulAmount <= 0)
        {
            Die();
        }
    }

    private void Heal(float health)
    {
        if (soulAmount + health <= maxSoulAmount)
        {
            soulAmount += health;
        }
        else
        {
            soulAmount = maxSoulAmount;
        }
        
        healthBar.fillAmount = soulAmount / 100f;
        HealSoundSource.Play();
    }

    private void Die()
    {
        gameObject.transform.localScale = new Vector3(1,.1f,1);
    }


    #region Light Stuff

    public void LightOff()
    {
        _animator.Play("LightOff");
    }
    public void LightOn()
    {
        _animator.Play("LightActivate");
    }
    public void LightFlicker()
    {
        _animator.Play("LightFlicker");
    }

    #endregion
}
