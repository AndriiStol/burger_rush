using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;


public class BurgerDisplay : MonoBehaviour
{
    public GameObject burgerPrefab; 
    public Transform pyramidParent; 
    public Text sizeText; 

    public ScoreDisplay scoreDisplay; 

    public float burgerSpacing = 0.01f; 
    public float rowSpacing = 0.1f;

    

    public void CreateBurgerPyramidWithAnimation(int burgerCount)
    {
        
        int maxRows = Mathf.CeilToInt(Mathf.Sqrt(burgerCount * 2));

       
        for (int currentRow = maxRows; currentRow > 0; currentRow--)
        {
           
            int burgersInRow = currentRow;

            burgersInRow = Mathf.Min(burgersInRow, burgerCount);

            Vector3 rowPosition = new Vector3(0, (maxRows - currentRow) * rowSpacing, 0);

            for (int i = 0; i < burgersInRow; i++)
            {
                Vector3 burgerPosition = new Vector3((i - burgersInRow / 2f) * burgerSpacing, 0, 0);

                GameObject burger = Instantiate(burgerPrefab, pyramidParent);

                burger.transform.localScale = Vector3.zero;

                burgerCount--;

                Sequence sequence = DOTween.Sequence();
                sequence.Append(burger.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).SetDelay(0.1f * i));
                sequence.OnComplete(() => StartCoroutine(EnableRigidbodyAfterDelay(burger.GetComponent<Rigidbody>(), 2f)));

                burger.transform.localPosition = rowPosition + burgerPosition;
            }

            if (burgerCount <= 0)
                break;
        }

        
    }

    private IEnumerator EnableRigidbodyAfterDelay(Rigidbody rb, float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.isKinematic = false;
        scoreDisplay.UpdateScoreDisplay(int.Parse(sizeText.text));
    }
}
