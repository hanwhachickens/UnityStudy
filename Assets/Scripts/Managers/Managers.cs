using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    // �̱��� ����
    static Managers s_instance; // static���� ���� ���ϼ��� ����ȴ�(Manager�� 1������ �ϹǷ�)
    static Managers Instance { get { Init(); return s_instance; } } // ������ �Ŵ����� ������ �´�

    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    public static InputManager Input { get { return Instance._input; } } // InputManager�� ����ϰ� �ʹٸ� Managers.Input
    public static ResourceManager Resource { get { return Instance._resource; } }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers"); // Managers Object�� �ҷ���
            if (go == null)
            {
                go = new GameObject { name = "@Managers" }; // Create Empty�� ����
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go); // �����ؼ� ���� �ȵǰ� ����
            s_instance = go.GetComponent<Managers>(); // Managers Script�� �ҷ���
        }
    }
}
