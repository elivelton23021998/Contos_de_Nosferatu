using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;

public class MenuScript : MonoBehaviour
{
    public TMP_Dropdown resolucaoDropdown;

    Resolution[] resolucoes;

    public float intensidade;
    public Volume luzes;
    private float[] orglightsintensity = { 0f, 0f, 0f, 0f };
    public Slider slider;

    void Start()
    {
        
        slider.maxValue = 1;
        intensidade = PlayerPrefs.GetFloat("Brilho");
        slider.value = intensidade; 
        

        

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        resolucoes = Screen.resolutions;
        resolucaoDropdown.ClearOptions();

        List<string> opcoes = new List<string>();

        int resolucaoAtualIndice = 0;
        for (int i = 0; i < resolucoes.Length; i++)
        {
            string opcao = resolucoes[i].width + " x " + resolucoes[i].height;
            opcoes.Add(opcao);

            if (resolucoes[i].width == Screen.currentResolution.width && resolucoes[i].height == Screen.currentResolution.height)
            {
                resolucaoAtualIndice = i;
            }
        }

        resolucaoDropdown.AddOptions(opcoes);
        resolucaoDropdown.value = resolucaoAtualIndice;
        resolucaoDropdown.RefreshShownValue();
    }

    public void Update()
    {
 
    }

    public void NovoJogo()
    {
        PlayerPrefs.DeleteAll();
        Cut();
    }

    public void Tuto()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void Cut()
    {
        SceneManager.LoadScene("Cutscene");
    }


    public void ConinueGame()
    {
        SceneManager.LoadScene("GameParte1");
    }

    public void Continuar()
    {
        Input.GetKeyDown(KeyCode.Escape);
    }

    public void Quit()
    {
        Application.Quit();
    }    

    public void DefinirQualidade(int qualidade)
    {
        QualitySettings.SetQualityLevel(qualidade);
    }

    public void TelaCheia(bool telaCheia)
    {
        Screen.fullScreen = telaCheia;
    }

    public void Brilho (float brilho)
    {    
        intensidade = brilho;
            luzes.weight = intensidade;
        PlayerPrefs.SetFloat("Brilho", intensidade);

    }
    public void Resolucao (int resolucaoIndice)
    {
        Resolution resolusao = resolucoes[resolucaoIndice];
        Screen.SetResolution(resolusao.width, resolusao.height, Screen.fullScreen);
    }

}
