 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using TMPro;
public class AtivarCutscene : MonoBehaviour
{
    PlayableDirector pd;
    //public PlayableDirector pdTempo;

    float tempo = 11f;
    bool iniciarTempo;

    public GameObject camPlayer;
    public GameObject camNosferatu;
    public GameObject Nosferatu;
    public GameObject NosferatuCutscene;
    public BoxCollider bc;

    public static bool iniciarTimer = false;

    private void Start()
    {
        pd = GetComponent<PlayableDirector>();
        bc = GetComponent<BoxCollider>();
        //tempoCountDown.enabled = false;
        pd.enabled = false;
        iniciarTempo = false;
        Nosferatu.SetActive(false);
        NosferatuCutscene.SetActive(false);
    }

    private void Update()
    {
        if (iniciarTempo)
        {
            if (tempo > 0f)
            {
                tempo -= Time.deltaTime;
            }

            if (tempo <= 0)
            {
                camPlayer.SetActive(true);
                camNosferatu.SetActive(false);
                NosferatuCutscene.SetActive(false);
                Nosferatu.SetActive(true);
                iniciarTimer = true;
                bc.enabled = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NosferatuCutscene.SetActive(true);
            pd.enabled = true;
            camPlayer.SetActive(false);
            camNosferatu.SetActive(true);
            iniciarTempo = true;
        }
    }
}
