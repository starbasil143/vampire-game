
using UnityEngine;

public class EnemyDamagerScript : MonoBehaviour
{
    public float damageAmount;
    public bool destroyOnContact;
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
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
