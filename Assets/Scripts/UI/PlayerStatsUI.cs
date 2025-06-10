using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour, IUIHandler
{
    public UIState State => UIState.Stat;

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
