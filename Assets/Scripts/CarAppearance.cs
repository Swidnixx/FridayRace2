using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarAppearance : MonoBehaviour
{
    public string playerName = "Player";
    public Color playerColor = Color.white;
    public TextMeshProUGUI nameText;
    public MeshRenderer carRenderer;

    private void Start()
    {
        nameText.text = playerName;
        nameText.color = playerColor;

        carRenderer.material.color = playerColor;
    }
}
