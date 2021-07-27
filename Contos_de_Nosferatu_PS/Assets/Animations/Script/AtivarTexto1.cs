using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AtivarTexto1 : MonoBehaviour
{
    public TextMeshProUGUI texto;
    private bool iniciarTimer = false;

    private float tempo;

    private void Start()
    {
        texto.enabled = false;
    }

    private void Update()
    {
        if (iniciarTimer)
        {
            tempo += Time.deltaTime;
        }

        if (tempo >= 5f)
        {
            texto.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            texto.enabled = true;
            iniciarTimer= true;
        }
    }
}
