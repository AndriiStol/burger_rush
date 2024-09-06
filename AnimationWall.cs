using UnityEngine;

public class AnimationWall : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, соприкасается ли коллайдер этого объекта с коллайдером персонажа
        if (other.CompareTag("Player"))
        {
            // Активируем анимацию "go"
            anim.SetTrigger("Go");
        }
    }
}
