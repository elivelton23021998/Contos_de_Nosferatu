using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorManivela : MonoBehaviour
{
    private bool podeUsarManivela;

    //public PortaManivela portaManiveala;

    private List<Manivela.TipoManivela> listaManivela;

    private void Awake()
    {
        listaManivela = new List <Manivela.TipoManivela>();
    }
    public void AdcManivela(Manivela.TipoManivela tipoManivela)
    {
        //Debug.Log("Pegou a Chave: " + tipoChave);
        listaManivela.Add(tipoManivela);
    }

    public void RemoveManivela(Manivela.TipoManivela tipoManivela)
    {
        //Debug.Log("Destruiu a Chave: " + tipoChave);
        listaManivela.Remove(tipoManivela);
    }

    public bool TemManivela(Manivela.TipoManivela tipoManivela)
    {
        return (listaManivela.Contains(tipoManivela));
    }

    private void OnTriggerStay(Collider other)
    {
        Manivela manivela = other.GetComponent<Manivela>();

        if (manivela != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                AdcManivela(manivela.UsarTipoManivela());
            }
        }

        PortaManivela portaManivela = other.GetComponent<PortaManivela>();

        if (portaManivela != null && Input.GetKeyDown(KeyCode.E))
        {
            if (TemManivela(portaManivela.UsarTipoManivela()))
            {
                RemoveManivela(portaManivela.UsarTipoManivela());
                portaManivela.AbrirPorta();
            }
        }
    }
}