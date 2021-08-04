using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    // might need to be updated when PlayerController script is no longer in use
    [SerializeField] PlayerController _player;

    private Text _info;
    private bool _hiddenInfo = true;

    void Start()
    {
        _info = GetComponent<Text>();
        if (!_info) print("PlayerInfo UI object unable to find Text component.");
        if (!_player) print("PlayerInfo Text Box unable to assign _player to a PlayerController.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_hiddenInfo)
            {
                _hiddenInfo = false;
            }
            else
            {
                _hiddenInfo = true;
                _info.text = "Press [ESC] to show info";
            }
        }

        if (_hiddenInfo) return;
        
        // TODO: setup logic to call updateinfo only when a change in state for any particular stat occurs
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        string info = "";
        info += "speed: " + _player.Speed; 
        info += "\njumpPower: " + _player.JumpPower;
        info += "\ngravity: " + _player.Gravity;
        info += "\n\nisGrounded: " + _player.IsGrounded;
        info += "\nisMoving: " + _player.IsMoving;
        info += "\nisAttacking: " + _player.IsAttacking;
        info += "\nfacingRight: " + _player.FacingRight;
        info += "\ntakingDamage: " + _player.TakingDamage;
        info += "\nisDead: " + _player.IsDead;
        info += "\ncombo: " + _player.Combo ;
        info += "\ncurrent animation: " + _player.CurrentAnimation;
        _info.text = info;
    }
}
