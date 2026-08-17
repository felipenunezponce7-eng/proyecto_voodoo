using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public enum PlayerLookAt
{
    CameraDirection,
    MovingDirection,
}
public class ThirdPersonMovement : MonoBehaviour
{
    private Camera cam;
	private Animator animator;
	private Rigidbody rb;
	public float speed = 6f;
    private Vector3 smoothDirection = Vector3.zero;
    private float tValue = 6f; //animation transition value
    public float rotationVelocity = 500f;
    private Vector3 direction;
	public PlayerLookAt lookAt;
	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
	}
	private void Start()
    {
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked; //ESC para salir
    }
    void Update()
    {
        float haxis = Input.GetAxisRaw("Horizontal");
        float vaxis = Input.GetAxisRaw("Vertical");
        direction = cam.transform.rotation * new Vector3(haxis, 0, vaxis);
        direction.y = 0; //cancelar movimiento en Y

        //Separo velocidad XZ de la velocidad de caida en Y
		rb.linearVelocity = new Vector3(direction.x, 0, direction.z).normalized * speed + new Vector3(0, rb.linearVelocity.y, 0);

		if (Input.GetMouseButtonDown(0)) 
		{
            animator.SetTrigger("Attack");
        }           
        if (Keyboard.current.digit3Key.wasPressedThisFrame)
		{
            animator.SetTrigger("Muerte");           
        }            
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
		{
            animator.SetTrigger("Pataarea");   
        }           
        if (Keyboard.current.digit2Key.wasPressedThisFrame) 
		{
            animator.SetTrigger("Pataatras");                   
        }
         animator.SetBool("Defend", Input.GetMouseButton(1));

		if (lookAt == PlayerLookAt.CameraDirection)
		{
			smoothDirection = Vector3.MoveTowards(smoothDirection, new Vector3(haxis, 0, vaxis), Time.deltaTime * tValue);
			ChangeDirection(cam.transform.forward);
		}
		if (lookAt == PlayerLookAt.MovingDirection)
		{
			smoothDirection = Vector3.MoveTowards(smoothDirection, new Vector3(0, 0, direction.magnitude), Time.deltaTime * tValue);
			ChangeDirection(direction);
		}

		animator.SetFloat("ejeX", smoothDirection.x);
		animator.SetFloat("ejeZ", smoothDirection.z);
	}

    void ChangeDirection(Vector3 directionToLook )
    {
		if (direction.magnitude > 0)
		{
			directionToLook.y = 0;
			Quaternion targetRotation = Quaternion.LookRotation(directionToLook);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationVelocity);
		}
	}

	void FixedUpdate()
    {
		float currentMovSpeed = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).magnitude;
		currentMovSpeed = Mathf.Clamp(currentMovSpeed, 0, speed);

		if (direction.magnitude > 0.5f)
			rb.linearVelocity = new Vector3(direction.x, 0, direction.z).normalized * currentMovSpeed + new Vector3(0, rb.linearVelocity.y, 0);
	}
}
