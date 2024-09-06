using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private float textAppearDelay = 0.1f; // Задержка перед появлением каждой цифры
    private int targetScore;
    private int currentScore;

    private void Start ()
    {
        // Устанавливаем начальное значение текста
        scoreText.text = "Your Score: 0";
        currentScore = 0;
        targetScore = 0;
    }

    // Метод для обновления отображаемого количества бургеров
    public void UpdateScoreDisplay(int score)
    {
        targetScore = score;
        StartCoroutine(AnimateScore());
    }

    private System.Collections.IEnumerator AnimateScore()
    {
        // Проходим по каждой цифре
        for (int i = currentScore; i <= targetScore; i++)
        {
            // Задаем текст с учетом текущей цифры
            scoreText.text = "Your Score: " + i.ToString();

            // Ждем заданное время перед следующей цифрой
            yield return new WaitForSeconds(textAppearDelay);
        }

        // Обновляем текущий счет до целевого счета
        currentScore = targetScore;
    }
}
