using UnityEngine;

public class TorsoRotation : MonoBehaviour
{
   
    //кость, которую мы поворачиваем
    public Transform targetBone;
    public GameObject player;
    private Vector3 playerRot;
    [SerializeField]
    private Animator _animator;
    public void LateUpdate() //методы вызываются в LateUpdate, чтобы анимации не влияли на поворот кости
    {
        playerRot = player.transform.localEulerAngles;

     
        handleRotationInput();
       
        
    }


    //рэйкаст из камеры к позиции курсора мыши. далше высчитывается угол между позицией кости и направлением от позиции кости к позиции мыши.
    // далее полученый угол прибавляем в localEulerAngles кости по оси x с оффсетом в зависимоти от анимации, которая сейчас активна.
    void handleRotationInput() 
    {
        //находим позицию мыши с поощью рэйкаста
        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out _hit))
        {
            //просчитываем направление между позицией мыши и позицией кости
            Vector3 targetDir =_hit.point - player.transform.position  ;
            //просчитывем угол между ними
            float angle = Vector3.SignedAngle(player.transform.position, targetDir, Vector3.up);

            //теперь возврааемый угол будет 0, 365, вместо -180, 180
            if (angle < 0)
            {
                angle = 360 - angle * -1;
            }
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Take 001"))
            {
                angle = (angle * -1) +35f;
            }
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Right-Idle"))
            {
                    angle = (angle * -1) + 80f;
            }

           
            //присваивание значения полученного угла в localEulerAngles кости
            targetBone.localEulerAngles = new Vector3(angle + playerRot.y, targetBone.rotation.y, targetBone.rotation.z);

        }
    }
   
}
