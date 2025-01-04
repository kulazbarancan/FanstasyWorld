using UnityEngine;

public class ScopedSingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    static T m_instance;

    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<T>();
            }

            return m_instance;
        }
    }

    public virtual void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this as T;
            return;
        }

        if (m_instance != this as T)
        {
            Debug.LogWarning($"2 TANE SCOPED SINGLETON {m_instance.GetType().Name} KOPYASI VAR. BURAYI KONTROL EDIN");
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning(
                $"SCOPED SINGLETON {m_instance.GetType().Name} ' KENDI AWAKE'INDEN ONCE ERISILIYOR. BURAYI KONTROL EDIN");
        }
    }
}