using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : ControladorChaves
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    [SerializeField] private Chave.TipoChaves tipoChaves;

    public Chave.TipoChaves PegaTipoChave()
    {
        return tipoChaves;
    }

    public void AbrirPorta()
    {
        //Debug.Log("Abriu a Porta: " + tipoChaves);
        //anim.SetBool("AbrirPorta", true);
        anim.enabled = true;
        anim.SetBool("AbrirPorta", true);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<MovimentoPJ>().key)
            {
                if (Input.GetKeyDown(KeyCode.E))
            {
                    anim.enabled = true;
                    anim.SetBool("AbrirPorta", true);
                    other.GetComponent<MovimentoPJ>().key = false;
                }
            }
        }
    }
}
