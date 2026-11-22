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
        Vector3 camPosOffset = new Vector3(0f, 9f, -5f);
        Vector3 camRotOffset = new Vector3(60f, 0f, 0f);
        
        transform.rotation = Quaternion.Euler(camRotOffset);
        transform.position = Vector3.Lerp(transform.position, (_targetPos.position + camPosOffset), 5f * Time.deltaTime);
    }
}
