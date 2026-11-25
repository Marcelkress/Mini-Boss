using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public string playSceneName;

    [Header("controls panel")] 
    public float animationSpeed;
    public RectTransform controlsPanel;
    public RectTransform targetPosition, closedPos;

    [Header("Buttons")] 
    public GameObject firstSelectedControlsButton;
    public GameObject firstSelectedMainButton;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstSelectedMainButton);
    }

    private bool controlsActive = false;
    
    public void Play()
    {
        SceneManager.LoadScene(playSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ToggleControls()
    {
        if (!controlsActive)
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedControlsButton);
            controlsPanel.DOAnchorPos(targetPosition.anchoredPosition, animationSpeed);
            controlsActive = true;
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(firstSelectedMainButton);
            controlsPanel.DOAnchorPos(closedPos.anchoredPosition, animationSpeed);
            controlsActive = false;
        }
    }

    public void ToggleFullScreen(bool enable)
    {
        Screen.fullScreen = enable;
    }
}
