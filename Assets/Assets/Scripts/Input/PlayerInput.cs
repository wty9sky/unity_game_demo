using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "PlayerInput", menuName = "PlayerInput", order = 0)]
public class PlayerInput : ScriptableObject, InputActions.IGamePlayActions
{

    public event UnityAction<Vector2> onMove;
    public event UnityAction onDodge;

    public event UnityAction onAttack;

    public event UnityAction onStopMove;

    InputActions inputActions;


    public void DisableAllInput(){
        inputActions.Disable();
    }

    private void OnEnable()
    {
        // PlayerInput 继承这个接口，传入this，将Playinput注册为回调函数的接受者
        inputActions = new InputActions();
        inputActions.GamePlay.SetCallbacks(this);
    }

    public void EnableInput()
    {
        // inputActions.GamePlay.Enable();
        SwitchActionMap(inputActions.GamePlay); 
    }

    void SwitchActionMap(InputActionMap actionMap)
    {
        inputActions.Disable();
        actionMap.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        { // 持续按下
            onMove?.Invoke(context.ReadValue<Vector2>());
        }
        if(context.canceled){
            onStopMove?.Invoke();
        }
        // switch (context.phase)
        // {
        //     case InputActionPhase.Started: // 开始
        //         break;
        //     case InputActionPhase.Performed: // 持续按下
        //         break;
        //     case InputActionPhase.Canceled: // 松开
        //         break;
        //         // case InputActionPhase.Disabled: // 禁用时
        //         //     break;
        //         // case InputActionPhase.Waiting: // 等待时
        //         //     break;
        // }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        { // 按下的那一刻
            onAttack?.Invoke();
        }
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (context.started)
        { // 按下的那一刻
            onDodge?.Invoke();
        }
    }
}