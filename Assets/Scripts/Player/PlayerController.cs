using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask;
    public string playerName;
    public int playerLevel;
    public int exp;
    public int gold;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;
    public bool canLook = true;

    //public Action inventory;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()//Update에 호출해도 큰 문제는 없지만, Rigidbody와 같은 물리 작용을 하는 메서드는 FixedUpdate에서 하는 게 좋다. 
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
        CameraLook();
        }
    }

    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y; //점프를 했을때만 위 아래로 움직여야하기 때문에 넣어줌. 

        _rigidbody.velocity = dir;
    }

    void CameraLook()
    {
        //마우스를 좌우로 움직이면 mouseDelta의 x값이 움직임. 
        //캐릭터의 x값을 움직이려면, y축을 회전시켜줘야함으로, 실제로 받는 값을 x값은 y에 넣어주고, y값은 x에 넣어줘야 우리가 원하는 효과를 낼 수 있다. 
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        //camCurXRot 앞에 -를 넣어준 이유는 rotation x가 마이너스일땐 위를 보고, 플러스일땐 아래를 보기 때문에 부호를 바꿔줌. 값과 보이는 값은 반대로 작동함. 

        //mouseDelta.x 에 민감도를 곱한 후 y값에 넣어줌. 
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    { 
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        { 
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    public void OnInventoryButton(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            // 현재 UI가 인벤토리면 닫고, 아니면 열기
            if (UIManager.instance.currentUIState == UIState.Inventory)
            {
                UIManager.instance.ChangeUI(UIState.InGame); // 인벤토리 닫기
            }
            else
            {
                UIManager.instance.ChangeUI(UIState.Inventory); // 인벤토리 열기
            }
        }
    }

    public void OnStatsButton(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            // 현재 UI가 인벤토리면 닫고, 아니면 열기
            if (UIManager.instance.currentUIState == UIState.Stat)
            {
                UIManager.instance.ChangeUI(UIState.InGame); // 인벤토리 닫기
            }
            else
            {
                UIManager.instance.ChangeUI(UIState.Stat); // 인벤토리 열기
            }
        }
    }

    public void OnMenuButton(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            // 현재 UI가 인벤토리면 닫고, 아니면 열기
            if (UIManager.instance.currentUIState == UIState.Menu)
            {
                UIManager.instance.ChangeUI(UIState.InGame); // 인벤토리 닫기
            }
            else
            {
                UIManager.instance.ChangeUI(UIState.Menu); // 인벤토리 열기
            }
        }
    }
}
