using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manivela : MonoBehaviour
{
    [SerializeField] private TipoManivela tipoManivela;

    Animator anima;

    private void Start()
    {
        anima = GetComponent<Animator>();
    }
    public enum TipoManivela
    {
        Vermelha,
        Verde,
        Azul,
        Amarela,
    }

    public TipoManivela UsarTipoManivela()
    {
        anima.SetBool("GirarManivela", true);
    
        return tipoManivela;
    }
}
