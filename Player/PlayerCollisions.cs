using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections;
public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] private GameObject bloodParticles;
    private Animator playerAnim;
    [SerializeField] private AudioSource win;
    [SerializeField] private AudioSource gate;
    [SerializeField] private AudioSource size;
    [SerializeField] private AudioSource breakWall;
    [SerializeField] private AudioSource blevotina;
    [SerializeField] private AudioSource pop;
    [SerializeField] private AudioSource lose;
    [SerializeField] private AudioSource fire;
    [SerializeField] private AudioSource auuu;
    [SerializeField] private AudioSource buldozer;
    [SerializeField] private BurgerProgressBar BurgerProgressBar;
    [SerializeField] private BurgerDisplay BurgerDisplay;



    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        bloodParticles.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Size")
        {
            GameEvents.instance.playerSize.Value += 1;
            size.Play();
            other.GetComponent<Collider>().enabled = false;
            other.transform.DOScale(Vector3.zero, 0.5f).OnComplete(()=>
            {
                Destroy(other.gameObject);
            });
        }

        if (other.tag == "blevotina")
        {
            playerAnim.SetTrigger("blevotina");
            blevotina.Play();
            lose.Play();
            GameEvents.instance.blevotina.SetValueAndForceNotify(true);
            GetComponent<Collider>().enabled = false;

            {
                Destroy(other.gameObject);
            };
        }

        if (other.tag == "Obstacle")
        {
            playerAnim.SetTrigger("kick");
            breakWall.Play();
            other.GetComponent<Block>().CheckHit();
        }
        if (other.tag == "Gate")
        {
            gate.Play();
            other.GetComponent<Gate>().ExecuteOperation();
        }

        if (other.tag == "Saw")
        {
            GameEvents.instance.gameLost.SetValueAndForceNotify(true);
            auuu.Play();
            StartCoroutine(PlayLoseSoundAfterDelay());
            bloodParticles.SetActive(true);
            GetComponent<Collider>().enabled = false;
        }
        if (other.tag == "Finish")
        {
            
            win.Play();
            buldozer.Play();
            fire.Play();
            GameEvents.instance.gameWon.SetValueAndForceNotify(true);
            BurgerDisplay.CreateBurgerPyramidWithAnimation(GameEvents.instance.playerSize.Value);
            BurgerProgressBar.Start();

        }
    }


    IEnumerator PlayLoseSoundAfterDelay()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second
        lose.Play();
    }

    public void Pop()
    {
        pop.Play();
    }


}