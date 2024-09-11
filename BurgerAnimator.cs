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
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("Go");
            sizeText.SetActive(false);

        }
    }

}
