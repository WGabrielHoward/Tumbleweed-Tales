using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Scripts.Components
{
    public struct MovementComponent
    {
        // body and ability to move
        public Rigidbody rigidbody;
        public float moveSpeed;

        // movement direction and input
        public Vector3 moveDirection;
        public int moveInput;

    }
}
