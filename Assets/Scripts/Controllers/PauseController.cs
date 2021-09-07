using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private GameObject Player;
    public bool _GamePaused { get; set; }

    // Start is called before the first frame update
    private void Awake() {
        _GamePaused = false;  
    }

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        PauseTimeInputs();
        PausePlayerInputs();
        if(_GamePaused){
            Time.timeScale = 0;
        }else{
            Time.timeScale = 1;
        }
    }

    private void PauseTimeInputs(){
        if(Input.GetKeyDown(KeyCode.P)){
            _GamePaused = !_GamePaused;
        }
    }
    
    private void PausePlayerInputs(){
        
        if(_GamePaused){
            Player.GetComponent<Character>().DeactivateCharacter();
        }else{
            Player.GetComponent<Character>().ReactivateCharacter();
        }
    }
}
