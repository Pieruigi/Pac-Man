using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PM
{
    public interface ITeleportable
    {
        void Teleport(Transform target);
    }

}
