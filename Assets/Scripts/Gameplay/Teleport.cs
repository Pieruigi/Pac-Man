using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PM
{
    public class Teleport : MonoBehaviour
    {
        [SerializeField]
        Transform target;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            ITeleportable teleportable = other.GetComponent<ITeleportable>();

            if (teleportable != null)
                teleportable.Teleport(target);
        }
    }

}
