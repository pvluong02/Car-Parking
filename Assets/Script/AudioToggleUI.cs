using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioToggleUI : MonoBehaviour
{
    public Button muteButton;
    public GameObject soundOnIcon;
    public GameObject soundOffIcon;
    private Image buttonImage;

    private void Start()
    {
        buttonImage = muteButton.GetComponentInChildren<Image>();

        UpdateUI();
        muteButton.onClick.AddListener(ToggleSound);

    }
    private void ToggleSound()
    {
        AudioManager.Instance.ToggleMute();
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (AudioManager.Instance.IsMuted())
        {
            soundOffIcon.SetActive(true);
            soundOnIcon.SetActive(false);
        }
        else
        {
            soundOnIcon.SetActive(true);
            soundOffIcon.SetActive(false);
        }
    }
}
