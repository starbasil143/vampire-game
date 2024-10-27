
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public float rotationSpeed;
    void Update()
    {
        transform.Rotate (new Vector3 (0, 0, 50) * Time.deltaTime * rotationSpeed);
    }
}
