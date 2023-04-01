using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TochaControl : MonoBehaviour
{
    //protected float velocY;
    //protected Vector3 impactoAtual;
    //[SerializeField] public float velocMove = 6f;
    //[SerializeField] protected float forcaPulo = 15f;
    //[SerializeField] protected float massa = 1f;
    //[SerializeField] protected float amortecer = 5f;
    //[SerializeField] protected float gravidade = -18;
    //[SerializeField] private bool podePegar;
    //public bool move, tocha, empurrar;
    //Vector3 velocidade;
    public GameObject chama;
    [SerializeField]private GameObject vela;

    private void Start()
    {

    }
    private void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        // if (chama == null) chama = GameObject.Find("/tocha/Chama");

        if (other.gameObject.CompareTag("Player"))
        {


            if (Input.GetKeyDown(KeyCode.E))
            {
               // vela = GameObject.Find("/JOGADOR/Vela"); // aqui pega a vela do jogador
                if (chama.activeSelf) // se a chama da tocha estiver ativa
                {
                    vela.SetActive(true); // ele acende a vela
                }
                else if (!chama.activeSelf && vela.activeSelf) // senao se a chama tivere apagada e a vela acessa ele acende a tocha
                {
                    chama.SetActive(true);
                }
            }
            
        }

    }


    private void OnTriggerExit(Collider other)
    {
       
    }
}

