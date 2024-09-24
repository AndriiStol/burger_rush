using UnityEngine;
using DG.Tweening;

public class ColorChanger : MonoBehaviour
{
    public float scaleAmount = 1.5f; 
    public float duration = 1.0f; 

    private Vector3 originalScale; 

    private void Start()
    {
        
        originalScale = transform.localScale;

        
        StartAnimation();
    }

    private void StartAnimation()
    {
        
        Sequence sequence = DOTween.Sequence();

        
        sequence.Append(transform.DOScale(originalScale * scaleAmount, duration / 2))
                
                .Append(transform.DOScale(originalScale, duration / 2))
                
                .SetLoops(-1)
                
                .SetEase(Ease.InOutQuad);

        
        sequence.Play();
    }
}
