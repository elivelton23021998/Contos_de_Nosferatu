using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;
using Jogador;



[RequireComponent(typeof(CharacterController))]
public class MovimentoPJ : MonoBehaviour
{
    //VARIAVEIS
    [SerializeField] private CharacterController controlador;
    [SerializeField] private Animator anim;
    private Vector3 jogadorVelocity;
    public bool empurrando;
    public Camera cam;

    //VARIAVEIS JOGADOR
    [SerializeField] private bool estaChao;
    public float velocidade = 0f;
    private float velocAnda = 4.0f;
    private float velocAbaixa = 2.0f;
    private float velocCorre = 7.0f;
    private float alturaPulo = 2.0f;
    private float gravidade = -18.81f;

    //VARIAVEIS ROTA��O
    protected float suavizarTempoGiro = 0.1f;
    protected float suavizarVelocGiro;

    //VARIAVEIS CHECA CH�O / PULO
    public Transform centro;
    private float raio = 0.2f;
    public LayerMask lmChao;

    //VARIAVEIS ABAIXAR
    public Transform centroT;
    private Transform tr;
    public LayerMask lmTeto;
    private float altura;
    public bool abaixado;
    public bool temTeto;

    //MORTE
    bool movendo = true, morto;
    //public bool morteBoss;
    public Image telaMorte, telaFim;
    [HideInInspector] public Vector3 respawnPos;
    [HideInInspector] public Quaternion respawnRot;

    //TRAVAR MOVIMENTO
    MorcegoPatrulha mp, mp1;

    GameObject[] cameras;

     public bool ok, morrendo, resetInimigo, key;
    public Image keyIcon;
    public string faseAtual;


    private void Start()
    {
        cameras = GameObject.FindGameObjectsWithTag("Cameras");
        
            //PlayerPrefs.GetFloat("posicaoX", respawnPos.x);
            //PlayerPrefs.GetFloat("posicaoY", respawnPos.y);
            //PlayerPrefs.GetFloat("posicaoZ", respawnPos.z);
            //PlayerPrefs.GetFloat("rotacaoX", respawnRot.x);
            //PlayerPrefs.GetFloat("rotacaoY", respawnRot.y);
            //PlayerPrefs.GetFloat("rotacaoZ", respawnRot.z);
            //PlayerPrefs.GetFloat("rotacaoW", respawnRot.w);
        

        for (int i = 0; i < cameras.Length; i++)
        {


            if (i != PlayerPrefs.GetInt("CameraAtual")) cameras[i].SetActive(false);

            else if (i == PlayerPrefs.GetInt("CameraAtual")) cameras[i].SetActive(true);
        }


        controlador = this.gameObject.GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
        mp = GameObject.Find("Morcego_Guia").GetComponent<MorcegoPatrulha>();
        mp1 = GameObject.Find("Morcego_Guia1").GetComponent<MorcegoPatrulha>();
        keyIcon = GameObject.Find("Key_Ico").GetComponent<Image>();

        tr = transform;
        altura = controlador.height;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        if (cam == null)
        {
            cam = Camera.main;
        }
       
        if (PlayerPrefs.GetString("faseAtual") != null)
        {
            PlayerPrefs.SetString("faseAtual", faseAtual);
        }
        if (PlayerPrefs.GetString("faseAtual") != faseAtual) SceneManager.LoadScene(PlayerPrefs.GetString("faseAtual"));



        if (PlayerPrefs.GetInt("primeiraVez") == 1)
        { 

        transform.position = new Vector3(PlayerPrefs.GetFloat("posicaoX"), PlayerPrefs.GetFloat("posicaoY"), PlayerPrefs.GetFloat("posicaoZ"));
        transform.rotation = new Quaternion(PlayerPrefs.GetFloat("rotacaoX"), PlayerPrefs.GetFloat("rotacaoY"), PlayerPrefs.GetFloat("rotacaoZ"), PlayerPrefs.GetFloat("rotacaoW"));
        }
    }

    void Update()
    {
        if (key) keyIcon.enabled = true;
        else if (!key) keyIcon.enabled = false;


        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("J2");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("Boss");
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerPrefs.DeleteAll();
        }

        if (morto) return;
        if (movendo && !mp.pegouJogador && !mp1.pegouJogador) Mover();
    }

    public void Mover()
    {
        float a = altura;

        //MOVIMENTO
        estaChao = Physics.CheckSphere(centro.position, raio, lmChao);
        temTeto = Physics.CheckSphere(centroT.position, raio, lmTeto);
        //////////////////////////temObj = Physics.CheckSphere(frente.position, raio, lmObj);

        //CAMERA POS
        Vector3 direcaoFrente = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z);
        Vector3 direcaoLado = new Vector3(cam.transform.right.x, 0, cam.transform.right.z);

        direcaoFrente.Normalize();
        direcaoLado.Normalize();

        direcaoFrente = direcaoFrente * Input.GetAxis("Vertical");
        direcaoLado = direcaoLado * Input.GetAxis("Horizontal");

        if (estaChao && jogadorVelocity.y < 0)
        {
            jogadorVelocity.y = -2f;
        }

        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        Vector3 move = direcaoFrente + direcaoLado;
        if (move.sqrMagnitude > 1)
        {
            move.Normalize();
        }
        ///////////

        //ROTA��O
        if (move.magnitude >= 0.1f)
        {
            velocidade = velocAnda;

            //GERA UM FLOAT (anguloAlvo) QUE � IGUAL AO ANGULO EM RAIOS DA TANGENTE DE (Y (-vertical) E X (horizontal), nesse caso X � representado pelo eixo Y e Z � representado pelo eixo X), * Mathf.Rad2Deg CONVERS�O DE RAIO PARA GRAU.
            float anguloAlvo = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
            //GERA UM FLOAT (angulo) QUE � IGUAL A ALTERA��O GRADUAL (Mathf.SmoothDampAngle) ENTRE A ROTA��O DE ANGULOS DE EULER PARA GRAUS NO EIXO Y, anguloAlvo, (ref REFERENCIA) VELOCIDADE ATUAL DO GIRO (NULA) E O TEMPO QUE QUEREMOS SUAVIZAR
            float angulo = Mathf.SmoothDampAngle(transform.eulerAngles.y, anguloAlvo, ref suavizarVelocGiro, suavizarTempoGiro);

            transform.rotation = Quaternion.Euler(0f, angulo, 0f);
        }
        else
        {
            velocidade = 0f;
        }
        ///////////

        //CORRER
        if (estaChao && Input.GetKey(KeyCode.LeftShift) && move != Vector3.zero)
        {
            anim.SetBool("EstaCorrendo", true);
            velocidade = velocCorre;
        }
        else
        {
            anim.SetBool("EstaCorrendo", false);
        }
        ///////////

        //ABAIXAR
        //if (Input.GetKey(KeyCode.LeftControl))
        //{
        //    abaixado = true;
        //}
        //else if (!temTeto)
        //{
        //    abaixado = false;
        //}

        //if (abaixado)
        //{
        //    a = 0.5f * altura;
        //    velocidade = velocAbaixa;
        //    anim.SetBool("EstaAbaixado", true);
        //}
        //else
        //{
        //    anim.SetBool("EstaAbaixado", false);
        //}

        //if (abaixado && move != Vector3.zero)
        //{
        //    anim.SetBool("EstaAndandoAbaixado", true);
        //}
        //else anim.SetBool("EstaAndandoAbaixado", false);

        /*
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Vector3 fPosicao = filho.position;
            fPosicao.y = filho.position.y + 0.5f;
            filho.position = fPosicao;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && !temTeto)
        {
            Vector3 fPosicao = filho.position;
            fPosicao.y = filho.position.y - 0.5f;
            filho.position = fPosicao;
        }
        */

        float ultimaAltura = controlador.height;
        controlador.height = Mathf.Lerp(controlador.height, a, 5);
        Vector3 tmpPosicao = tr.position;
        tmpPosicao.y += (controlador.height - ultimaAltura) / 2;
        tr.position = tmpPosicao;
        ///////////

        //CONTINUA��O MOVIMENTO
        controlador.Move(move * Time.deltaTime * velocidade);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;

        }

        if (velocidade == velocAnda)
            anim.SetBool("EstaAndando", true);

        else anim.SetBool("EstaAndando", false);
        ///////////

        //PULO
        if (Input.GetButtonDown("Jump") && estaChao && !abaixado)
        {
            jogadorVelocity.y += Mathf.Sqrt(alturaPulo * -3.0f * gravidade);
            anim.SetBool("Pulo", true);
        }
        else anim.SetBool("Pulo", false);

        /*if (Input.GetButtonDown("Jump") && estaChao && !abaixado && velocidade == velocCorre)
        {
            jogadorVelocity.y += Mathf.Sqrt(alturaPulo * -3.0f * gravidade);
            anim.SetBool("PuloCorre", true);
        }
        else anim.SetBool("PuloCorre", false);
        */

        jogadorVelocity.y += gravidade * Time.deltaTime;
        controlador.Move(jogadorVelocity * Time.deltaTime);
    }
    public IEnumerator Morte()
    {

        Debug.Log("Morreu");
        yield return null;
        
        
        if (!morrendo)
        {
            morrendo = true;
            telaMorte.gameObject.SetActive(true);
            GetComponent<CharacterController>().enabled = false;
            movendo = false;
            //GetComponent<Visao>().enabled = false;


            yield return new WaitForSeconds(1);

            Color cor = telaMorte.color;
            cor.a = 0;
            while (cor.a < 0.9f)
            {
                cor.a += Time.deltaTime;
                telaMorte.color = cor;
                yield return null;
            }
            telaMorte.color = cor;

            if (ok) SceneManager.LoadScene(faseAtual);

            transform.position = respawnPos;
            transform.rotation = respawnRot;


            yield return new WaitForSeconds(1);



            cor.a = 1;
            while (cor.a > 0)
            {
                cor.a -= Time.deltaTime;
                telaMorte.color = cor;
                yield return null;
            }
            telaMorte.color = cor;

            anim.SetBool("EstaStruggle", false);
            GetComponent<CharacterController>().enabled = true;
            movendo = true;
            morto = false;
            telaMorte.gameObject.SetActive(false);
            
            morrendo = false;
        }
        

    }

    IEnumerator Fim()
    {
        telaFim.gameObject.SetActive(true);
        GetComponent<CharacterController>().enabled = false;
        movendo = false;
        //GetComponent<Visao>().enabled = false;


        yield return new WaitForSeconds(1);

        Color cor = telaFim.color;
        cor.a = 0;
        while (cor.a < 0.9f)
        {
            cor.a += Time.deltaTime;
            telaFim.color = cor;
            yield return null;
        }
        telaFim.color = cor;


        yield return new WaitForSeconds(3);



        SceneManager.LoadScene("Menu");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Inimigo"))
        {
            StartCoroutine(Morte());
            other.GetComponent<Inimigo_Controlador>().resetarPosicao = true;
        }

        if (other.CompareTag("End"))
        {
            StartCoroutine(Fim());
        }

        if (other.CompareTag("Next"))
        {
            SceneManager.LoadScene("Boss");
        }

        if (other.CompareTag("Save"))
        {

            //camera
            for (int i = 0; i < cameras.Length; i++)
            {


                if (cameras[i].activeSelf)
                {
                    PlayerPrefs.SetInt("CameraAtual", i);
                }
            }
            //MORTE
            respawnPos = transform.position;
            respawnRot = transform.rotation;
            //SALVA PRO CONTINUE = pra deletar td =  PlayerPrefs.DeleteAll();
            PlayerPrefs.SetFloat("posicaoX", respawnPos.x);
            PlayerPrefs.SetFloat("posicaoY", respawnPos.y);
            PlayerPrefs.SetFloat("posicaoZ", respawnPos.z);
            PlayerPrefs.SetFloat("rotacaoX", respawnRot.x);
            PlayerPrefs.SetFloat("rotacaoY", respawnRot.y);
            PlayerPrefs.SetFloat("rotacaoZ", respawnRot.z);
            PlayerPrefs.SetFloat("rotacaoW", respawnRot.w);
            PlayerPrefs.SetString("faseAtual", faseAtual);
            Destroy(other.gameObject);
            PlayerPrefs.SetInt("primeiraVez", 1);
        }
    }
}
