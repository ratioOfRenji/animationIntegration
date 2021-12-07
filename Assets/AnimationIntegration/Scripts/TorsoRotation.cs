using UnityEngine;

public class TorsoRotation : MonoBehaviour
{
   
    //�����, ������� �� ������������
    public Transform targetBone;
    public GameObject player;
    private Vector3 playerRot;
    [SerializeField]
    private Animator _animator;
    public void LateUpdate() //������ ���������� � LateUpdate, ����� �������� �� ������ �� ������� �����
    {
        playerRot = player.transform.localEulerAngles;

     
        handleRotationInput();
       
        
    }


    //������� �� ������ � ������� ������� ����. ����� ������������� ���� ����� �������� ����� � ������������ �� ������� ����� � ������� ����.
    // ����� ��������� ���� ���������� � localEulerAngles ����� �� ��� x � �������� � ���������� �� ��������, ������� ������ �������.
    void handleRotationInput() 
    {
        //������� ������� ���� � ������ ��������
        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out _hit))
        {
            //������������ ����������� ����� �������� ���� � �������� �����
            Vector3 targetDir =_hit.point - player.transform.position  ;
            //����������� ���� ����� ����
            float angle = Vector3.SignedAngle(player.transform.position, targetDir, Vector3.up);

            //������ ����������� ���� ����� 0, 365, ������ -180, 180
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

           
            //������������ �������� ����������� ���� � localEulerAngles �����
            targetBone.localEulerAngles = new Vector3(angle + playerRot.y, targetBone.rotation.y, targetBone.rotation.z);

        }
    }
   
}
