using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RetryUI : MonoBehaviour
{
    [Header("Player references")]
    public PlayerInput playerInput;

    [Header("UI references")] 
    public RectTransform deathPanel;
    public RectTransform targetPosition;
    public float animationSpeed = 0.5f;
    public GameObject selectedButton;
    
    public void EnableRetryUI()
    {
        deathPanel.DOAnchorPos(targetPosition.anchoredPosition, animationSpeed);
        playerInput.SwitchCurrentActionMap("UI action map");
        EventSystem.current.SetSelectedGameObject(selectedButton);
    }

    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
