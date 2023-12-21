using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonTank : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] _summonList;
    void Start()
    {
        _summonList = GetComponent<BehaviourTreeRunner>().GetSummonList();
    }

    // Update is called once per frame
    public void SummonRandom(Vector3 pos)
    {
        int random = Random.Range(0, _summonList.Length);
        Instantiate(_summonList[random], pos, Quaternion.identity );
    }
}
