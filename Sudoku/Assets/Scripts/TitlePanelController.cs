using UnityEngine;
using UnityEngine.UI;
using System;

public class TitlePanelController : MonoBehaviour
{
    [SerializeField] private Button playButton;

    public event Action OnPlayClicked;

    private void Awake()
    {
        playButton.onClick.AddListener(Play);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(Play);
    }

    private void Play()
    {
        // play button pressed
        OnPlayClicked?.Invoke();
    }
}
