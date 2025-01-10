using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace PM
{
    public class PlayerController : MonoBehaviour, ITeleportable
    {
        [SerializeField]
        float pitchMax = 80;
       

        [SerializeField]
        float rotationSpeedMax = 720;
        public float RotationSpeedMax
        {
            get { return rotationSpeedMax; }
        }

        [SerializeField]
        float moveSpeedMax = 4;

        [SerializeField]
        float acceleration = 10;

        [SerializeField]
        float deceleration = 10;

        CharacterController cc;

        float yaw = 0;
        public float Yaw
        {
            get { return yaw; }
        }
        float pitch = 0;
        public float Pitch
        {
            get { return pitch; }
        }

        float mouseSens = 1;
        Vector3 currentVelocity;
        
        private void Awake()
        {
            cc = GetComponent<CharacterController>();
        }

        // Start is called before the first frame update
        void Start()
        {
            Application.targetFrameRate = -1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        void Update()
        {
            // Get input
            var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            var aimInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            // Compute yaw and pitch
            yaw += aimInput.x * mouseSens;
            pitch -= aimInput.y * mouseSens;
            pitch = Mathf.Clamp(pitch, -pitchMax, pitchMax);

            // Rotate yaw
            var rotTarget = Quaternion.Euler(0, yaw, 0); // This is the target rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotTarget, rotationSpeedMax * Time.deltaTime);


            // Move
            var moveDir = transform.TransformDirection(new Vector3(input.x, 0, input.y)).normalized;
            
            var targetVelocity = moveSpeedMax * moveDir;
            
            if(moveDir.magnitude > 0) // Accelerate
            {
                currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, acceleration * Time.deltaTime);
             
            }
            else // Decellerate
            {
                currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, deceleration * Time.deltaTime);
            }

            cc.Move(currentVelocity * Time.deltaTime);
        }

        public void Teleport(Transform target)
        {
            Teleport(target.position);
        }

        public void Teleport(Vector3 position)
        {
            cc.enabled = false;
            transform.position = position;
            cc.enabled = true;
        }
        
    }

}
