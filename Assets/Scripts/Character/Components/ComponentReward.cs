using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentReward : MonoBehaviour
{
    private bool _RewardIssued = false;
    private bool _Reward = false;
    [SerializeField] List<Collectable> RewardsList;
    private Character _Character;

    private void Start() {
        _Character = GetComponent<Character>();
    }
    
    private void Update() {
        if(!_Character.IsAlive){_Reward = true;}
        
        if(!_Character.IsAlive && _RewardIssued == false && _Reward == true){
            _RewardIssued = true;
            Reward();
        }
    }
    
    public void IssueReward(){
        _Reward = true;
    }

    private void Reward(){
        // WOULD BE BETTER TO USE A BOOL FOR AUTO REWARDS

        // Look at Rewards list
        if(RewardsList.Count == 1){
            Instantiate(RewardsList[0], transform.position + new Vector3(0, 1.5f, 0), transform.rotation);
        }
        if(RewardsList.Count > 1){
            // Choose between items
            // 50% no item
            // The other 50% is which item

            var randNum = Random.Range(0, 100);

            if(randNum > 55){
                // Item is rewarded
                var randNum2 = Random.Range(0, RewardsList.Count);
                Debug.Log(randNum2);
                Instantiate(RewardsList[randNum2], transform.position + new Vector3(0, 1.5f, 0), transform.rotation);
            }
        }else{
            return;
        }
    }

}
