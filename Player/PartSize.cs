using UniRx;
using UnityEngine;

public class PartSize : MonoBehaviour
{
    public IntReactiveProperty size { get; private set; } = new IntReactiveProperty(1);

    // Другие методы и логика для изменения размера части тела
}
