using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionMenu : MonoBehaviour
{
    public Button[] levelButtons; 
    private int levelsUnlocked; 

    void Start()
    {
        
        levelsUnlocked = PlayerPrefs.GetInt("LevelsUnlocked", 1);

       
        UpdateLevelButtons();
    }

   
    void UpdateLevelButtons()
    {
      
        for (int i = 0; i < levelButtons.Length; i++)
        {
            
            if (i < levelsUnlocked)
            {
                levelButtons[i].interactable = true; 
                                                     
            }
            else
            {
                levelButtons[i].interactable = false; 
            }
        }
    }

    
    public void OnLevelCompleted()
    {
        
        levelsUnlocked++;

       
        PlayerPrefs.SetInt("LevelsUnlocked", levelsUnlocked);

        
        UpdateLevelButtons();
    }
}
