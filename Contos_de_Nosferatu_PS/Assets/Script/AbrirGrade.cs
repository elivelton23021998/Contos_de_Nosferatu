using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirGrade : MonoBehaviour
{
    bool iniciarAnim;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        iniciarAnim = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (iniciarAnim)
        {
            if (Input.GetKey(KeyCode.E))
            {
                anim.SetBool("AbrirGrade", true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Player"))
        {
            iniciarAnim = true;
        }
    }
}
