using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    // 싱글톤 패턴
    static Managers s_instance; // static으로 인해 유일성이 보장된다(Manager는 1개여야 하므로)
    static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 가지고 온다

    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    public static InputManager Input { get { return Instance._input; } } // InputManager를 사용하고 싶다면 Managers.Input
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
            GameObject go = GameObject.Find("@Managers"); // Managers Object를 불러옴
            if (go == null)
            {
                go = new GameObject { name = "@Managers" }; // Create Empty와 동일
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go); // 웬만해선 삭제 안되게 만듦
            s_instance = go.GetComponent<Managers>(); // Managers Script를 불러옴
        }
    }
}
