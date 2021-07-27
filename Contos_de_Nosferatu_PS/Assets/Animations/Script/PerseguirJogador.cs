using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;
using UnityEngine.SceneManagement;
using TMPro;

public class PerseguirJogador : MonoBehaviour
{
    public Transform jogador;

    public MovimentoPJ jogadorScript;

    private NavMeshAgent navMeshAgent;
    Animator anim;
    float dist;

    float tempo = 30f;
    private TextMeshProUGUI tempoCountDown;
    string tempoHolder;

    public Rigidbody lustre;

    private void Start()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        jogador = GameObject.Find("JOGADOR").GetComponent<Transform>();
        jogadorScript = GameObject.Find("JOGADOR").GetComponent<MovimentoPJ>();

        tempoCountDown = GameObject.Find("CountDown").GetComponent<TextMeshProUGUI>();

        lustre.isKinematic = true;

        if (navMeshAgent == null)
        {
            Debug.LogError("Não tem NavMesh " + gameObject.name);
        }

        tempoCountDown.enabled = false;
    }

    private void Update()
    {
        navMeshAgent.SetDestination(jogador.transform.position);
        anim.SetBool("PerseguirJogador", true);
        dist = Vector3.Distance(jogador.position, transform.position);

        if (dist <= 2.5f)
        {
            //AtivarCutscene.iniciarTimer = false;
            StartCoroutine(jogadorScript.Morte());
           // tempo = 30;
            jogadorScript.ok =true;
            //tempoCountDown.enabled = false;
        }

        if (AtivarCutscene.iniciarTimer)
        {
            tempoCountDown.enabled = true;

            tempo -= Time.deltaTime;

            tempoHolder = ((int)tempo).ToString();
            tempoCountDown.text = tempoHolder;
        }

        if (tempo <= 0)
        {
            tempoCountDown.enabled = false;

            //INICIAR ANIMA��O DE CAIR O LUSTRE E QUEBRAR O CH�O
            lustre.isKinematic = false;
        }
    }
}
