using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object // Prefab�� �ҷ���
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null) // Prefab�� �Ҵ���
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        return Object.Instantiate(prefab, parent);
    }

    public void Destory(GameObject go) // Prefab�� ������
    {
        if (go == null)
        {
            return;
        }

        Object.Destroy(go);
    }
}
