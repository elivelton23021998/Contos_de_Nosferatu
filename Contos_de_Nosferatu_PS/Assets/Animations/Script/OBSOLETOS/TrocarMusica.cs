using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrocarMusica : MonoBehaviour
{
        //Adicionar novas músicas para tocar
        public AudioSource principal;
        public AudioSource chasemusic;
        //Ativar música de perseguição
        void OnTriggerEnter(Collider collider)
        {
            if (collider.tag == "Player")
            {

                chasemusic.Play();
                principal.Pause();
            }
        }
        void OnTriggerExit(Collider collider)
        {
            if (collider.tag == "Player")
            {
                chasemusic.Pause();
                principal.Play();
            }
        }
}