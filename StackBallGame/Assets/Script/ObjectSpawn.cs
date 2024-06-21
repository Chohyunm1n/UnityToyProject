using Unity.VisualScripting;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{

    //������Ʈ ������ ��� ���� ���� �ʿ�
    [SerializeField]
    private ObjectData[] allStackObect;
    [SerializeField]
    private Transform lastStackObject;

    public int SpawnObject()
    {
        var stackObjects = SelectStackObject();

        var objectCount = SetupStackObjectCount();

        var index = SetupStartAndEndIndex(stackObjects);

        for (int i = 0; i < objectCount; i++)
        {
            var stackObject = Instantiate(stackObjects[Random.Range(index.Item1, index.Item2)]);

            stackObject.position = new Vector3 (0, -i * 0.5f, 0);
            stackObject.eulerAngles = new Vector3(0, -i *5, 0);

            stackObject.SetParent(transform);

        }

        lastStackObject.position = new Vector3(0, -objectCount * 0.5f, 0);
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


    private (int, int) SetupStartAndEndIndex(Transform[] transform)
    {
        int level = PlayerPrefs.GetInt("level");

        float startDuraction = 0.05f;
        float endDuraction = 0.1f;


        int startIndex = Mathf.Min((int)(level * startDuraction), transform.Length-1);
        int endIndex = Mathf.Min((int)(level * endDuraction) + 2, transform.Length);

        return (startIndex, endIndex);
    }


    // ����ü // ������ ������Ʈ ������
    [System.Serializable]
    public struct ObjectData
    {
        public Transform[] objectStack;
    }

}
