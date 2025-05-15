using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO pauseEvent;
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pausePanel;

    private void Start()
    {
        pauseButton.onClick.AddListener(() => pauseEvent.Raise());
    }

    private void OnEnable()
    {
        pauseEvent.OnEventRaised += ShowPauseMenu;
    }

    private void OnDisable()
    {
        pauseEvent.OnEventRaised -= ShowPauseMenu;
    }

    private void ShowPauseMenu()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
