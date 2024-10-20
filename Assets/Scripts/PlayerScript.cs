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
    private Rigidbody2D _rigidbody;
    private bool isChargingSpell;
    public float spellChargeSpeed = .02f;
    private float spellCharge;
    public float maxSoulAmount = 25f;
    public float soulAmount = 25f;

    private void Awake()
    {
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        _rigidbody = GetComponent<Rigidbody2D>();
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
}
