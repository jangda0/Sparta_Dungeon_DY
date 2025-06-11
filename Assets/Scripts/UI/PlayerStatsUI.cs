using TMPro;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour, IUIHandler
{
    public UIState State => UIState.Stat;

    private PlayerController controller;
    private PlayerCondition condition;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI PlayerLevelText;
    public TextMeshProUGUI ExpText;
    public TextMeshProUGUI jumpText;
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
        ExpText.text = $"{player.controller.exp}";
        jumpText.text = $"{player.controller.jumpPower}";
        goldText.text = $"{player.controller.gold}";
    }

    public void Open()
    {
        gameObject.SetActive(true); // 보이기
        SetCursorVisible(true);
    }

    public void Close()
    {
        Debug.Log("꺼졌다");
        gameObject.SetActive(false); // 숨기기
        SetCursorVisible(false);
    }

    private void SetCursorVisible(bool visible)
    {
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = visible;

        // 시점 회전 허용 여부 (PlayerController 연결)
        if (controller != null)
        {
            controller.canLook = !visible;
        }
    }

    public void OnClickOpenButton()
    {
        UIManager.instance.ChangeUI(UIState.Stat);
    }

    public void OnClickExitButton()
    {
        UIManager.instance.ChangeUI(UIState.InGame);
    }
}
