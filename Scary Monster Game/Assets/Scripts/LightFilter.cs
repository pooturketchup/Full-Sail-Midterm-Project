using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFilter : MonoBehaviour
{

    public Light Security1;
    public Light Security2;
    public Light Security3;
    public Light Security4;

    // Start is called before the first frame update
    void OnPreCull()
    {
        if (Security1 != null)
            Security1.enabled = false;
        if (Security2 != null)
            Security2.enabled = false;
        if (Security3 != null)
            Security3.enabled = false;
        if (Security4 != null)
            Security4.enabled = false;

    }

    void OnPreRender()
    {
        if (Security1 != null)
            Security1.enabled = false;
        if (Security2 != null)
            Security2.enabled = false;
        if (Security3 != null)
            Security3.enabled = false;
        if (Security4 != null)
            Security4.enabled = false;

    }
    void OnPostRender()
    {
        if (Security1 != null)
            Security1.enabled = true;
        if (Security2 != null)
            Security2.enabled = true;
        if (Security3 != null)
            Security3.enabled = true;
        if (Security4 != null)
            Security4.enabled = true;

    }
}
