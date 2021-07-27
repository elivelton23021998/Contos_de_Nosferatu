using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AtivarCutsceneBoss : MonoBehaviour
{
    PlayableDirector pd;
    public GameObject camOn;
    public GameObject camOff;

    public PerseguirJogadorBOSS nosf;

    bool reativarCam = false;

    float tempoParaReativar = 0f;

    private void Start()
    {
        pd = GetComponent<PlayableDirector>();
        pd.enabled = false;
        nosf.enabled = false;
    }

    public void Update()
    {
        if (reativarCam)
        {
            tempoParaReativar += Time.deltaTime;

            if (tempoParaReativar >= 13f)
            {
                nosf.enabled = true;
                camOn.SetActive(false);
                camOff.SetActive(true);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pd.enabled = true;
            camOn.SetActive(true);
            camOff.SetActive(false);
            reativarCam = true;
        }
    }
}
