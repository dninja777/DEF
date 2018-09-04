using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ProjectileType
{
    rock, arrow, fireball
};
public class Projectile : MonoBehaviour {

    //when you make a variable private you need to create a getter to get the value from it


        /* for example

         private int defenceStrength;

        to get the defenceStrength create a getter


        public int DefenceStrength {
        
        get { return defenceStrength 
        }

        */


    [SerializeField] private int attackStrength;
    [SerializeField] private ProjectileType projectileType;

  

    public int AttackStrength
    {
        get { return attackStrength; }
    }

    public ProjectileType ProjectileType
    {
        get { return projectileType;  }
    }

 
}
