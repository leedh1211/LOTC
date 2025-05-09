using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                // �ش� ������Ʈ�� ������ �ִ� ���� ������Ʈ ��ȯ
                instance = (T)FindAnyObjectByType(typeof(T));

                if (instance == null)
                {
                    // ���ο� ���� ������Ʈ ���� �� ������Ʈ �߰�
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                    // ���ο� ������Ʈ�� ������Ʈ �ν��Ͻ��� ����
                    instance = obj.GetComponent<T>();
                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        // �ν��Ͻ��� ���� �� �ش� ������Ʈ�� ����
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }

        // �ν��Ͻ��� �����Ѵٸ� ���� ������Ʈ �ı�
        else if (instance != null)
            Destroy(gameObject);
    }
}
