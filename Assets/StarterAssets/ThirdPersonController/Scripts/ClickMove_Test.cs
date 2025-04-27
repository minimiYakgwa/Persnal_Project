using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class ClickMove_Test : MonoBehaviour
{
    NavMeshAgent nav;
    Animator anim;

    [SerializeField]
    Transform spot;

    LineRenderer lr;

    Coroutine draw;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        lr = GetComponent<LineRenderer>();

        lr.startWidth = 0.1f;
        lr.material.color = Color.red; 
        lr.endWidth = 0.1f;
        lr.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                nav.velocity = Vector3.zero;
                nav.SetDestination(hit.point);

                anim.SetFloat("Speed", 2);
                anim.SetFloat("MotionSpeed", 2);

                spot.gameObject.SetActive(true);
                spot.position = hit.point;

                if (draw != null) StopCoroutine(draw);
                draw = StartCoroutine(DrawLine());
                

            }
        }

        else if (nav.remainingDistance < 0.2f)
        {
            anim.SetFloat("Speed", 0f);
            anim.SetFloat("MotionSpeed", 0f);

            spot.gameObject.SetActive(false);

            lr.enabled = false;

            if (draw != null) StopCoroutine(draw);
        }
    }

    private IEnumerator DrawLine()
    {
        lr.enabled = true;
        yield return null;
        while (true)
        {
            int cnt = nav.path.corners.Length;
            lr.positionCount = cnt;
            for (int i = 0; i< cnt; i++)
            {
                lr.SetPosition(i, nav.path.corners[i]);
            }
            yield return null;
        }
    }
}
