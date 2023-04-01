using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class ControladorChaves : MonoBehaviour
    {
        private bool podePegarChave;
        [SerializeField] private GameObject chaveIcon;

        private List<Chave.TipoChaves> listaChaves;

        private void Awake()
        {
            listaChaves = new List<Chave.TipoChaves>();
        }

        public void AdcChave(Chave.TipoChaves tipoChave)
        {
            //Debug.Log("Pegou a Chave: " + tipoChave);
            listaChaves.Add(tipoChave);
        }

        public void RemoveChave(Chave.TipoChaves tipoChave)
        {
            //Debug.Log("Destruiu a Chave: " + tipoChave);
            listaChaves.Remove(tipoChave);
        }

        public bool TemChave(Chave.TipoChaves tipoChave)
        {
            return (listaChaves.Contains(tipoChave));
        }


        private void OnTriggerStay(Collider other)
        {
            Chave chave = other.GetComponent<Chave>();

            if (chave != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //chaveIcon.SetActive(true);
                    AdcChave(chave.PegaTipoChave());
                    Destroy(chave.gameObject);
                    GetComponent<MovimentoPJ>().key = true;
                }
            }

            Porta chavePorta = other.GetComponent<Porta>();

            if (chavePorta != null && Input.GetKeyDown(KeyCode.E))
            {
                if (TemChave(chavePorta.PegaTipoChave()))
                {
                    RemoveChave(chavePorta.PegaTipoChave());
                    chavePorta.AbrirPorta();
                   // chaveIcon.SetActive(false);
                }
            }
        }
    }