using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour 
{
	public float movementSpeed = 5.0f;
	public float mouseSensitivity = 5.0f;
	public float jumpSpeed = 20.0f;
    public float upDownRange = 60.0f;

    private float verticalRotation = 0;
	private float verticalVelocity = 0;
	
	private CharacterController characterController;
	
	void Start () 
	{
		Cursor.lockState = CursorLockMode.Locked;
		characterController = GetComponent<CharacterController>();
	}

	void Update () 
	{
		// Rotation
		float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
		transform.Rotate(0, rotLeftRight, 0);

		verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
		Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
		
		// Movement
		float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
		float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;
		
		verticalVelocity += Physics.gravity.y * Time.deltaTime;
		
		if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space)) 
		{
			verticalVelocity = jumpSpeed;
		}
		
		Vector3 speed = new Vector3( sideSpeed, verticalVelocity, forwardSpeed );
		speed = transform.rotation * speed;
		
		characterController.Move( speed * Time.deltaTime );
	}
}
