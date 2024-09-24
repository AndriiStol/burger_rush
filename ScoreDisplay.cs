using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private float textAppearDelay = 0.1f; 
    private int targetScore;
    private int currentScore;

    private void Start ()
    {
        
        scoreText.text = "Your Score: 0";
        currentScore = 0;
        targetScore = 0;
    }

    
    public void UpdateScoreDisplay(int score)
    {
        targetScore = score;
        StartCoroutine(AnimateScore());
    }

    private System.Collections.IEnumerator AnimateScore()
    {
       
        for (int i = currentScore; i <= targetScore; i++)
        {
            
            scoreText.text = "Your Score: " + i.ToString();

            
            yield return new WaitForSeconds(textAppearDelay);
        }

       
        currentScore = targetScore;
    }
}
