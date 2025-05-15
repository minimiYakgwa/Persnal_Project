using UnityEngine;

public class BuffVFXSpin : MonoBehaviour
{
    [SerializeField]
    private float spinSpeed = 10f;

    private void Update()
    {
        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
    }
}
