using UnityEngine;

public class FOVMesh : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private float radius;
    [SerializeField] private int segments;
    [SerializeField] private float angle;
    [SerializeField] private LayerMask layerMask;


    private MeshFilter m_Filter;
    private MeshRenderer m_Renderer;



    private void Awake()
    {
        m_Filter = gameObject.GetComponent<MeshFilter>();
        m_Renderer = gameObject.GetComponent<MeshRenderer>();

        m_Renderer.material = material;

        Mesh diskMesh = new Mesh();
        diskMesh.MarkDynamic(); // 메쉬를 동적으로 변환시킨다는 선언.
        diskMesh.name = "Disk";
        m_Filter.mesh = diskMesh;
    }

    private void LateUpdate()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        UpdateRendering();
    }

    private void UpdateRendering()
    {
        Vector3[] vertices = new Vector3[segments + 1];
        int[] triangles = new int[segments * 3];

        vertices[0] = Vector3.zero;
        float angleStep = angle / segments;

        for (int i = 1; i < segments + 1; i++)
        {
            float angleRad = Mathf.Deg2Rad * angleStep * i + (Mathf.PI - Mathf.Deg2Rad * angle) / 2;

            Vector3 directionWorld = transform.TransformDirection(new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad)));

            float collisionRadius = radius;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionWorld, out hit, radius, layerMask))
            {
                collisionRadius = hit.distance;
            }

            vertices[i] = new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad)) * collisionRadius;
        }

        for (int i = 0; i < segments - 1; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 2;
            triangles[i * 3 + 2] = i + 1;
        }

        //int[] indexes = new int[3] { 0, 1, 2 };

        m_Filter.mesh.Clear();
        m_Filter.mesh.vertices = vertices;
        m_Filter.mesh.triangles = triangles;
        m_Filter.mesh.RecalculateNormals();

           
    }
}
