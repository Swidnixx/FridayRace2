using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public TextMeshProUGUI nicknameText;
    public Renderer carRenderer;

    public Slider sliderR;
    public Slider sliderG;
    public Slider sliderB;

    public Button playButton;

    private void Start()
    {
        sliderR.value = PlayerPrefs.GetFloat("R");
        sliderG.value = PlayerPrefs.GetFloat("G");
        sliderB.value = PlayerPrefs.GetFloat("B");
        OnColorChanged(0);

        sliderR.onValueChanged.AddListener(OnColorChanged);
        sliderG.onValueChanged.AddListener(OnColorChanged);
        sliderB.onValueChanged.AddListener(OnColorChanged);

        nicknameText.text = PlayerPrefs.GetString("Nickname");
        if(nicknameText.text == "")
        {
            playButton.interactable = false;
        }
    }

    private void OnColorChanged(float value)
    {
        Color color = new Color(sliderR.value, sliderG.value, sliderB.value);
        carRenderer.material.color = color;
        nicknameText.color = color;

        PlayerPrefs.SetFloat("R", color.r);
        PlayerPrefs.SetFloat("G", color.g);
        PlayerPrefs.SetFloat("B", color.b);
    }

    public void SetNickname(string nickname)
    {
        nicknameText.text = nickname;
        PlayerPrefs.SetString("Nickname", nickname);

        if (nicknameText.text == "")
        {
            playButton.interactable = false;
        }
        else
        {
            playButton.interactable = true;
        }
    }
}
