using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SetOutline : MonoBehaviour
{
    private Material outlineMaterial;
    [SerializeField] private float outlineScaleFactor;
    private Color outlineColor;
    private Renderer outlineRenderer;

    void Start()
    {
        outlineMaterial = Resources.Load<Material>("outlineMat");
        outlineRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
    }

    Renderer CreateOutline(Material outlineMat, float scaleFactor, Color color)
    {

        GameObject outlineObject = Instantiate(this.gameObject, transform.position, transform.rotation, transform);
        outlineObject.transform.localScale = new Vector3(1, 1, 1);
        Renderer rend = outlineObject.GetComponent<Renderer>();
        rend.material = outlineMat;
        rend.material.SetColor("_OutlineColor", color);
        rend.material.SetFloat("_ScaleFactor", scaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        outlineObject.GetComponent<SetOutline>().enabled = false;
        if (outlineObject.GetComponent<Collider>())
        {
            outlineObject.GetComponent<Collider>().enabled = false;
        }
        rend.enabled = false;

        return rend;
    }
    private void Update()
    {
        outlineRenderer.enabled = true;
    }

    //private void OnMouseEnter()
    //{
    //    outlineRenderer.enabled = true;
    //}

    //private void OnMouseOver()
    //{
    //    transform.Rotate(Vector3.up, 1f, Space.World);
    //}

    //private void OnMouseExit()
    //{
    //    outlineRenderer.enabled = false;
    //}
}