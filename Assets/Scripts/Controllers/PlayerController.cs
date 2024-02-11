using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // public float _speed = 10.0f; public�� ���� �� ����Ƽ���� ���� ����
    [SerializeField] // public�� �ٿ��ִ� ȿ��
    float _speed = 10.0f;

    bool _moveToDest = false; // �̵� ���
    Vector3 _destPos; // ���� ����

    void Start()
    {
        Managers.Input.KeyAction -= OnKeyboard; // �ߺ� ���� ����
        Managers.Input.KeyAction += OnKeyboard; // ���� ��û 
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }

    // GameObject (Player)
    // Transform
    // PlayerController (���� ��ġ)
    // ���� Transform�� �����ϱ� ���ؼ��� �θ��� GameObject�� ã�ƾ� ������ Transform�� ���� ���� ã�Ƽ� ����� ����(transform)

    void Update()
    {
        if (_moveToDest)
        {
            Vector3 dir = _destPos - transform.position;
            if (dir.magnitude < 0.0001f) // �����ϸ�
            {
                _moveToDest = false;
            }
            else
            {
                float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude); // _speed * Time.deltaTime�� 0���� dir.magnitude �� �������� �����ؾ� ĳ���Ͱ� ��鸮�� ����
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

        // transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f)); ȸ��

        if (Input.GetKey(KeyCode.W)) // WŰ�� ������
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f); // 0.2f��� ������ �ξ�(0�̸� ����, 1�̸� ����)������ ȸ���ϵ���
            transform.position += Vector3.forward * Time.deltaTime * _speed;
            // Quaternion.LookRotation(Vector3.forward), 0.2f); // ���븸 ������
        }
        // transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed); transform.TransformDirection�� �ٿ���� local �׷��� ������ global
        // transform.Translate(Vector3.forward * Time.deltaTime * _speed); // �ڵ� �ν�
        if (Input.GetKey(KeyCode.S)) // SŰ�� ������
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f); // 0.2f��� ������ �ξ�(0�̸� ����, 1�̸� ����)������ ȸ���ϵ���
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }
        // transform.Translate(Vector3.back * Time.deltaTime * _speed);
        if (Input.GetKey(KeyCode.A)) // AŰ�� ������
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f); // 0.2f��� ������ �ξ�(0�̸� ����, 1�̸� ����)������ ȸ���ϵ���
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
        // transform.Translate(Vector3.left * Time.deltaTime * _speed);
        if (Input.GetKey(KeyCode.D)) // DŰ�� ������
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f); // 0.2f��� ������ �ξ�(0�̸� ����, 1�̸� ����)������ ȸ���ϵ���
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
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f); // 1�� ���� ����
        // int mask = (1 << 8) | (1 << 9); // ��Ʈ����(Monster layer)
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;
            _moveToDest = true;
            //Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
        }
    }
}
