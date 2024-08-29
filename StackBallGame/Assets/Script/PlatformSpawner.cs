using Unity.VisualScripting;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

    //������Ʈ ������ ��� ���� ���� �ʿ�
    [SerializeField]
    private PlatformShape[] allPlatforms;
    [SerializeField]
    private Transform lastStackObject;

    public int SpawnObject()
    {
        var stackObjects = SelectStackObject();

        var platformCount = SetupStackObjectCount();

        var index = SetupStartAndEndIndex(stackObjects);

        for (int i = 0; i < platformCount; i++)
        {
            var platform = Instantiate(stackObjects[Random.Range(index.Item1, index.Item2)]);

            platform.position = new Vector3 (0, -i * 0.5f, 0);
            platform.eulerAngles = new Vector3(0, -i *5, 0);

            platform.SetParent(transform);

        }

        lastStackObject.position = new Vector3(0, -platformCount * 0.5f, 0);
        return platformCount;
    }
    

    //��������  ������Ʈ ���� ����
    private Transform[] SelectStackObject()
    {
        int objectIndex = Random.Range(0, allPlatforms.Length);

        Transform[] selectStackObjectp = new Transform[allPlatforms[objectIndex].platforms.Length];

        for (int i = 0; i < allPlatforms[objectIndex].platforms.Length; i++)
        {
            selectStackObjectp[i] = allPlatforms[objectIndex].platforms[i];
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


    private (int, int) SetupStartAndEndIndex(Transform[] platforms)
    {
        int level = PlayerPrefs.GetInt("level");

        float startDuraction = 0.05f;
        float endDuraction = 0.1f;


        int startIndex = Mathf.Min((int)(level * startDuraction), platforms.Length-1);
        int endIndex = Mathf.Min((int)(level * endDuraction) + 2, platforms.Length);

        return (startIndex, endIndex);
    }


    // ����ü // ������ ������Ʈ ������
    [System.Serializable]
    public struct PlatformShape
    {
        public Transform[] platforms;
    }

}
