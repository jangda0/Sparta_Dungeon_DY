using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour, IUIHandler
{
    public UIState State => UIState.InGame;

    private PlayerController controller;
    private PlayerCondition condition;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI PlayerLevelText;
    public TextMeshProUGUI goldText;

    public Player player;

    void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;


        UpdateStatusUI();
    }

    public void UpdateStatusUI()
    {
        if (player == null) return;

        playerNameText.text = $"{player.controller.playerName}";
        PlayerLevelText.text = $"{player.controller.playerLevel}";
        goldText.text = $"{player.controller.gold}";
    }
    public void Open()
    {
        gameObject.SetActive(true); // 보이기
    }

    public void Close()
    {
        Debug.Log("꺼졌다");
        gameObject.SetActive(false); // 숨기기
    }

}
