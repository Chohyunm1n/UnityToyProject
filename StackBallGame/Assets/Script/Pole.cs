using UnityEngine;

public class Pole : MonoBehaviour
{

    //��� ���Ƿ� ������ ���� �� �� ���� ������ ���̰� �ϴµ�
    //������ ������  GameController.Awake ���� �����ϱ� ������
    // ���� �������� ������ ������ Start �޼ҵ忡�� �߰� ����



    private void Start()
    {
        GetComponent<MeshRenderer>().material.color += Color.gray;
    }
}
