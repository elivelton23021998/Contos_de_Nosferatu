using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brilhar : MonoBehaviour
{
    float valor;

    // Update is called once per frame
    void Update()
    {
        valor = Random.Range(0.3f, 1f);
        MeshRenderer rend = GetComponent<MeshRenderer>();
        Color cor = new Color(0, valor, 1, 1); 
        rend.material.shader = Shader.Find("HDRP/Lit");
        rend.material.SetColor("_SpecularColor", cor);
    }
}
