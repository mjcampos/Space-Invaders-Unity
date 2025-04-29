using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LivesAnimation : MonoBehaviour
{
    [SerializeField] Sprite[] spriteArray;

    [SerializeField] float changeSpeed = .2f;
    
    Image _image;
    int _spriteArrayLength;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        _image = GetComponent<Image>();
        _spriteArrayLength = spriteArray.Length;

        StartCoroutine(ChangeSprite());
    }

    IEnumerator ChangeSprite()
    {
        int currentSpriteIndex = 0;

        do
        {
            Sprite currentSprite = spriteArray[currentSpriteIndex];
            
            _image.sprite = currentSprite;

            yield return new WaitForSecondsRealtime(changeSpeed);

            currentSpriteIndex++;

            if (currentSpriteIndex >= _spriteArrayLength)
            {
                currentSpriteIndex = 0;
            }
        } while (true);
    }
}
