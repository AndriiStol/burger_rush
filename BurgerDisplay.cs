using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;


public class BurgerDisplay : MonoBehaviour
{
    public GameObject burgerPrefab; // Префаб бургера
    public Transform pyramidParent; // Родительский объект для пирамиды бургеров
    public Text sizeText; // Текстовое поле с количеством бургеров

    public ScoreDisplay scoreDisplay; // Ссылка на компонент для отображения счета

    public float burgerSpacing = 0.01f; // Расстояние между бургерами
    public float rowSpacing = 0.1f; // Расстояние между рядами бургеров

    

    public void CreateBurgerPyramidWithAnimation(int burgerCount)
    {
        // Рассчитываем максимальное количество рядов для пирамиды
        int maxRows = Mathf.CeilToInt(Mathf.Sqrt(burgerCount * 2));

        // Начинаем строить пирамиду снизу вверх
        for (int currentRow = maxRows; currentRow > 0; currentRow--)
        {
            // Рассчитываем количество бургеров в текущем ряду
            int burgersInRow = currentRow;

            // Если количество бургеров в текущем ряду больше, чем количество оставшихся бургеров, уменьшаем его
            burgersInRow = Mathf.Min(burgersInRow, burgerCount);

            // Рассчитываем позицию для текущего ряда
            Vector3 rowPosition = new Vector3(0, (maxRows - currentRow) * rowSpacing, 0);

            // Создаем ряд пирамиды
            for (int i = 0; i < burgersInRow; i++)
            {
                // Рассчитываем позицию для текущего бургера
                Vector3 burgerPosition = new Vector3((i - burgersInRow / 2f) * burgerSpacing, 0, 0);

                // Создаем новый бургер
                GameObject burger = Instantiate(burgerPrefab, pyramidParent);

                // Рассчитываем начальный масштаб для анимации
                burger.transform.localScale = Vector3.zero;

                // Уменьшаем количество оставшихся бургеров
                burgerCount--;

                // Анимируем появление бургера
                Sequence sequence = DOTween.Sequence();
                sequence.Append(burger.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).SetDelay(0.1f * i));
                sequence.OnComplete(() => StartCoroutine(EnableRigidbodyAfterDelay(burger.GetComponent<Rigidbody>(), 2f)));

                // Устанавливаем позицию бургера
                burger.transform.localPosition = rowPosition + burgerPosition;
            }

            // Если бургеров больше нет, завершаем построение пирамиды
            if (burgerCount <= 0)
                break;
        }

        // Обновляем отображаемый счет
        
    }

    private IEnumerator EnableRigidbodyAfterDelay(Rigidbody rb, float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.isKinematic = false;
        scoreDisplay.UpdateScoreDisplay(int.Parse(sizeText.text));
    }
}
