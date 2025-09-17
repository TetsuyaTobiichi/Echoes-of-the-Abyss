using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    public interface EnemyIde
    {
        public List<Transform> MovePoints { get; }
    }
}