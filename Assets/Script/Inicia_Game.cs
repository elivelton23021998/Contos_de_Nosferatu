using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class Inicia_Game : MonoBehaviour
{
    public Image telaFim;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { StartCoroutine(Fim()); }
    }
            IEnumerator Fim()
    {
        telaFim.gameObject.SetActive(true);
       
        


        yield return new WaitForSeconds(1);

        Color cor = telaFim.color;
        cor.a = 0;
        while (cor.a < 0.9f)
        {
            cor.a += Time.deltaTime;
            telaFim.color = cor;
            yield return null;
        }
        telaFim.color = cor;


        yield return new WaitForSeconds(3);



        PlayerPrefs.SetInt("primeiraVez", 0);
        SceneManager.LoadScene("Cutscene");

    }
}
