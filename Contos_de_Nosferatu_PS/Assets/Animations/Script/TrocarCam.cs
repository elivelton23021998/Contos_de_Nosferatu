using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TrocarCam : MonoBehaviour
{
    public CinemachineVirtualCamera cam1;
    public CinemachineFreeLook cam2;

    float tempo;
    bool mudarCam = false;

    private void Update()
    {
        if (mudarCam)
        {
            tempo += Time.deltaTime;

            if (tempo >= 15f)
            {
                cam1.enabled = true;
                cam2.enabled = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
         if (other.CompareTag ("Player"))
        {
            cam1.enabled = false;
            cam2.enabled = true;

            mudarCam = true;
        }
    }
}
