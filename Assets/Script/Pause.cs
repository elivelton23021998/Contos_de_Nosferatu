using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Jogador
{

    public class Pause : MonoBehaviour
    {
        public static bool jogoPausado = false;
        public GameObject pauseMenu;

        private void Start()
        {
            pauseMenu.SetActive(false);
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (jogoPausado)
                {
                    Retornar();
                }
                else
                {
                    Pausar();
                }
            }
        }

        public void Retornar()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            jogoPausado = false;
        }

        public void Pausar()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0.0f;
            jogoPausado = true;
        }

        public void Menu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
        }
    }
}
