using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueManager : MonoBehaviour
{

    public float wordTime;
    private float wordTimeCount;
    public float boxTime;
    private float boxTimeCount;
    public int stage;
    public dialogueTree[] dialogueTreeList;
    public bool onDialugue =false;
    public dialogueTree curentDialogue = null;
    public IOManager iom;
    public InputDataManager idm;
    public textManager textManager;
    public dialogueBox dialogueBox;
    public float dt;
    public string textShow;
    public int textCount;
    public main Manager;

    // Start is called before the first frame update
    void Start()
    {
        initDialogueTreeList();
        foreach(dialogueTree d in dialogueTreeList)
        {
            UF.print(d.name);
        }
        //UF.print(dialogueTreeList[0].)
        //startDialogues("start");
    }

    public void initDialogueTreeList()
    {
        dialogueTreeList = dialogueIniter.genDialogueTreesFromString(iom.readStringFromData(0));       
    }


    public void startDialogues(string s)
    {
        UF.print(s);
        for(int i = 0; i < dialogueTreeList.Length; i++)
        {
            if(dialogueTreeList[i].name == s)
            {
                curentDialogue = dialogueTreeList[i];
                startDialogues();
                return;
            }
        }
    }
    public void startDialogues()
    {
        if (!onDialugue)
        {
            stage = 0;
            boxTimeCount = 0;
            onDialugue = true;
            startText();
            closeAssable();
            curentDialogue.current = curentDialogue.head;
        }
    }

    public void startText()
    {
        textManager.setContent("", true);
        wordTimeCount = 0;
        textCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        dt = Time.deltaTime;
        if (onDialugue)
        {
            switch (stage)
            {
                case (0):dStart();break;
                case (1):readWord();break;
                case (2):waitClick();break;
                case (100):dEnd();break;
            }
        }
    }


    public void closeAssable()
    {
        idm.closeLayer(0);
    }

    public void openAssable()
    {
        idm.openLayer(0);
    }
    //state0
    public void dStart()
    {
        if (boxTimeCount < boxTime)
        {
            boxTimeCount += dt;
            if (boxTimeCount > boxTime)
            {
                boxTimeCount = boxTime;
            }
            dialogueBox.setState(boxTimeCount / boxTime);
        }
        else
        {
            stage++;
        }
    }
    //state100
    public void dEnd()
    {
        if (boxTimeCount <= boxTime)
        {
            boxTimeCount += dt;
            dialogueBox.setState(1-boxTimeCount / boxTime);
        }
        else
        {
            openAssable();
            onDialugue = false;
        }
    }
    //state1
    public void readWord()
    {
        wordTimeCount += dt;
        if (wordTimeCount > wordTime)
        {
            wordTimeCount = 0;
            textCount++;

            if (textCount > curentDialogue.current.content[0].Length)
            {
                stage++;
                return;
            }
            textShow = UF.slice(curentDialogue.current.content[0], 0, textCount);
            textManager.setContent(textShow);
        }
        if (isClick())
        {
            textManager.setContent(curentDialogue.current.content[0]);
            stage++;
            return;
        }
    }

    public bool isClick()
    {
        return (idm.mouseD(0, 0) || idm.mouseD(1, 0) || idm.keyD("space", 0) || idm.keyD("enter", 0));
    }
    //state2
    public void waitClick()
    {
        if (isClick())
        {
            nextDialogue();
        }
    }

    public void nextDialogue()
    {
        Manager.dialogueMassgae(curentDialogue.current.endMessage);
        if (curentDialogue.next() != null)
        {
            startText();
            stage = 1;
        }
        else
        {
            boxTimeCount = 0;
            stage = 100;
            textManager.setContent("", true);
        }
    }







}


