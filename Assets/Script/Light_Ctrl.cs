using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Ctrl : MonoBehaviour
{
	public GameObject trovao;
	public GameObject barulho;
	public float t=1;
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		t -= Time.deltaTime;
		if (t<=0)
        {
			StartCoroutine(Luz());
			t = Random.Range (10, 30);
        }
    }
	IEnumerator Luz()
	{
		trovao.SetActive (true);// aqui ativa a luz
		barulho.SetActive(true); // e faz barulho


		yield return new WaitForSeconds(Random.Range(0.3f, 0.5f));

		trovao.SetActive(false);// aqui ativa a luz


		yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));

		trovao.SetActive(true);// aqui ativa a luz


		yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));

		trovao.SetActive(false);// aqui ativa a luz


		yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));

		trovao.SetActive(true);// aqui ativa a luz


		yield return new WaitForSeconds(Random.Range(0.1f, 0.4f));

		trovao.SetActive(false);// aqui ativa a luz
		yield return new WaitForSeconds(4f);

		barulho.SetActive(false);



	}
}
