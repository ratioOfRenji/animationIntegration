using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        //�������� input � wasd
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //�������� input � ������
        Vector3 movementspeed = new Vector3(-horizontal, 0, -vertical);
        //vector3.normalized ����� �� ��������, �� ����� �� ����� �������� ���������� � ����� ����� ��� ��� ������� ����� ��������� ���������
        //� ������ ������ ����� ������ �������� ��� �������� �� 0.7, ���� ������������ ������ 2 ������. ����� ��������� ������ ����� ����������, � ����������� ���������/��������� ���������.
        if (horizontal != 0 && vertical != 0)
            movementspeed = movementspeed *.7f;
        //�������� ������ � ��������
        transform.Translate(Speed * movementspeed * Time.deltaTime, Space.World);

    }
}
