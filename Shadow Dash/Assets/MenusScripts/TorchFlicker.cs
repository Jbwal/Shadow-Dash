using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TorchFlicker : MonoBehaviour
{
    private Light2D light2D;
    [SerializeField] private float minIntensity = 0.6f;
    [SerializeField] private float maxIntensity = 1f;
    [SerializeField] private float speed = 3f;

    private void Awake() => light2D = GetComponent<Light2D>();

    private void Update()
    {
        light2D.intensity = Mathf.Lerp(minIntensity, maxIntensity,
            Mathf.PerlinNoise(Time.time * speed, 0f));
    }
}