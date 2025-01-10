using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace PM
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField]
        Transform cameraTarget;

        Transform target;
        PlayerController controller;


        // Start is called before the first frame update
        void Start()
        {
            target = FindObjectOfType<PlayerController>().transform;
            controller = target.GetComponent<PlayerController>();
            //transform.rotation = target.transform.rotation;
            transform.parent = cameraTarget;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }


        private void LateUpdate()
        {
            // Apply pitch
            var pitch = controller.Pitch;
            var rotMax = controller.RotationSpeedMax;
            var yaw = transform.eulerAngles.y;
            var rot = Quaternion.Euler(pitch, yaw, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, rotMax * Time.deltaTime);
        }
    }

}
