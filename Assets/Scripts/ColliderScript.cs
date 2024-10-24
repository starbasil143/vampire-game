using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HarmfulObjectScript>())
        {
            if (collision.gameObject.GetComponent<HarmfulObjectScript>().thwartedByWalls)
            {
                collision.gameObject.GetComponent<HarmfulObjectScript>().DestroySelf();
            }
        }
    }
}
