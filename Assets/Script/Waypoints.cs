using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] pontos;

    // Start is called before the first frame update
    void Awake()
    {
        pontos = new Transform[transform.childCount];
        for (int i = 0; i < pontos.Length; i++)
        {
            pontos[i] = transform.GetChild(i);
        }
    }
}
