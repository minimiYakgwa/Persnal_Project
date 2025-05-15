using UnityEngine;

public class RopeAction : MonoBehaviour
{
    public Transform player;

    Camera cam;
    RaycastHit hit;
    public LayerMask GrapplingObj;
    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RopeShoot();
        }
    }

    private void RopeShoot()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100f, GrapplingObj)){
            Debug.Log("장애물 발견");
        }
    }
}
