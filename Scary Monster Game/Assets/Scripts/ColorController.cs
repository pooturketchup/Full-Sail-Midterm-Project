using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    [SerializeField] private Material selectedMaterial;
    private Material defaultMaterial;
    private string tag;

    private void Start()
    {
        tag = this.transform.tag;
    }
    private void OnTriggerStay(Collider other)
    {
        if (this.transform.tag == tag && other.transform.tag == "Player")
        {
            var renderObject = this.GetComponent<Renderer>();
            defaultMaterial = renderObject.material;
            renderObject.material = selectedMaterial;
            this.transform.tag = "Usable";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (this.transform.tag == "Usable" && other.transform.tag == "Player")
        {
            var renderObject = this.GetComponent<Renderer>();
            renderObject.material = defaultMaterial;
            this.transform.tag = tag;
        }
    }
}
