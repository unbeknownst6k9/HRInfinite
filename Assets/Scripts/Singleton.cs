using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * this class is provided from https://gamedev.stackexchange.com/questions/116009/in-unity-how-do-i-correctly-implement-the-singleton-pattern
 * this abstract class create class that requires to be a singleton
 */
public abstract class Singleton<T> : Singleton where T: MonoBehaviour
{
    #region Fields
    /*can be null*/
    private static T _instance;

    /*can not be null*/
    private static readonly object Lock= new object();

    [SerializeField]
    private bool _persistent = true;
    #endregion

    #region Properties
    /*cannot be null*/
    public static T Instance 
    { 
        get
        {
            if (Quitting)
            {
                Debug.LogWarning($"[{nameof(Singleton)}<{typeof(T)}>] Instance will not be returned because the application is quitting");
                return null;
            }
            lock (Lock)
            {
                if (_instance != null)
                {
                    return _instance;
                }
                var instances = FindObjectsOfType<T>();
                var count = instances.Length;
                if(count > 0)
                {
                    if(count == 1)
                    {
                        return _instance = instances[0];
                    }
                    Debug.LogWarning($"[{nameof(Singleton)}<{typeof(T)}>] There should only be one object of {nameof(Singleton)} of Type <{typeof(T)}> in the scene, but {count} were found. The first instance found will be used, all others will be destroyed.");
                    for(var i = 1; i < instances.Length; i++)
                    {
                        Destroy(instances[i]);
                    }
                    return _instance = instances[0];
                }
                /*Lazy method*/
                Debug.Log($"[{nameof(Singleton)}<{typeof(T)}>] An instance is needed in the scene and no existing instances were found, so a new instance will be created.");
                return _instance = new GameObject($"({nameof(Singleton)}){typeof(T)}")
                           .AddComponent<T>();
            }
        }
    }
    #endregion
}

public abstract class Singleton : MonoBehaviour
{
    #region Properties
    public static bool Quitting { get; private set; }
    #endregion

    #region Methods
    private void OnApplicationQuit()
    {
        Quitting = true;
    }
    #endregion
}