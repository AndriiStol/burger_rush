using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private bool isSoundEnabled = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleSound()
    {
        isSoundEnabled = !isSoundEnabled;
        AudioListener.volume = isSoundEnabled ? 1f : 0f;
    }

    public bool IsSoundEnabled()
    {
        return isSoundEnabled;
    }
}
    