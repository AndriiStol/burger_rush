using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionMenu : MonoBehaviour
{
    public Button[] levelButtons; // Массив кнопок уровней
    private int levelsUnlocked; // Количество открытых уровней

    void Start()
    {
        // Загружаем количество открытых уровней из PlayerPrefs или другого источника
        levelsUnlocked = PlayerPrefs.GetInt("LevelsUnlocked", 1);

        // Определяем, какие уровни открыты, и обновляем состояние кнопок
        UpdateLevelButtons();
    }

    // Обновление состояния кнопок уровней
    void UpdateLevelButtons()
    {
        // Проходим по всем кнопкам уровней
        for (int i = 0; i < levelButtons.Length; i++)
        {
            // Если уровень открыт, делаем кнопку активной
            if (i < levelsUnlocked)
            {
                levelButtons[i].interactable = true; // Делаем кнопку активной
                                                     // Добавьте здесь логику для отображения номера уровня на кнопке, если это необходимо
            }
            else
            {
                levelButtons[i].interactable = false; // Делаем кнопку неактивной (закрытой)
            }
        }
    }

    // Этот метод должен вызываться, когда игрок завершает уровень
    public void OnLevelCompleted()
    {
        // Увеличиваем количество открытых уровней
        levelsUnlocked++;

        // Сохраняем новое количество открытых уровней в PlayerPrefs или другое хранилище
        PlayerPrefs.SetInt("LevelsUnlocked", levelsUnlocked);

        // Обновляем состояние кнопок
        UpdateLevelButtons();
    }
}