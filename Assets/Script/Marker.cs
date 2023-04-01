using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour
{
    public Text texto;
    public string objetivo;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(NextObjetivo());
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(NextObjetivo());
        }
    }
    IEnumerator NextObjetivo()
    {
     
        texto.text = objetivo;

        yield return new WaitForSeconds(6);
        
        texto.text = "";

        Destroy(gameObject);
    }
}
