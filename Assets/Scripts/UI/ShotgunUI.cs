using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShotgunUI : MonoBehaviour
{
    [SerializeField] private Sprite[] _ChamberSprites;
    private Image _ShotgunChamber;
    private int _CurrentShots;

    private void Start()
    {
        _ShotgunChamber = GetComponent<Image>();
        _CurrentShots = 2;

        if (_ShotgunChamber == null) Debug.LogError("No sprite found for Lives UI Component.");
    }

    public void UpdateChamber(int clipSize)
    {
        if (clipSize > _ChamberSprites.Length) return;

        _ShotgunChamber.sprite = _ChamberSprites[clipSize];
    }

    public void FireShot()
    {
        if (_CurrentShots - 1 < 0) return;
        _CurrentShots--;
        UpdateChamber(_CurrentShots);
    }

    public void Reload()
    {
        _CurrentShots = 2;
        UpdateChamber(2);
    }
}