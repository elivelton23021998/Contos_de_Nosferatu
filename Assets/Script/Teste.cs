using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour
{
    public GameObject entrulho;
    public GameObject nosf;

    private void Start()
    {
        entrulho.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            entrulho.SetActive(true);
        }

       if (other.CompareTag("Nosferatu"))
        {
            Invoke ("DestroirNosf", 3f);
        }
    }

    void DestroirNosf()
    {
        Destroy(nosf);
    }
}
