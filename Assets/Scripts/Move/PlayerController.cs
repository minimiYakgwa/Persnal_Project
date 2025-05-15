using Mono.Cecil.Cil;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rigid;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float rotSpeed = 0.1f;

    private void FixedUpdate()
    {
        Move();     
    }
    private void Update()
    {
        isJump();
    }
    private void isJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
    }
    private void Move()
    {
        
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 localDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
        Vector3 worldDirection = transform.TransformDirection(localDirection);

        if (worldDirection == Vector3.zero)
            return;

        rigid.MovePosition(rigid.position + worldDirection * speed * Time.fixedDeltaTime);
        if (worldDirection.magnitude > 0.1f && !Mathf.Approximately(horizontalInput, 0))
        {
            Quaternion rotation = Quaternion.LookRotation(worldDirection);
            rigid.MoveRotation(Quaternion.Slerp(rigid.rotation, rotation, rotSpeed));
        }
    }
        


}
