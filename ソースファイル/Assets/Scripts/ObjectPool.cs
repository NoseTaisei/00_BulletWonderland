using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField]
    private GameObject[] playerPoolObj;

    [SerializeField]
    private GameObject[] enemyPoolObj;

    [SerializeField]
    private Transform[] objBox; // プールが格納されるオブジェクト

    private Vector2 pos = new Vector2(1000, 1000);　// 生成される場所

    private List<GameObject>[] poolObjList = new List<GameObject>[4];

    private const int maxCount = 100; // 最初に生成する弾の数


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            foreach(GameObject bullet in playerPoolObj) 
            {
                CreatePool(0, bullet.tag); 
            }
            foreach(GameObject bullet in enemyPoolObj)
            {
                CreatePool(1, bullet.tag);
            }
        }   
    }

    private void CreatePool(int x,string type)
    {
        poolObjList[x] = new List<GameObject>();
        for(int i = 0; i < maxCount; i++)
        {
            var newObj = CreateNewBullet(x,type);
            newObj.GetComponent<Rigidbody2D>().simulated = false; // 物理演算を切る
            poolObjList[x].Add(newObj);
        }
    }
    
    public GameObject GetBullet(int x,string type)
    {
        // 指定した弾で使用中でないものを探して返す
        foreach (var obj in poolObjList[x])
        {
            var objrb = obj.GetComponent<Rigidbody2D>();
            if (objrb.simulated == false && obj.tag == type)
            {
                obj.SetActive(true);
                objrb.simulated = true;
                
                return obj;
            }          
        }

        // 全て使用中だったら新しく作り、リストに追加してから返す
        var newObj = CreateNewBullet(x,type);
        poolObjList[x].Add(newObj);
        newObj.SetActive(true);
        newObj.GetComponent<Rigidbody2D>().simulated = true;
        return newObj;
    }
    public void OnReturnedToPool(GameObject bullet)　// プールに戻す
    {
        bullet.SetActive(false);
        bullet.transform.position = pos;
    }

    private GameObject CreateNewBullet(int x,string type)
    {
        GameObject[] poolObj;
        if(x == 0)
        {
            poolObj = playerPoolObj;
        }
        else
        {
            poolObj = enemyPoolObj;
        }
        var newObj = Instantiate(poolObj.ToList().Find(b => b.tag == type), pos, transform.rotation);

        newObj.transform.SetParent(objBox[x]); // 指定の子オブジェクトにいれる　

        newObj.name = poolObj.ToList().Find(b => b.tag == type).name + (poolObjList[x].Count + 1); // 名前をつける
        newObj.SetActive(false);

        return newObj;
    }
    
}
