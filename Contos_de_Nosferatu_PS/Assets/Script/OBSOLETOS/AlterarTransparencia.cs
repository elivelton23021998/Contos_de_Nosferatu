using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlterarTransparencia : MonoBehaviour
{
    public float alphaLevel;
    public Transform jogador;

    public Image fala;
    public TextMeshProUGUI texto;

    //ESFERA
    public bool jogadorProximo;
    public Transform centro;
    private float raio = 25f;
    public LayerMask lmJogador;

    private void Start()
    {
        this.GetComponent<SkinnedMeshRenderer>().enabled = false;
        fala.enabled = false;
        texto.enabled = false;
    }

    void Update()
    {
        jogadorProximo = Physics.CheckSphere(centro.position, raio, lmJogador);

        if (jogadorProximo)
        {
            this.GetComponent<SkinnedMeshRenderer>().enabled = true;
            fala.enabled = true;
            texto.enabled = true;

            alphaLevel = Vector3.Distance(this.transform.position, jogador.transform.position)*0.255f;
            this.GetComponent<SkinnedMeshRenderer>().material.color = new Color(0, 0, 0, alphaLevel);
            fala.color = new Color(1, 1, 1, alphaLevel);
            texto.color = new Color(1, 1, 1, alphaLevel);
        }
        else
        {
            this.GetComponent<SkinnedMeshRenderer>().enabled = false;
            fala.enabled = false;
            texto.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(centro.position, raio);
    }
}
