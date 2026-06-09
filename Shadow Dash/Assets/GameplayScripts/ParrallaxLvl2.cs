using UnityEngine;

public class ParrallaxLvl2 : MonoBehaviour
{
    public Transform cameraTransform;
    [Range(0f, 1f)]
    public float parallaxFactor = 0.1f;

    private Vector3 lastCameraPosition;

    void Start()
    {
        lastCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        transform.position += new Vector3(
            deltaMovement.x * parallaxFactor,
            deltaMovement.y * parallaxFactor,
            0
        );

        lastCameraPosition = cameraTransform.position;
    }
}
