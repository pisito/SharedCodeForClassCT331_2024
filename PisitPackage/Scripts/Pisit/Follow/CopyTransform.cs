using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pisit.Follow
{
    public class CopyTransform : MonoBehaviour
    {
        public Transform target;
        public float speed = 1.0f;
        public float epsilon = 0.4f;

        public Vector3 offset = new Vector3(0f, 0f, -5f);

        // Update is called once per frame
        void Update()
        {
            if (target == null) return;
            if(Vector2.Distance(transform.position, (target.position + offset)) > epsilon)
            {
                transform.position = Vector2.Lerp(transform.position, target.position, speed * Time.deltaTime);
            }
        }
    }
}
