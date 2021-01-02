using UnityEngine;

public class UIWidget : MonoBehaviour
{
    private void OnValidate() {
        LookAtCamera();
    }

    private void Update() {
        LookAtCamera();
    }

    private void LookAtCamera() {
        this.GetComponent<RectTransform>().LookAt(-Camera.main.transform.position);
    }
}
