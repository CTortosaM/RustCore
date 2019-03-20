using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private string horizontalInputName = "Horizontal";
    [SerializeField] private string verticalInputName = "Vertical";
    [SerializeField] private float movementSpeed = 16.5f;

    [SerializeField] private float slopeForce = 5;
    [SerializeField] private float slopeForceRayLength = 5;

    private CharacterController charController;

    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier = 5;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode dashKey = KeyCode.LeftShift;
    [SerializeField] private float dashSpeedMultiplier = 3f;

    [SerializeField] private float dashCoolDown = 1f;
    [SerializeField] private float dashDuration = .17f;

    //Variables de control de cooldown
    private bool isJumping;
    private bool isDashing;
    private bool canDash;
    private float nextPossibleDashTime;



    //Propiedades
    public float DashCoolDown { get => DashCoolDown1; set => DashCoolDown1 = value; }
    public float DashDuration { get => DashDuration1; set => DashDuration1 = value; }
    public float DashCoolDown1 { get => dashCoolDown; set => dashCoolDown = value; }
    public float DashDuration1 { get => dashDuration; set => dashDuration = value; }
    public AnimationCurve JumpFallOff { get => jumpFallOff; set => jumpFallOff = value; }
    public float JumpMultiplier { get => jumpMultiplier; set => jumpMultiplier = value; }
    public KeyCode JumpKey { get => jumpKey; set => jumpKey = value; }
    public KeyCode DashKey { get => dashKey; set => dashKey = value; }
    public float DashSpeedMultiplier { get => dashSpeedMultiplier; set => dashSpeedMultiplier = value; }
    public string HorizontalInputName { get => horizontalInputName; set => horizontalInputName = value; }
    public string VerticalInputName { get => verticalInputName; set => verticalInputName = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public float SlopeForce { get => slopeForce; set => slopeForce = value; }
    public float SlopeForceRayLength { get => slopeForceRayLength; set => slopeForceRayLength = value; }
    ///////////////////////////////////////////////////


    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        canDash = true;
    }

    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float horizInput = Input.GetAxis(HorizontalInputName);
        float vertInput = Input.GetAxis(VerticalInputName);

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        charController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f) * MovementSpeed);

        if ((vertInput != 0 || horizInput != 0) && OnSlope())
            charController.Move(Vector3.down * charController.height / 2 * SlopeForce * Time.deltaTime);

        JumpInput();
        DashInput();
    }

    private bool OnSlope()
    {
        if (isJumping)
            return false;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, charController.height / 2 * SlopeForceRayLength))
            if (hit.normal != Vector3.up)
                return true;
        return false;
    }

    private void JumpInput()
    {
        if (Input.GetKeyDown(JumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }


    private IEnumerator JumpEvent()
    {
        charController.slopeLimit = 90.0f;
        float timeInAir = 0.0f;
        do
        {
            float jumpForce = JumpFallOff.Evaluate(timeInAir);
            charController.Move(Vector3.up * jumpForce * JumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

        charController.slopeLimit = 45.0f;
        isJumping = false;
    }

    private void DashInput()
    {
        
        if (Input.GetKeyDown(DashKey) && canDash && Time.time >= nextPossibleDashTime)
        {
            nextPossibleDashTime = Time.time + DashCoolDown;
            canDash = false;
            isDashing = true;
            StartCoroutine(dashEvent());
        }
    }

    private IEnumerator dashEvent()
    {
        
        float OGSpeed = MovementSpeed;
        MovementSpeed *= DashSpeedMultiplier;
        yield return new WaitForSeconds(DashDuration);
        MovementSpeed = OGSpeed;
        charController.Move(new Vector3(0,0,0));
        isDashing = false;
        canDash = true;
    }
  

}

    
