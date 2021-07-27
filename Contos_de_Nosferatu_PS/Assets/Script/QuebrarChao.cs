using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuebrarChao : MonoBehaviour
{
    public GameObject[] partesChao;
    public Rigidbody[] rbs;

    private void Start()
    {
        partesChao = GameObject.FindGameObjectsWithTag("Quebrar");
        rbs = new Rigidbody[partesChao.Length];

        for (int i = 0; i < partesChao.Length; i++)
        {
            GameObject parte = partesChao[i];
            rbs[i] = parte.GetComponent<Rigidbody>();
        }

        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Quebrar"))
        {
            foreach (Rigidbody rb in rbs)
            {
                rb.isKinematic = false;
            }
        }
    }

}
