using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chave : ControladorChaves
{
    [SerializeField] private TipoChaves tipoChave;
    public enum TipoChaves
    {
        Vermelho,
        Verde,
        Azul,
        Violeta,
    }

    public TipoChaves PegaTipoChave()
    {
        return tipoChave;
    }
}
