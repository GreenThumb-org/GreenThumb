using TMPro;
using UnityEngine;

public class TestCropBillboard : MonoBehaviour
{
    private TMP_Text labelText;
    private FieldData fieldData;

    private Transform cameraTransform;

    private void Start()
    {
        if (Camera.main != null)
            cameraTransform = Camera.main.transform;

        if (fieldData == null)
            fieldData = GetComponentInParent<FieldData>();

        if (labelText == null)
            labelText = GetComponentInChildren<TMP_Text>();

        if (fieldData != null && labelText != null)
            labelText.text = fieldData.CropType;
    }

    public void Initialize(FieldData data)
    {
        fieldData = data;

        if (labelText == null)
            labelText = GetComponentInChildren<TMP_Text>();

        if (fieldData != null && labelText != null)
            labelText.text = fieldData.FieldName;
    }

    private void LateUpdate()
    {
        if (cameraTransform == null)
            return;

        Vector3 lookDirection = transform.position - cameraTransform.position;
        lookDirection.y = 0f;

        if (lookDirection.sqrMagnitude > 0.001f)
            transform.rotation = Quaternion.LookRotation(lookDirection);
    }
}