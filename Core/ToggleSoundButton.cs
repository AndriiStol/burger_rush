using UnityEngine;
using UnityEngine.UI;

public class ToggleSoundButton : MonoBehaviour
{
    private Button toggleButton;
    private Image buttonImage;

    public Sprite soundEnabledImage;
    public Sprite soundDisabledImage; 

    private void Start()
    {
        toggleButton = GetComponent<Button>();
        buttonImage = toggleButton.image;

        UpdateButtonImage();

        toggleButton.onClick.AddListener(ToggleSound);
    }

    private void UpdateButtonImage()
    {
        if (SoundManager.Instance.IsSoundEnabled())
        {
            buttonImage.sprite = soundEnabledImage;
        }
        else
        {
            buttonImage.sprite = soundDisabledImage;
        }
    }

    private void ToggleSound()
    {
        SoundManager.Instance.ToggleSound();
        UpdateButtonImage();
    }
}
