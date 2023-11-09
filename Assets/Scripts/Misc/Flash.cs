using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMaterial;
    [SerializeField] private float restoreDefaultMaterialTime = 0.2f;

    private SpriteRenderer spriteRenderer;
    private Material defaultMaterial;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
    }

    public float GetRestoreDefaultMaterialTime()
    {
        return restoreDefaultMaterialTime;
    }

    public void GetFlashEffect()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.material = whiteFlashMaterial;
        
        yield return new WaitForSeconds(restoreDefaultMaterialTime);

        spriteRenderer.material = defaultMaterial;
    }
}
