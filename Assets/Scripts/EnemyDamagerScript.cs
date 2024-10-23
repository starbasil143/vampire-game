using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

public class EnemyDamagerScript : MonoBehaviour
{
    public float damageAmount;
    [SerializeField] private float existenceTime = 10f;
    private float existenceTimer = 0f;

    private void Awake()
    {
        
    }
    private void Update()
    {
        existenceTimer += Time.deltaTime;
        if (existenceTimer >= existenceTime)
        {
            Destroy(gameObject);
        }
    }
}
