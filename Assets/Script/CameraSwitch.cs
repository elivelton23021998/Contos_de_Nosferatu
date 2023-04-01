using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject camON;

    public GameObject[] cam;

    private void Start()
    {
        
    }

    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            camON.SetActive (true);
            cam = GameObject.FindGameObjectsWithTag("Cameras");
            foreach (GameObject op in cam)
            {
                if (camON != op)
                {
                    op.SetActive(false);
                }
            }
        }
    }
}
