using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class PersonClickController : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    public Transform spot;

    LineRenderer lr;

    Coroutine draw;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        lr = GetComponent<LineRenderer>();
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.material.color = Color.red;
        lr.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.velocity = Vector3.zero;
                agent.SetDestination(hit.point);
                anim.SetFloat("Speed", 2.0f);
                anim.SetFloat("MotionSpeed", 2.0f);

                spot.gameObject.SetActive(true);
                spot.position = hit.point;

                if (draw != null) StopCoroutine(draw);
                draw = StartCoroutine(DrawPath());
            }
        }

        else if (agent.remainingDistance < 0.1f)
        {
            anim.SetFloat("Speed", 0f);
            anim.SetFloat("MotionSpeed", 0f);
            spot.gameObject.SetActive(false);

            lr.enabled = false;
            if (draw != null) StopCoroutine(draw);
            StopCoroutine(draw);
        }
    }
    
    IEnumerator DrawPath()
    {
        lr.enabled = true;
        yield return null;
        while (true)
        {
            int cnt = agent.path.corners.Length;
            lr.positionCount = cnt;
            for (int i = 0; i< cnt; i++)
            {
                lr.SetPosition(i, agent.path.corners[i]);
            }
            yield return null;
        }
    }
}
