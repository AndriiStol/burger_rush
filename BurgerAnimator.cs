using UnityEngine;

public class BurgerAnimator : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameObject sizeText;


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
            sizeText.SetActive(false);

        }
    }

}
