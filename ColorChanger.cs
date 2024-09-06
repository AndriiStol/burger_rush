using UnityEngine;
using DG.Tweening;

public class ColorChanger : MonoBehaviour
{
    public float scaleAmount = 1.5f; // Максимальное увеличение
    public float duration = 1.0f; // Длительность анимации

    private Vector3 originalScale; // Исходный размер картинки

    private void Start()
    {
        // Сохраняем исходный размер картинки
        originalScale = transform.localScale;

        // Запускаем анимацию увеличения и уменьшения
        StartAnimation();
    }

    private void StartAnimation()
    {
        // Создаем последовательную анимацию с помощью DOTween
        Sequence sequence = DOTween.Sequence();

        // Увеличиваем размер картинки до максимального значения
        sequence.Append(transform.DOScale(originalScale * scaleAmount, duration / 2))
                // Затем уменьшаем до исходного размера
                .Append(transform.DOScale(originalScale, duration / 2))
                // Повторяем анимацию бесконечно
                .SetLoops(-1)
                // Устанавливаем тип плавности
                .SetEase(Ease.InOutQuad);

        // Запускаем анимацию
        sequence.Play();
    }
}
