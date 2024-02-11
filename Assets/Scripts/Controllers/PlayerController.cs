using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // public float _speed = 10.0f; public을 붙일 시 유니티에서 관리 가능
    [SerializeField] // public을 붙여주는 효과
    float _speed = 10.0f;

    bool _moveToDest = false; // 이동 모드
    Vector3 _destPos; // 도착 지점

    void Start()
    {
        Managers.Input.KeyAction -= OnKeyboard; // 중복 구독 방지
        Managers.Input.KeyAction += OnKeyboard; // 구독 신청 
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }

    // GameObject (Player)
    // Transform
    // PlayerController (현재 위치)
    // 따라서 Transform에 접근하기 위해서는 부모인 GameObject를 찾아야 하지만 Transform은 워낙 자주 찾아서 기능이 있음(transform)

    void Update()
    {
        if (_moveToDest)
        {
            Vector3 dir = _destPos - transform.position;
            if (dir.magnitude < 0.0001f) // 도착하면
            {
                _moveToDest = false;
            }
            else
            {
                float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude); // _speed * Time.deltaTime은 0에서 dir.magnitude 값 사이임을 보장해야 캐릭터가 흔들리지 않음
                transform.position += dir.normalized * moveDist;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
                // transform.LookAt(_destPos);
            }
        }
    }

    void OnKeyboard()
    {
        // +- delta
        // transform.Rotate(new Vector3(0.0f, Time.deltaTime * 100.0f, 0.0f));

        // transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f)); 회전

        if (Input.GetKey(KeyCode.W)) // W키를 누르면
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f); // 0.2f라는 비중을 두어(0이면 전자, 1이면 후자)스스륵 회전하도록
            transform.position += Vector3.forward * Time.deltaTime * _speed;
            // Quaternion.LookRotation(Vector3.forward), 0.2f); // 몸통만 앞으로
        }
        // transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed); transform.TransformDirection을 붙여줘야 local 그렇지 않으면 global
        // transform.Translate(Vector3.forward * Time.deltaTime * _speed); // 자동 인식
        if (Input.GetKey(KeyCode.S)) // S키를 누르면
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f); // 0.2f라는 비중을 두어(0이면 전자, 1이면 후자)스스륵 회전하도록
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }
        // transform.Translate(Vector3.back * Time.deltaTime * _speed);
        if (Input.GetKey(KeyCode.A)) // A키를 누르면
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f); // 0.2f라는 비중을 두어(0이면 전자, 1이면 후자)스스륵 회전하도록
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
        // transform.Translate(Vector3.left * Time.deltaTime * _speed);
        if (Input.GetKey(KeyCode.D)) // D키를 누르면
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f); // 0.2f라는 비중을 두어(0이면 전자, 1이면 후자)스스륵 회전하도록
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
        // transform.Translate(Vector3.right * Time.deltaTime * _speed);

        _moveToDest = false;
    }

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (evt != Define.MouseEvent.Click)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f); // 1초 동안 유지
        // int mask = (1 << 8) | (1 << 9); // 비트연산(Monster layer)
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;
            _moveToDest = true;
            //Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
        }
    }
}
