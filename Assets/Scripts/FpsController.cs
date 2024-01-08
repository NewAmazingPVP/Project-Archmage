using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
   public Camera playerCamera;
   public float walkSpeed = 6f;
   public float runSpeed = 12f;
   public float jumpPower = 7f;
   public float gravity = 10f;

   public float lookSpeed = 2f;
   public float lookXLimit = 45f;

   private bool isCrouching;
   private Vector3 originalCenter;
   private float originalHeight;
   private float originalWalkSpeed;

   Vector3 moveDirection = Vector3.zero;
   float rotationX = 0;

   public bool canMove = true;

   CharacterController characterController;
   void Start()
   {
       characterController = GetComponent<CharacterController>();
       originalCenter = characterController.center;
       originalHeight = characterController.height;
       originalWalkSpeed = walkSpeed;
       Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false;
   }

   void Update()
   {
       Vector3 forward = transform.TransformDirection(Vector3.forward);
       Vector3 right = transform.TransformDirection(Vector3.right);

       bool isRunning = Input.GetKey(KeyCode.LeftShift);
       float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
       float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
       float movementDirectionY = moveDirection.y;
       moveDirection = (forward * curSpeedX) + (right * curSpeedY);

       if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
       {
           moveDirection.y = jumpPower;
       }
       else
       {
           moveDirection.y = movementDirectionY;
       }

       if (!characterController.isGrounded)
       {
           moveDirection.y -= gravity * Time.deltaTime * 4;
       }

       characterController.Move(moveDirection * Time.deltaTime);

       if (canMove)
       {
           rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
           rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
           playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
           transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
       }

       // Handle Crouching
       if (Input.GetButtonDown("Crouch") && canMove && characterController.isGrounded)
       {
           characterController.height = 0.5f;
           characterController.center = new Vector3(0, -0.5f, 0);
           walkSpeed = 3f;
           isCrouching = true;
       }
       else if (Input.GetButtonUp("Crouch") && canMove && characterController.isGrounded)
       {
           Vector3 point0 = transform.position + originalCenter - new Vector3(0.0f, originalHeight, 0.0f);         
           Vector3 point1 = transform.position + originalCenter + new Vector3(0.0f, originalHeight, 0.0f);
           if (Physics.OverlapCapsule(point0, point1, characterController.radius).Length == 0)
           {
               characterController.height = originalHeight;
               characterController.center = originalCenter;
               walkSpeed = originalWalkSpeed;
               isCrouching = false;
           }
       }
   }
}