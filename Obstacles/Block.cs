using TMPro;
using UnityEngine;
using DG.Tweening;

public class Block : MonoBehaviour
{
    [SerializeField] private AudioSource lose;

    [Header("Size & Color")]
    [SerializeField] private int startingSize;
    [SerializeField] private Material[] blockColor;
    [SerializeField] private MeshRenderer blockMesh;
    [SerializeField] private MeshRenderer[] brokenBlockMeshes;

    [Header("References")]
    [SerializeField] private GameObject completeBlock;
    [SerializeField] private GameObject brokenBlock;
    [SerializeField] private TextMeshPro blockSizeText;

    private void Awake()
    {
        completeBlock.SetActive(true);
        brokenBlock.SetActive(false);
        blockSizeText.text = startingSize.ToString();
        AssignColor();
    }


    private void AssignColor()
    {
        // Generate a single random color index for both the whole block and the broken block
        int randomColorIndex = Random.Range(0, blockColor.Length);

        // Assign the random color to both materials
        blockMesh.material = blockColor[randomColorIndex];
        for (int i = 0; i < brokenBlockMeshes.Length; i++)
        {
            brokenBlockMeshes[i].material = blockColor[randomColorIndex];
        }
    }

    public void CheckHit()
    {
        Handheld.Vibrate();
        Camera.main.transform.DOShakePosition(0.1f, 0.5f, 5);

        if (GameEvents.instance.playerSize.Value > startingSize)
        {
            ParticleManager.instance.PlayParticle(0, transform.position);
            GameEvents.instance.playerSize.Value -= startingSize;
            completeBlock.SetActive(false);
            brokenBlock.SetActive(true);
            blockSizeText.gameObject.SetActive(false);
        }
        else
        {
            GameEvents.instance.gameLost.SetValueAndForceNotify(true);
            lose.Play();
        }
    }
}