using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cameraTransform;
    public float parallaxEffect;

    private float startPos;
    private float initialCamPos;

    void Start()
    {
        startPos = transform.position.x;
        initialCamPos = cameraTransform.position.x;
    }

    void LateUpdate()
    {
        float delta = cameraTransform.position.x - initialCamPos;

        float newX = startPos + delta * parallaxEffect;

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}