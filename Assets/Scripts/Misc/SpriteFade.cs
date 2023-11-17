using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFade : MonoBehaviour
{
    [SerializeField] private float fadeTime = 0.4f;
    
    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator SlowFadeRoutine()
    {
        float elapsedTime = 0f;
        float startValue = mySpriteRenderer.color.a;
        
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, 0f, elapsedTime / fadeTime);
            Color color = mySpriteRenderer.color;
            color = new Color(color.r, color.g, color.b,
                newAlpha);
            mySpriteRenderer.color = color;
            yield return null;
        }
        
        Destroy(gameObject);
    }
}
