using UniRx;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();

    [SerializeField] private Vector3 minPosition;
    [SerializeField] private Vector3 maxPosition;
    [SerializeField] private Vector3 winPosition;
    [SerializeField] private Vector3 winRotation; // Добавлено поле для задания позиции при победе
    [SerializeField] private Vector3 losePosition;
    [SerializeField] private Vector3 loseRotation; // Добавлено поле для задания позиции при победе
    [SerializeField] private Vector3 blevotinaPosition;
    [SerializeField] private Vector3 blevotinaRotation; // Добавлено поле для задания позиции при получении "blevotina"
    private float progress;

    private void OnEnable()
    {
        StartCoroutine(Subscribe());
    }
    private IEnumerator Subscribe()
    {
        yield return new WaitUntil(() => GameEvents.instance != null);
        GameEvents.instance.playerSize.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (!GameEvents.instance.gameWon.Value && !GameEvents.instance.gameLost.Value)
                {
                    float progress = (float)(value - 1) / 40;
                    Vector3 currentPos = Vector3.Lerp(minPosition, maxPosition, progress);
                    transform.DOLocalMove(currentPos, 1);
                }
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameWon.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                {
                    transform.DOLocalMove(winPosition, 1).SetDelay(0.5f);
                    transform.DOLocalRotate(winRotation, 1).SetDelay(0.5f); 
                }
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameLost.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                {
                    transform.DOLocalMove(losePosition, 1).SetDelay(0.5f);
                    transform.DOLocalRotate(loseRotation, 1).SetDelay(0.5f); 
                }
            })
            .AddTo(subscriptions);

        GameEvents.instance.blevotina.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                if (value)
                {
                    transform.DOLocalMove(blevotinaPosition, 1).SetDelay(0.5f);
                    transform.DOLocalRotate(blevotinaRotation, 1).SetDelay(0.5f); 
                }
            })
            .AddTo(subscriptions);
    }
    private void OnDisable()
    {
        subscriptions.Clear();
    }
}
