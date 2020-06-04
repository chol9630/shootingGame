using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;         //string 값으로 비교
        public GameObject preFabs; //프리팹넣기
        public int size;           //몇개까지 만들지   사이즈가 초과되면 어떻게 할건지
        
    }
    

    public List<Pool> listPool;

    public Dictionary<string, Queue<GameObject>> DictionaryPool;

    public static ObjectPool Instance;
    
    private void Awake()
    {
        if(Instance ==null)
        {
            Instance = this;
        }
    }
    
    

    // Start is called before the first frame update
    void Start()
    {

        DictionaryPool = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in listPool)
        {
            Queue<GameObject> objectsPool = new Queue<GameObject>();

            for(int i =0; i<pool.size; i++)
            {
                GameObject go = Instantiate(pool.preFabs);
                go.SetActive(false);
                objectsPool.Enqueue(go);
            }

            DictionaryPool.Add(pool.tag, objectsPool);

        }
    }
   
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotaion)
    {


        if(!DictionaryPool.ContainsKey(tag))
        {
            return null;
        }

        
        //해당 테그 풀의 오브젝트 꺼내서 트루로 만들어주는건데, 
        //Dequeue 한 오브젝트가 이미 켜져있다면 새로 만들어서 풀에 넣어준다.

        GameObject objectFrompool = DictionaryPool[tag].Dequeue();

        if (objectFrompool.activeInHierarchy == true) //나오면서 이미켜져있으면 
        {
            DictionaryPool[tag].Enqueue(objectFrompool); //켜져있는건 다시 넣어주고
            objectFrompool = Instantiate(objectFrompool); //프리팹을 하나만든다
                                
        }
        


        objectFrompool.SetActive(true);
        objectFrompool.transform.position = position;
        objectFrompool.transform.rotation = rotaion;

        DictionaryPool[tag].Enqueue(objectFrompool);

        return objectFrompool;
    }


    // Update is called once per frame

   

}
