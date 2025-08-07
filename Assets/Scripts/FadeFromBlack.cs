using UnityEngine;
using System.Collections;

public class SpriteFade : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float fadeDuration = 2f;

    void Start()
    {
        StartCoroutine(FadeOutBlack());
    }

    IEnumerator FadeOutBlack()
    {
        Color color = spriteRenderer.color;
        float elapsed = 0f;

        color.a = 1f;
        spriteRenderer.color = color;

        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration); 
            color.a = alpha;
            spriteRenderer.color = color;
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        color.a = 0f;
        spriteRenderer.color = color;
    }
}