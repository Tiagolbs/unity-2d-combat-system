using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Misc
{
    public class TransparentDetection : MonoBehaviour
    {
        [Range(0, 1)]
        [SerializeField] private float transparencyAmount = 0.8f;
        [SerializeField] private float fadeTime = 0.4f;

        private SpriteRenderer spriteRenderer;
        private Tilemap tilemap;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            tilemap = GetComponent<Tilemap>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<PlayerController>())
            {
                StartCoroutine(spriteRenderer
                    ? FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, transparencyAmount)
                    : FadeRoutine(tilemap, fadeTime, tilemap.color.a, transparencyAmount));
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<PlayerController>())
            {
                StartCoroutine(spriteRenderer
                    ? FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, 1f)
                    : FadeRoutine(tilemap, fadeTime, tilemap.color.a, 1f));
            }
        }

        private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetTransparency)
        {
            float elapsedTime = 0f;
            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
                Color color = spriteRenderer.color;
                color = new Color(color.r, color.g, color.b,
                    newAlpha);
                spriteRenderer.color = color;
                yield return null;
            }
        }
    
        private IEnumerator FadeRoutine(Tilemap tilemap, float fadeTime, float startValue, float targetTransparency)
        {
            float elapsedTime = 0f;
            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
                Color color = tilemap.color;
                color = new Color(color.r, color.g, color.b,
                    newAlpha);
                tilemap.color = color;
                yield return null;
            }
        }
    }
}
