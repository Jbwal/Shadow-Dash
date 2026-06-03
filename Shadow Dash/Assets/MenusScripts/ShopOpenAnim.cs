using UnityEngine;

public class ShopOpenAnim : MonoBehaviour
{
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        StartCoroutine(Open());
    }

    private System.Collections.IEnumerator Open()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 6f;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one,
                Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
        transform.localScale = Vector3.one;
    }
}