using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {   


    private static T shared;

    public static T Shared
    {
        get
        {

            if (shared == null)
            {
                shared = FindObjectOfType<T>();

            }
            else if (shared != FindObjectOfType<T>())
            {
                Destroy(FindObjectOfType<T>());


            }
            DontDestroyOnLoad(FindObjectOfType<T>());


            return shared;
        }
    }

        
  
}
