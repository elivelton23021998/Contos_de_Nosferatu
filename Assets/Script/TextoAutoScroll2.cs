using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextoAutoScroll2 : MonoBehaviour
{
    public float scrollSpeed = 20;
    float tempo;

    public RectTransform posicao;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 localVectorUp = transform.TransformDirection(0, 1, 0);
        pos += localVectorUp * scrollSpeed * Time.deltaTime;
        transform.position = pos;


        if (posicao.position.y >= 525f)
        {
            pos = transform.position;
            localVectorUp = transform.TransformDirection(0, 0, 0);
            pos += localVectorUp;
            transform.position = pos;

            tempo += Time.deltaTime;
        }

        if (tempo >= 2f)
        {
            SceneManager.LoadScene("Menu");
        }
    }

}
