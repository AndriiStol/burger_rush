using TMPro;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private enum OperationType
    { 
        plus,
        minus,
        umhogit,
        razdelit
    }

    [Header("Operation")]
    [SerializeField] private OperationType gateOperation;
    [SerializeField] private int value;

    [Header("References")]
    [SerializeField] private TextMeshPro operationText;
    [SerializeField] private MeshRenderer forceField;
    [SerializeField] private Material[] operationTypeMaterial;

    private void Awake()
    {
        AssignOperation();
    }
    private void AssignOperation()
    {
        string finalText = "";

        if (gateOperation == OperationType.plus)
            finalText += "+";
        if (gateOperation == OperationType.minus)
            finalText += "-";
        if (gateOperation == OperationType.umhogit)
            finalText += "x";
        if (gateOperation == OperationType.razdelit)
            finalText += "÷";

        finalText += value.ToString();
        operationText.text = finalText;

        if (gateOperation == OperationType.plus || gateOperation == OperationType.umhogit)
            forceField.material = operationTypeMaterial[0];
        else
            forceField.material = operationTypeMaterial[1];
    }

    public void ExecuteOperation()
    {
        if (gateOperation == OperationType.plus)
            GameEvents.instance.playerSize.Value += value;
        if (gateOperation == OperationType.minus)
            GameEvents.instance.playerSize.Value -= value;
        if (gateOperation == OperationType.umhogit)
            GameEvents.instance.playerSize.Value *= value;
        if (gateOperation == OperationType.razdelit)
            GameEvents.instance.playerSize.Value /= value;

        GetComponent<BoxCollider>().enabled = false;
        forceField.gameObject.SetActive(false);
    }
}