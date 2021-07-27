using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaManivela : MonoBehaviour
{
    Animator anim;

    public float tempo;
    private bool iniciarTempo = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        tempo = 0f;
    }

    private void Update()
    {
        if (iniciarTempo)
        tempo += Time.deltaTime;

        if (tempo > 3f)
        {
            anim.SetBool("AbrirPortaManivela", false);
            anim.SetBool("FechandoPortaManivela", true);
        }

        if (tempo > 33f)
        {
            tempo = 0f;
            anim.SetBool("FechandoPortaManivela", false);
            iniciarTempo = false;
        }
    }

    [SerializeField] private Manivela.TipoManivela tipoManivela;

    public Manivela.TipoManivela UsarTipoManivela()
    {
        return tipoManivela;
    }

    public void AbrirPorta()
    {
        //Debug.Log("Abriu a Porta: " + tipoChaves);
        anim.SetBool("AbrirPortaManivela", true);
        iniciarTempo = true;
    }
}
