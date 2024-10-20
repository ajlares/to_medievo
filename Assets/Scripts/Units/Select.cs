using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    private Renderer objRenderer;
    private Material originalMaterial;
    public Material highlightMaterial;

    private void Start()
    {
        objRenderer = GetComponent<Renderer>();
        originalMaterial = objRenderer.material;
    }

    public Material GetHighlightMaterial()
    {
        return highlightMaterial;  
    }

    public void ResetMaterial()
    {
        if (originalMaterial != null)
        {
            GetComponent<Renderer>().material = originalMaterial; 
        }
    }
}