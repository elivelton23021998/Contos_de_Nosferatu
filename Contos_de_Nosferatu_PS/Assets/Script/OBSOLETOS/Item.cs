using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private GameObject vela, velaIcon, chaveIcon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (vela.activeSelf) velaIcon.SetActive(true);
        else velaIcon.SetActive(false);


    }
}
