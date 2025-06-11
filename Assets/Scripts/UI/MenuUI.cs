using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class MenuUI : MonoBehaviour, IUIHandler
{
    public UIState State => UIState.Menu;

    private PlayerController controller;
    private PlayerCondition condition;

    void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;
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
        UIManager.instance.ChangeUI(UIState.Menu);
    }

    public void OnClickExitButton()
    {
        UIManager.instance.ChangeUI(UIState.InGame);
    }
}
