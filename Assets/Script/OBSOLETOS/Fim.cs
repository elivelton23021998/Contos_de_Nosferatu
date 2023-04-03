using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fim : MonoBehaviour
{

    [SerializeField] private Image telaFim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(EndGame());
        }


    }
    IEnumerator EndGame()
    {
        Color cor = telaFim.color;
        cor.a = 0;

        while (cor.a < 1f)
        {
            cor.a += Time.deltaTime;
            telaFim.color = cor;
            yield return null;
        }

        yield return new WaitForSeconds(4);
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Menu");


    }
}
