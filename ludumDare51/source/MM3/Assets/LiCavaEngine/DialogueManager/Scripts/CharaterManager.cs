using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterManager : MonoBehaviour
{
    static public CharaterManager mainManager;
    static public Character GetCharacter(char c,int font)
    {
        return mainManager.NewCharacter(c,font);
    }
    static public float Space(int font)
    {
        return mainManager.SpaceLength(font);
    }
    static public Sprite[] GetNumSprites(int n)
    {
        Sprite[] tem = mainManager.Font1Sprites(n + 60);

        return mainManager.Font1Sprites(n + 60);
    }



    public GameObject CharacterPrefab;
    public Sprite[] Font1;
    public Sprite[] Font2;
    private int randomInit;
    private ObjectsPool<Character> characterPool = new ObjectsPool<Character>(200);



    public void Awake()
    {
        if (!(mainManager is null))
        {
            Debug.LogError("Repetitive CharacterManager");
        }
        mainManager = this;
    }
    public Sprite[] Font1Sprites(int index)
    {
        switch (randomInit)
        {
            case (0): randomInit++; return new Sprite[] { Font1[index], Font1[index + 70], Font1[index + 140] };
            case (1): randomInit++; return new Sprite[] { Font1[index+140], Font1[index], Font1[index + 70] };
            default: randomInit=0; return new Sprite[] { Font1[index + 70], Font1[index+140], Font1[index] };
        }
        
    } 
    public Sprite[] Font2Sprites(int index)
    {
        switch (randomInit)
        {
            case (0): randomInit++; return new Sprite[] { Font2[index], Font2[index + 36], Font2[index + 72] };
            case (1): randomInit++; return new Sprite[] { Font2[index + 72], Font2[index], Font2[index + 36] };
            default: randomInit = 0; return new Sprite[] { Font2[index + 36], Font2[index + 72], Font2[index] };
        }
    }

    public float SpaceLength(int font = 1)
    {
        switch (font)
        {
            case (1):return Font1[55].rect.width * UF.PixelLength;
            case (2):return Font2[26].rect.width * UF.PixelLength;
            default: return Font1[55].rect.width * UF.PixelLength;
        }
    }

    public Sprite[] GetSpritesFromChar(char c)
    {
        if (LettersData.CharIsLowercase(c))
        {
            return Font1Sprites(c - 'a');
        }
        else if (LettersData.CharIsUppercase(c))
        {
            return Font1Sprites(c - 'A' + 26);
        }
        else if (LettersData.CharIsNum(c))
        {
            return Font1Sprites(c - '0' + 60);
        }
        else if(c == ':')
        {
            return Font1Sprites(52);
        }
        else if (c == '.')
        {
            return Font1Sprites(53);
        }
        else if (c == ',')
        {
            return Font1Sprites(54);
        }
        else if (c == '?')
        {
            return Font1Sprites(55);
        }
        else if (c == '!')
        {
            return Font1Sprites(56);
        }
        else if (c == '(')
        {
            return Font1Sprites(57);
        }
        else if (c == ')')
        {
            return Font1Sprites(58);
        }
        else if (c == '=')
        {
            return Font1Sprites(59);
        }
        else
        {
            return Font1Sprites(52);
        }
    }


    public Sprite[] GetLargeSpritesFromChar(char c)
    {
        if (LettersData.CharIsUppercase(c))
        {
            return Font2Sprites(c - 'A' );
        }
        else if (LettersData.CharIsNum(c))
        {
            return Font2Sprites(c - '0' + 26);
        }
        else
        {
            return Font2Sprites(26);
        }
    }
    public Character NewCharacter(char c,int font = 1)
    {
        Character tem;
        if(characterPool.CanGetItem())
        { 
            tem = characterPool.GetItem();

        }
        else
        {
            tem = Instantiate(CharacterPrefab, transform).transform.GetComponent<Character>();
        }
        switch (font)
        {
            case (1): tem.SetSprites(GetSpritesFromChar(c));
                break;
            case (2): tem.SetSprites(GetLargeSpritesFromChar(c));
                break;
            default:tem.SetSprites(GetSpritesFromChar(c));
                break;
        }
        tem.SetPool(characterPool);
        return tem;
    }

}
