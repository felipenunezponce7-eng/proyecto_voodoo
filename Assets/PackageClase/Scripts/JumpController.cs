using UnityEngine;

public class JumpController : MonoBehaviour
{
	
    private Rigidbody rb;
	private Animator animator;
	public bool isGrounded = false;
    public float jumpStrenght = 300f;
    public LayerMask groundLayer;
    private RaycastHit hit;
	private CapsuleCollider capsuleCollider;
	void Start()
    {
        rb = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		capsuleCollider = GetComponent<CapsuleCollider>();
	}
    void Update()
    {
        bool wasGrounded = isGrounded;
		Vector3 origin = transform.position + capsuleCollider.center;

		float distance = (capsuleCollider.height / 2f) - capsuleCollider.radius + 0.01f;

		isGrounded = Physics.SphereCast(
			origin,
			capsuleCollider.radius,
			Vector3.down,
			out RaycastHit hit,
			distance
		);

		if (wasGrounded != isGrounded && isGrounded) //al caer nuevamente al piso //nuevo toda la condicion
        {
            animator.SetTrigger("Land");
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
            rb.AddForce(Vector3.up * jumpStrenght);
            animator.SetTrigger("Jump"); //nuevo ****
        }
    }
	private void OnDrawGizmosSelected()
	{
		if (capsuleCollider == null)
			capsuleCollider = GetComponent<CapsuleCollider>();

		Vector3 origin = transform.position + capsuleCollider.center;
		float radius = capsuleCollider.radius;
		float distance = (capsuleCollider.height / 2f) - capsuleCollider.radius + 0.01f;

		Vector3 direction = Vector3.down;
		Vector3 endPosition = origin + direction * distance;

		// Color según estado
		Gizmos.color = isGrounded ? Color.green : Color.red;

		// Esfera inicial
		Gizmos.DrawWireSphere(origin, radius);

		// Esfera final
		Gizmos.DrawWireSphere(endPosition, radius);

		// Línea de trayectoria
		Gizmos.DrawLine(origin, endPosition);
	}
}
