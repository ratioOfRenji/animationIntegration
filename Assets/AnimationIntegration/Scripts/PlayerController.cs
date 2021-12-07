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
        //получаем input с wasd
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //передаем input в вектор
        Vector3 movementspeed = new Vector3(-horizontal, 0, -vertical);
        //vector3.normalized здесь не подойдет, тк тогда не будет плавного замедления и игрок будет еще пол секунды после остановки скользить
        //в данном случае можно просто умножить оба значения на 0.7, если одновременно нажаты 2 кнопки. тогда сккорость всегда будет одинаковой, а постепенное ускорение/замеление останется.
        if (horizontal != 0 && vertical != 0)
            movementspeed = movementspeed *.7f;
        //приводим игрока в движение
        transform.Translate(Speed * movementspeed * Time.deltaTime, Space.World);

    }
}
