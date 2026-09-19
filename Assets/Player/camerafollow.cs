using UnityEngine;

public class camerafollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.05f;

    private Vector3 velocity = Vector3.zero;



    // Update is called once per frame
    void LateUpdate()
    {
        if(target == null)
        {
            return;
        }
        Vector3 desiredPosition = target.position +  new Vector3 (0, 0, -10);
        Transform cam = gameObject.transform;
        cam.position = Vector3.SmoothDamp(cam.position, desiredPosition, ref velocity, smoothTime);
    }
}
