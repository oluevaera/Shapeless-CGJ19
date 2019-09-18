using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerIndicator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    public void OnDeviceLost()
    {
        spriteRenderer.gameObject.SetActive(true);
    }

    public void OnDeviceRegained()
    {
        spriteRenderer.gameObject.SetActive(false);
    }
}
