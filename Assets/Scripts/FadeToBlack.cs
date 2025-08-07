using UnityEngine;
using System.Collections;

public class SpriteFadeToBlack : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float fadeDuration = 2f;

    void Start()
    {
        StartCoroutine(FadeToBlack());
    }

    IEnumerator FadeToBlack()
    {
        Color color = spriteRenderer.color;
        float elapsed = 0f;

        // Start fully transparent
        color.a = 0f;
        spriteRenderer.color = color;

        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration); // fade from transparent to opaque
            color.a = alpha;
            spriteRenderer.color = color;
            elapsed += Time.deltaTime;
            yield return null;
        }
        

        // Ensure fully black
        color.a = 1f;
        spriteRenderer.color = color;
    }
}