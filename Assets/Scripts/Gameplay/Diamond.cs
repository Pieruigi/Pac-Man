using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PM
{
    public class Diamond : MonoBehaviour
    {
        public static UnityAction<Diamond> OnEat;

        bool eat = false;

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
            if (!other.CompareTag("Player") || eat)
                return;

            eat = true;

            Destroy(gameObject);

            OnEat?.Invoke(this);
        }
    }

}
