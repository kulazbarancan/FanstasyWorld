using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
            }

            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            gameObject.transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
            return;
        }

        if (_instance != this as T)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}