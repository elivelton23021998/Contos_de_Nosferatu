using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cortina : MonoBehaviour
{
    public GameObject roupaMexendo, roupaCaindo, triggerDanoNosferaru;
    bool ativar;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ativar)
        {
            roupaCaindo.SetActive(true);
            triggerDanoNosferaru.SetActive(true);   
            roupaMexendo.SetActive(false);
        }
        //if (transform.localPosition.z >= n)
        //{
        //    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);

        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                ativar = true;
            }
        }
      
    }
}
