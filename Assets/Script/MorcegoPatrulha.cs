using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MorcegoPatrulha : MonoBehaviour
{
    public static MorcegoPatrulha instancia;

    //LISTA DOS PONTOS DE PATRULHA
    [SerializeField] List<Destinos> destinos;

    //DETERMINA EM QUE NODE O AGENTE ESPERA
    [SerializeField] bool patrulhaEspera;

    //DETERMINA SE O JOGADOR ESTÁ DENTRO DA ÁREA DE CONTATO DO MORCEGO
    [SerializeField] LayerMask lmJogador;
    public float raio = 10;
    [SerializeField] LayerMask lmVela;

    [SerializeField] public bool pegouJogador;
    [SerializeField] public bool perseguirJogador = false;
    [SerializeField] public bool temVela = false;

    //TEMPO DE ESPERAR EM CADA NODE
    [SerializeField] float tempoEsperaTotal = 3f;

    //PROBABILIDADE DE MUDAR DE DIREÇÃO
    [SerializeField] float probTroca = 0.2f;

    //VARIAVEIS - COMPORTAMENTO
    NavMeshAgent navMeshAgent;
    int patrulhaIndentificaAtual;
    bool viajando;
    bool esperando;
    bool patrulhaFrente;
    float tempoEspera;

    //VARIAVEIS PERSEGUIR
    private Transform jogador;
    private MovimentoPJ jog;
    public GameObject morcego;

    //VARIAVEIS STRUGGLE
    float lutarPelaVida;
    bool struggleBK;

    //Animator anim;
    private Animator anim;
    private BoxCollider colisor;

    //FIM DE JOGO
    public Image telaFim;


    bool ok;
    Vector3 reset;

    void Start()
    {

        reset = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        anim = GameObject.Find("JOGADOR").GetComponent<Animator>();
        colisor = this.GetComponent<BoxCollider>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        jogador = GameObject.Find("JOGADOR").GetComponent<Transform>();
        jog = GameObject.Find("JOGADOR").GetComponent<MovimentoPJ>();

        if (navMeshAgent == null)
        {
            Debug.LogError("Não tem NavMesh " + gameObject.name);
        }
        else if (destinos != null && destinos.Count >= 2)
        {
            patrulhaIndentificaAtual = 0;
            DefineDestino();
        }
        //else Debug.Log("Sem pontos de patrulha suficiente");

        perseguirJogador = false;
    }

    void Update()
    {

        perseguirJogador = Physics.CheckSphere(this.transform.position, raio, lmJogador);
        temVela = Physics.CheckSphere(this.transform.position, raio + 1, lmVela);

        //VERIFICA SE ESTA PERTO DO DESTINO
        if (viajando && navMeshAgent.remainingDistance <= 1f)
        {
            viajando = false;

            //SE VAMOS ESPERAR, ENTÃO ESPERA
            if (patrulhaEspera)
            {
                esperando = true;
                tempoEspera = 0f;
            }
            else
            {
                TrocaPontoPatrulha();
                DefineDestino();
            }
        }

        //SE ESTIVERMOS ESPERANDO  
        if (esperando)
        {
            tempoEspera += Time.deltaTime;

            if (tempoEspera >= tempoEsperaTotal)
            {
                esperando = false;

                TrocaPontoPatrulha();
                DefineDestino();
            }
        }

        //PERSEGUIR O JOGADOR
        if (perseguirJogador)
        {
            navMeshAgent.SetDestination(jogador.transform.position);
        }
        else
        {
            DefineDestino();
        }

        //SE TIVER VELA
        if (temVela)
        {
            navMeshAgent.SetDestination(jogador.transform.position * 2 + transform.position);
            TrocaPontoPatrulha();
        }

        //"ANIMAÇÃO" PEGAR JOGADOR
        PegouJogador();

        //LUTAR PELA VIDA
        if (pegouJogador)
        {
            LutarPelaVida();
        }
    }

    void TrocaPontoPatrulha()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= probTroca)
        {
            patrulhaFrente = !patrulhaFrente;
        }

        if (patrulhaFrente)
        {
            patrulhaIndentificaAtual = (patrulhaIndentificaAtual + 1) % destinos.Count;
        }

        else if (--patrulhaIndentificaAtual < 0)
        {
            patrulhaIndentificaAtual = destinos.Count - 1;
        }
    }

    void DefineDestino()
    {
        patrulhaEspera = false;

        if (destinos != null)
        {
            Vector3 alvoVector = destinos[patrulhaIndentificaAtual].transform.position;
            navMeshAgent.SetDestination(alvoVector);
            viajando = true;
        }
    }

    void PegouJogador()
    {
      //  print (pegouJogador);
        if (pegouJogador)
        {
            morcego.transform.position = Vector3.Lerp((morcego.transform.position), (new Vector3(morcego.transform.position.x, 3.25f, morcego.transform.position.z)), 1.5f * Time.deltaTime);
            anim.SetBool("EstaStruggle", true);
            anim.SetBool("EstaAndando", false);
            anim.SetBool("EstaCorrendo", false);
        }

        else
        {
            morcego.transform.position = Vector3.Lerp((morcego.transform.position), (new Vector3(morcego.transform.position.x, 5f, morcego.transform.position.z)), 1.5f * Time.deltaTime);
            anim.SetBool("EstaStruggle", false);
        }

    }

    void LutarPelaVida()
    {
        if (pegouJogador && lutarPelaVida < 20)
        {
            Invoke("EndGame", 5f);
        }
        else
        {
            CancelInvoke("EndGame");
        }


        if (pegouJogador && Input.GetKeyDown(KeyCode.A))
        {
            struggleBK = true;
        }

        if (struggleBK && Input.GetKeyDown(KeyCode.D))
        {
            lutarPelaVida++;
            struggleBK = false;
        }

        if (lutarPelaVida >= 10f)
        {
            CancelInvoke("EndGame");
            pegouJogador = false;
            DesativarCollider();
            Invoke("AtivarCollider", 5f);
            lutarPelaVida = 0f;
        }
    }


    public void EndGame()
    {
        StartCoroutine(jog.Morte());
        jog.GetComponent<MovimentoPJ>().ok = true;
        transform.position = reset;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            pegouJogador = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            pegouJogador = false;
            CancelInvoke("EndGame");
    }

    void DesativarCollider()
    {
        colisor.enabled = false;
    }
    void AtivarCollider()
    {
        colisor.enabled = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(this.transform.position, raio);
        Gizmos.DrawWireSphere(this.transform.position, raio + 1);
    }
}