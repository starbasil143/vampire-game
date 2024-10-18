using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _player;
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
        }


        #endregion
    }
}
