using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections;

public class BurgerProgressBar : MonoBehaviour
{
    public int totalBurgersInLevel = 20; 
    public GameObject filledStarPrefab; 
    public Transform starsParent; 
    public float starAppearDelay = 0.5f; 
    public float appearScale = 1.5f; 
    public float appearDuration = 0.5f; 
    public float returnDuration = 0.3f; 

    private Vector3 originalScale; 

    public void Start ()
    {
        CalculateRating();
    }

    private void CalculateRating()
    {
        int collectedBurgers = GameEvents.instance.playerSize.Value; 
        float percentage = (float)collectedBurgers / totalBurgersInLevel;

        
        int starsToShow = Mathf.Clamp(Mathf.FloorToInt(percentage * starsParent.childCount), 0, starsParent.childCount);

        
        StartCoroutine(ShowStarsWithDelay(starsToShow));
    }

    private IEnumerator ShowStarsWithDelay(int starsToShow)
    {
        yield return new WaitForSeconds(2f); 

        StartCoroutine(ShowStars(starsToShow));
    }

    private IEnumerator ShowStars(int starsToShow)
    {
        for (int i = 0; i < starsToShow; i++)
        {
            GameObject star = starsParent.GetChild(i).gameObject;

           
            originalScale = star.transform.localScale;

           
            yield return new WaitForSeconds(starAppearDelay);

           
            star.transform.localScale = Vector3.zero;
            star.SetActive(true);
            star.transform.DOScale(Vector3.one * appearScale, appearDuration).SetEase(Ease.OutBack).OnComplete(() =>
            {
                
                star.transform.DOScale(originalScale, returnDuration).SetEase(Ease.InOutQuad);
            });

           
            star.GetComponent<Image>().sprite = filledStarPrefab.GetComponent<Image>().sprite;
        }
    }
}
