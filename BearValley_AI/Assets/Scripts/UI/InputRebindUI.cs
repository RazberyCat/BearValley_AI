using UnityEngine;
using UnityEngine.UI;

public class InputRebindUI : MonoBehaviour
{
    [SerializeField] private Button rebindJumpButton;
    [SerializeField] private Button rebindAttackButton;

    private void Start()
    {
        rebindJumpButton.onClick.AddListener(() => StartRebind("Player/Jump"));
        rebindAttackButton.onClick.AddListener(() => StartRebind("Player/Attack"));
    }

    private void StartRebind(string actionPath)
    {
        Debug.Log($"Rebinding {actionPath}");
        var pathSplit = actionPath.Split('/');
        Managers.Input.StartRebind(pathSplit[1], 0, () =>
        {
            Debug.Log($"{actionPath} rebound!");
        });
    }
}
