using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Jogador
{
    public class Inimigo_Controlador : MonoBehaviour
    {
        ///public float vida=10;

        public GameObject jogador;

        [SerializeField] private Pause pauseMenu;


        public float angulo = 45, distanceRay = 30;// rotVel=120;


        public NavMeshAgent agente;
        public Transform[] ponto;
        private bool volta;

        int pontoIndex;
        float dist;
        Vector3 ultimaPos;

        public float delay;

       
        Vector3 reset;

        // Start is called before the first frame update
        private void Awake()
        {

            jogador = GameObject.Find("JOGADOR");
            //pauseMenu = GameObject.Find("/CANVAS").GetComponent<Pause>();
        }
        void Start()
        {
            agente = GetComponent<NavMeshAgent>();
            pontoIndex = 0;
            reset = new Vector3(transform.position.x, transform.position.y, transform.position.z);
           
        }

        // Update is called once per frame
        void Update()
        {
         
            if (!Pause.jogoPausado)
            {
                agente.enabled = true;
                //print(delay);
                if (JogadorProximo())
                {
                    agente.SetDestination(jogador.transform.position);
                    ultimaPos = jogador.transform.position;
                    delay = 5;
                }
                else
                {

                    agente.SetDestination(ultimaPos);

                    //fazer aquele bagulho de deley no time delta time

                    if (delay > 0)
                    {
                        agente.SetDestination(ultimaPos);
                        delay -= Time.deltaTime;
                    }
                    else //EndGame
                    {

                        dist = Vector3.Distance(transform.position, ponto[pontoIndex].position);
                        if (dist < 1)
                        {
                            AddIndex();
                        }
                        Patrol();
                    }
                }
            }
        }

        public IEnumerator ResetPosition()
        {
            yield return new WaitForSeconds(2);
            transform.position = reset;
        }

        void Patrol()
        {
            agente.SetDestination(ponto[pontoIndex].position);
        }
        void AddIndex()
        {
            pontoIndex++;
            if (pontoIndex >= ponto.Length)
            {
                pontoIndex = 0;
            }
        }

        bool JogadorProximo()
        {

            if (Vector3.Distance(transform.position, jogador.transform.position) < distanceRay)
            {

                Vector3 alvo = jogador.transform.position - transform.position;
                if (Vector3.Angle(transform.forward, alvo) <= angulo)// se o angulo entre a visao da torreta e o caminho do plauer ser a msm
                {
                    Ray raio = new Ray(transform.position, alvo);
                    Debug.DrawRay(raio.origin, raio.direction * 10, Color.red);

                    RaycastHit hit;
                    if (Physics.Raycast(raio, out hit, distanceRay))
                    {
                        if (hit.transform == jogador.transform)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        //public virtual void OnDrawnGizmos()
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawSphere(transform.position, debugDrawRadius);
        //}
    }
}
