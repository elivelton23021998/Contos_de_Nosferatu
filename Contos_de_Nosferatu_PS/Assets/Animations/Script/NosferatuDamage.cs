using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NosferatuDamage : MonoBehaviour
{
    public Animator anim;
    public Transform jogador;
    public NavMeshAgent agent;
    private bool resetDano;
    private float tempo;
    public PerseguirJogadorBOSS nosf;

    public void Start()
    {
        anim = GameObject.Find("Nosferatu").GetComponent<Animator>();
        nosf = GameObject.Find("Nosferatu").GetComponent<PerseguirJogadorBOSS>();
        agent = GameObject.Find("Nosferatu").GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        if (resetDano)
        {
            tempo += Time.deltaTime;
        }

        if (tempo >= 3f)
        {
            anim.SetBool("NosferatuHit", false);
            agent.SetDestination(jogador.transform.position);
            nosf.enabled = true;
            tempo = 0f;
            resetDano = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Nosferatu"))
        {
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAH");
            anim.SetBool("NosferatuHit", true);
            resetDano = true;
            nosf.enabled = false;
            nosf.Fase2();
        }
    }
}
