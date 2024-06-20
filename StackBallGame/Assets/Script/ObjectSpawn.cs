using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{

    //������Ʈ ������ ��� ���� ���� �ʿ�
    public ObjectData[] allStackObect;


    //TODO: ������Ʈ�� ���� ���� �� ��ġ
    //������Ʈ ������ �� ������ ���� ���̵� �߰�
    public int SpawnObject()
    {
        var stackObjects = SelectStackObject();

        var objectCount = SetupStackObjectCount();

        return objectCount;
    }
    

    //��������  ������Ʈ ���� ����
    public Transform[] SelectStackObject()
    {
        int objectIndex = Random.Range(0, allStackObect.Length);

        Transform[] selectStackObjectp = new Transform[allStackObect[objectIndex].objectStack.Length];

        for (int i = 0; i < allStackObect[objectIndex].objectStack.Length; i++)
        {
            selectStackObjectp[i] = allStackObect[objectIndex].objectStack[i];
        }

        return selectStackObjectp;
    }



    //������ ���� ������ ������Ʈ ���� ����

    public int SetupStackObjectCount()
    {
        int level = PlayerPrefs.GetInt("level");

        int baseCount = 10;

        if (level % 5 == 0)
        {
            baseCount += 5;
        }

        int StackObjectCount = baseCount + level;

        return StackObjectCount;
    }





    // ����ü // ������ ������Ʈ ������
    public struct ObjectData
    {
        public Transform[] objectStack;
    }

}
