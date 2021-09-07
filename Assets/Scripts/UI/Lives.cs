using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    [SerializeField] private Sprite[] _LiveSprites;
    private Image _LivesImage;

    private void Start()
    {
        _LivesImage = GetComponent<Image>();

        if (_LivesImage == null) Debug.LogError("No sprite found for Lives UI Component.");
    }

    public void UpdateLives(int lives)
    {
        if (lives > _LiveSprites.Length) return;

        _LivesImage.sprite = _LiveSprites[lives];
    }
}