using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputChecker : MonoBehaviour
{
    private PlayerInputActions actions;

    private StringBuilder sb = new StringBuilder(256);
    private string lastControl;
    private double lastTime;

    public bool isLog = false;

    private void OnEnable()
    {
        actions = new PlayerInputActions();
        actions.Enable();

        // 주요 액션에 콜백 연결 (per-frame polling 외에 이벤트로도 확인)
        actions.PlayerActions.Jump.performed += OnActionPerformed;
        actions.PlayerActions.NormalAttack.performed += OnActionPerformed;
        actions.PlayerActions.StrongAttack.performed += OnActionPerformed;
        actions.PlayerActions.Move.performed += OnActionPerformed;
        actions.PlayerActions.Move.canceled += OnActionPerformed;
        actions.PlayerActions.Interact.performed += OnActionPerformed;

        // 디바이스 연결/해제 로그
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDisable()
    {
        if (actions != null)
        {
            actions.PlayerActions.Jump.performed -= OnActionPerformed;
            actions.PlayerActions.NormalAttack.performed -= OnActionPerformed;
            actions.PlayerActions.StrongAttack.performed -= OnActionPerformed;
            actions.PlayerActions.Move.performed -= OnActionPerformed;
            actions.PlayerActions.Move.canceled -= OnActionPerformed;
            actions.PlayerActions.Interact.performed -= OnActionPerformed;
            actions.Disable();
        }

        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void OnActionPerformed(InputAction.CallbackContext ctx)
    {
        lastControl = ctx.control?.displayName ?? ctx.control?.path ?? "(unknown)";
        lastTime = Time.realtimeSinceStartupAsDouble;

        if (isLog)
            Debug.Log($"[Input] {ctx.action.name} => {lastControl}, value={ctx.ReadValueAsObject()}");
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (isLog)
            Debug.Log($"[Input] Device {change}: {device.displayName} ({device.layout})");
    }

    void OnGUI()
    {
        // 간단한 화면 출력 (에디터/빌드 공통). UI Text를 쓰고 싶으면 TMP로 바꿔도 됩니다.
        var move = actions.PlayerActions.Move.ReadValue<Vector2>();
        bool jumpThisFrame = actions.PlayerActions.Jump.WasPerformedThisFrame();
        bool normalAttackThisFrame = actions.PlayerActions.NormalAttack.WasPerformedThisFrame();
        bool strongAttackThisFrame = actions.PlayerActions.StrongAttack.WasPerformedThisFrame();

        sb.Clear();
        sb.AppendLine("<b>Input Health Check</b>");
        sb.AppendLine($"Devices: {GetDeviceSummary()}");
        sb.AppendLine($"Move: {move}");
        sb.AppendLine($"Jump(performed this frame): {jumpThisFrame}");
        sb.AppendLine($"Attack(performed this frame): {normalAttackThisFrame}");
        sb.AppendLine($"Attack(performed this frame): {strongAttackThisFrame}");

        if (!string.IsNullOrEmpty(lastControl))
        {
            sb.AppendLine($"Last Control: {lastControl}  ({(Time.realtimeSinceStartupAsDouble - lastTime):0.00}s ago)");
        }

        // 키보드/패드 저수준 확인(보조 진단)
        if (Keyboard.current != null)
        {
            sb.Append("Keyboard: ");
            if (Keyboard.current.anyKey.isPressed) sb.Append("anyKey ");
            if (Keyboard.current.aKey.isPressed) sb.Append("A ");
            if (Keyboard.current.dKey.isPressed) sb.Append("D ");
            if (Keyboard.current.spaceKey.wasPressedThisFrame) sb.Append("Space! ");
            sb.AppendLine();
        }
        if (Gamepad.current != null)
        {
            var gp = Gamepad.current;
            sb.Append("Gamepad: ");
            if (gp.buttonSouth.wasPressedThisFrame) sb.Append("South(A/Cross)! ");
            if (gp.leftStick.IsActuated()) sb.Append($"LS:{gp.leftStick.ReadValue()} ");
            if (gp.dpad.IsActuated()) sb.Append($"DPad:{gp.dpad.ReadValue()} ");
            sb.AppendLine();
        }

        var style = new GUIStyle(GUI.skin.box) { richText = true, alignment = TextAnchor.UpperLeft };
        GUILayout.BeginArea(new Rect(10, 10, 460, 220));
        GUILayout.Box(sb.ToString(), style, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
        GUILayout.EndArea();
    }

    private string GetDeviceSummary()
    {
        string k = (Keyboard.current != null) ? "Keyboard o" : "Keyboard x";
        string g = (Gamepad.current != null) ? $"Gamepad o ({Gamepad.current.displayName})" : "Gamepad x";
        string m = (Mouse.current != null) ? "Mouse o" : "Mouse x";
        return $"{k}, {g}, {m}";
    }
}
