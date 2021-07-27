using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cair : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("parede"))
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("parede"))
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
}