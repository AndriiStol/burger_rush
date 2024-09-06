using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections;

public class BurgerProgressBar : MonoBehaviour
{
    public int totalBurgersInLevel = 20; // Общее количество бургеров на уровне
    public GameObject filledStarPrefab; // Префаб заполненной звезды
    public Transform starsParent; // Родительский объект для звезд
    public float starAppearDelay = 0.5f; // Задержка перед появлением каждой звезды
    public float appearScale = 1.5f; // Масштаб при появлении звезды
    public float appearDuration = 0.5f; // Длительность анимации появления звезды
    public float returnDuration = 0.3f; // Длительность анимации возвращения звезды к исходному размеру

    private Vector3 originalScale; // Исходный размер звезды

    public void Start ()
    {
        CalculateRating();
    }

    private void CalculateRating()
    {
        int collectedBurgers = GameEvents.instance.playerSize.Value; // Количество собранных бургеров
        float percentage = (float)collectedBurgers / totalBurgersInLevel; // Процент собранных бургеров от общего количества

        // Рассчитываем количество звезд, которые будут отображены в зависимости от процента собранных бургеров
        int starsToShow = Mathf.Clamp(Mathf.FloorToInt(percentage * starsParent.childCount), 0, starsParent.childCount);

        // Отображаем звезды с задержкой
        StartCoroutine(ShowStarsWithDelay(starsToShow));
    }

    private IEnumerator ShowStarsWithDelay(int starsToShow)
    {
        yield return new WaitForSeconds(2f); // Задержка в 2 секунды перед отображением звезд

        StartCoroutine(ShowStars(starsToShow));
    }

    private IEnumerator ShowStars(int starsToShow)
    {
        for (int i = 0; i < starsToShow; i++)
        {
            GameObject star = starsParent.GetChild(i).gameObject;

            // Сохраняем исходный размер звезды
            originalScale = star.transform.localScale;

            // Задержка перед появлением текущей звезды
            yield return new WaitForSeconds(starAppearDelay);

            // Запускаем анимацию появления звезды
            star.transform.localScale = Vector3.zero;
            star.SetActive(true);
            star.transform.DOScale(Vector3.one * appearScale, appearDuration).SetEase(Ease.OutBack).OnComplete(() =>
            {
                // После завершения анимации возвращаем звезде предыдущий размер
                star.transform.DOScale(originalScale, returnDuration).SetEase(Ease.InOutQuad);
            });

            // Заполняем звезду
            star.GetComponent<Image>().sprite = filledStarPrefab.GetComponent<Image>().sprite;
        }
    }
}
