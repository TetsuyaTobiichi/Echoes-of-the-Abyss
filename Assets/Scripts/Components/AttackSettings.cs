
using System;

namespace Constants
{
    [Serializable]
    public struct AttackSettings
    {
        public float AttackSpeed;
        public float AttackRange;
        //without weapon or without weapon scale damage
        public float AttackDamage;
    }
}