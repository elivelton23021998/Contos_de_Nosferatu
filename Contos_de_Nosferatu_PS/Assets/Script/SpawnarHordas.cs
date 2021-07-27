using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnarHordas : MonoBehaviour
{
    public Transform pregabInimigo;
    public Transform pontoSpawn;

    public float tempoEntreHordas = 5f;
    private float contagemRegressiva = 2f;


    private int hordaIndice = 0;
    private int i;

    private void Awake()
    {
        this.enabled = false;    
    }

    void Update()
    {
        if (contagemRegressiva <= 0f)
        {
            StartCoroutine(SpawnarHorda());

            contagemRegressiva = tempoEntreHordas;
        }

        contagemRegressiva -= Time.deltaTime;
    }

    IEnumerator SpawnarHorda()
    {
        hordaIndice++;

        for (i = 0; i < hordaIndice; i++)
        {
            SpawnarInimigo();
            yield return new WaitForSeconds(0.5f);
        }

        if (hordaIndice > 5)
        {
            hordaIndice = 5;
            i = 1;
        }
    }

    void SpawnarInimigo()
    {
        Instantiate(pregabInimigo, pontoSpawn.position, pontoSpawn.rotation);
    }
}
