using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelfKiller : MonoBehaviour
{
    public void KillSelf()
    {
        Destroy(gameObject);
    }
}
