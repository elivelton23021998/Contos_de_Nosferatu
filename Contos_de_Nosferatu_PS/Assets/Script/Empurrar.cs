using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Empurrar : MonoBehaviour
{
    protected float velocY;
    protected Vector3 impactoAtual;
    [SerializeField] public float velocMove;
    [SerializeField] protected float amortecer = 5f;
    [SerializeField] protected float gravidade = -18;
    public bool empurra, zescada;
    Vector3 velocidade;
    public PlayableDirector subirEscada;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        subirEscada = GetComponent<PlayableDirector>();


        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (empurra)
        {
            //CAMERA POS
            Vector3 direcaoFrente = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z);
            Vector3 direcaoLado = new Vector3(cam.transform.right.x, 0, cam.transform.right.z);

            direcaoFrente.Normalize();
            direcaoLado.Normalize();

            direcaoFrente = direcaoFrente * Input.GetAxis("Vertical");
            direcaoLado = direcaoLado * Input.GetAxis("Horizontal");

            Vector3 direcao = direcaoFrente + direcaoLado;

            if (direcao.sqrMagnitude > 1)
            {
                direcao.Normalize();
            }
            ///////////

            //APLICANDO GRAVIDADO
            velocY += gravidade * Time.deltaTime;

            //DEFININDO MOVIMENTO (EIXO X, Y E Z)
            velocidade = direcao * velocMove + Vector3.up * velocY;

            //ADICIONANDO IMPACT
            if (impactoAtual.magnitude > 0.2f)
            {
                velocidade += impactoAtual;
            }

            //APLICANDO MOVER (DEF_)
            GetComponent<CharacterController>().Move(velocidade * Time.deltaTime);

            //REDUZIR IMPACTO
            impactoAtual = Vector3.Lerp(impactoAtual, Vector3.zero, amortecer * Time.deltaTime);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // if (chama == null) chama = GameObject.Find("/tocha/Chama");

        if (other.gameObject.CompareTag("Player"))
        {
            // empurra = true;
            if (Input.GetKey(KeyCode.Mouse0))
            {
                empurra = true;
                other.GetComponent<MovimentoPJ>().empurrando = true;
                velocMove = other.GetComponent<MovimentoPJ>().velocidade;

            }
            else
            {
                empurra = false;
                other.GetComponent<MovimentoPJ>().empurrando = false;
            }

            if (zescada)
            {

                subirEscada.enabled = true;
            }


        }
        if (other.gameObject.CompareTag("ZEscada"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                zescada = true;
                StartCoroutine(Reativar());
            }

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            empurra = false;
            other.GetComponent<MovimentoPJ>().empurrando = false;
        }

        if (other.gameObject.CompareTag("ZEscada"))
        {
            zescada = false;
        }
    }
    public IEnumerator Reativar()
    { 


        yield return new WaitForSeconds(5);
        zescada = false;

    }
}

