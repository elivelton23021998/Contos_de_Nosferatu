using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PerseguirJogadorBOSS : MonoBehaviour
{
    public Transform jogador;
    private NavMeshAgent navMeshAgent;
    public Transform resetPos;
    Animator anim;
    float dist;

    float indicador = 0f;

    public GameObject painel;

    public MovimentoPJ jogadorScript;

    float tempo;

    public GameObject BossFightCam, DeathCam;

    private void Start()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        jogador = GameObject.Find("JOGADOR").GetComponent<Transform>();
        jogadorScript = GameObject.Find("JOGADOR").GetComponent<MovimentoPJ>();

        if (navMeshAgent == null)
        {
            Debug.LogError("N�o tem NavMesh " + gameObject.name);
        }
    }

    private void Update()
    {
        navMeshAgent.SetDestination(jogador.transform.position);
        anim.SetBool("PerseguirJogador", true);
        dist = Vector3.Distance(jogador.position, transform.position);

        if (dist <= 3f)
        {
            StartCoroutine(jogadorScript.Morte());
            jogadorScript.ok = true;
        }

        if (indicador >= 3)
        {
            navMeshAgent.speed = 5.5f;
            navMeshAgent.acceleration = 20f;
        }

        if (indicador >= 6)
        {
            
            anim.SetBool("Morte", true);
            navMeshAgent.SetDestination(this.transform.position);
            BossFightCam.SetActive(false);
            DeathCam.SetActive(true);



            tempo += Time.deltaTime;

            if (tempo >= 3f)
            {
                PlayerPrefs.DeleteAll();
                SceneManager.LoadScene("Cutscene Final");
            }
        }    
    }

    public void Fase2()
    {
        indicador += 1;

        if (indicador <= 5f)
        {
            navMeshAgent.SetDestination(resetPos.transform.position);
        }
    }
}
