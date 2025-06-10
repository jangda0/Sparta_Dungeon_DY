using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIHandler
{
    UIState State { get; } // 해당 UI가 어떤 상태를 나타내는지 (UIState enum)
    void Open();// UI가 열릴 때 호출되는 함수
    void Close();// UI가 닫힐 때 호출되는 함수
}

public enum UIState
{
    None,
    InGame,
    Main,
    Stat,
    Inventory,
    Paused

}

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; } // 싱글톤 인스턴스

    private Dictionary<UIState, IUIHandler> uiHandlers = new(); // UIState별 핸들러 저장소
    private IUIHandler currentUI; // 현재 활성화된 UI
    // currentUI의 상태를 안전하게 외부에 노출하는 프로퍼티
    public UIState currentUIState
    {
        get
        {
            return currentUI != null ? currentUI.State : UIState.None;
        }
    }
    private Stack<IUIHandler> popupStack = new();

    //자식 하위 모든 UI 컴포넌트들을 Dictionary에 저장하고, 각 UI는 닫힌 상태로 초기화, UIState.Start 화면만 열림.
    private void Awake()
    {
        // 이미 다른 인스턴스가 있으면 제거 
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        // 비활성 오브젝트 포함하여 모두 가져옴
        var handlers = GetComponentsInChildren<IUIHandler>(true);
        foreach (var handler in handlers)
        {
            uiHandlers[handler.State] = handler; // 상태별 핸들러 등록
            handler.Close(); // 시작 시 모든 UI 비활성화
        }

        ChangeUI(UIState.InGame); // 초기 상태: Start UI 열기
    }

    public void ChangeUI(UIState state)
    {
        currentUI?.Close(); // 현재 UI가 있다면 닫음
        Debug.Log("여기까지왔다");

        if (uiHandlers.TryGetValue(state, out var newUI))
        {
            currentUI = newUI;  // 현재 UI 갱신 
            newUI.Open();       // 새로운 UI 열기
        }
    }

    public void OnClickExit()  //UIManager.Instance.OnClickExit(); 게임 종료 버튼 등에서 호출 가능. 
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 실행 중지
#else
           Application.Quit();   // 빌드에서는 앱 종료
#endif
    }
}
