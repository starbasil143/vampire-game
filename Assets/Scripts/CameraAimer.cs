
using UnityEngine;

public class CameraAimer : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _player;
    public float threshold;
    public float damp;

    private void Update()
    {
        Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = (_player.position + mousePos) * .5f;

        targetPos.x = Mathf.Clamp(targetPos.x, -threshold + _player.position.x, threshold + _player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -threshold + _player.position.y, threshold + _player.position.y);

        gameObject.transform.position = targetPos;
    }
}
