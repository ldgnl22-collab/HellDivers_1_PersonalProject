using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _targetPos;

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 camPosOffset = new Vector3(0f, 17.5f, -7.45f);
        Vector3 camRotOffset = new Vector3(70f, 0f, 0f);
        
        transform.rotation = Quaternion.Euler(camRotOffset);
        transform.position = Vector3.Lerp(transform.position, (_targetPos.position + camPosOffset), 5f * Time.deltaTime);
    }
}
