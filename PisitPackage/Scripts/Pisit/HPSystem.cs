using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Pisit.HPSystem;

namespace Pisit
{ 
    public class HPSystem : MonoBehaviour
    {
        public float HP;
        public float MaxHP = 100f;

        public void ModifyHP( float value )
        {
            HP = HP + value;
            // Mathf.Clamp(HP, 0.0f, 100.0f);
            Mathf.Clamp(HP, 0.0f, MaxHP);
        }

        public void SetHPToMax()
        {
            HP = MaxHP;
        }
    }
}
