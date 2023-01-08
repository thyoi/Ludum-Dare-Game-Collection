using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public MouseBox CharacterBox;
    public Sprite[] CharacterMouseSprites;
    public CharacterSpriteCountroler[] CharacterSpriteCountrolers;

    public void InitCharacterBox()
    {
        CharacterBox.MouseSprites = CharacterMouseSprites;
        CharacterBox.HoverFunction = (Vector2 position) => { return true; };
    }
    // Start is called before the first frame update
    void Start()
    {
        InitCharacterBox();
        CharacterBox.ClickFunction = (Vector2 v) => { CharacterSpriteCountrolers[0].Touch(); return true; };
        //CharacterBox.UpFunction = (Vector2 v) => { return true; };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
