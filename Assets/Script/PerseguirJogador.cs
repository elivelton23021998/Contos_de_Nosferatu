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

    public BoxCollider block;

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
        block.enabled = true;
    }

    private void Update()
    {
        navMeshAgent.SetDestination(jogador.transform.position);
        anim.SetBool("PerseguirJogador", true);
        dist = Vector3.Distance(jogador.position, transform.position);

        if (dist <= 2.5f)
        {
            StartCoroutine(jogadorScript.Morte());
            jogadorScript.ok =true;
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
            block.enabled= false;

            lustre.isKinematic = false;
        }
    }
}
