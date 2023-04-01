using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InimigosBOSS : MonoBehaviour
{
    public Transform player;

    private float speed = 5f;

    bool perseguirJogador;

    public LayerMask lmJogador;

    float tempo;

    public Vector3 offset;

    void Start()
    {
        player = GameObject.Find("JOGADOR").GetComponent<Transform>();

        Vector3 playerPos = new Vector3(player.position.x, player.position.y, player.position.z);

        transform.LookAt(player.position);
    }

    // Update is called once per frame
    void Update()
    {
        perseguirJogador = Physics.CheckSphere(transform.position, 3f, lmJogador);

        if (!perseguirJogador)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        if(perseguirJogador)
        {
            Vector3 direcao = player.position - transform.position;

            float distanciaFrame = speed * Time.deltaTime;

            transform.Translate((direcao.normalized + offset) * distanciaFrame, Space.World);

            tempo += Time.deltaTime;

            if (tempo >= 5f)
            {
                Destroy(gameObject);
            }
        }

        float dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist <= 1f)
        {
          //  Debug.Log("JOGADOR MORREU");
            //PLAYER MORRE 
            //REINICIA A CENA
        }

        if (dist >= 40)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 3f);
    }
}
