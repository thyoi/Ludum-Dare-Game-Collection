using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueIniter
{
    public static int linesNum = 49;
    public static void checkData(string s)
    {
        string[] db = s.Split('`');
        for (int i = 1; i < db.Length; i++)
        {
            if (i != UF.stringToInt(db[i].Split('\n')[0].Split(' ')[0]))
            {
                UF.print("index wrong at " + UF.stringToInt(db[i].Split('\n')[0].Split(' ')[0]));
            }
        }
        UF.print("" + (db.Length - 1) + "block total");
    }

    public static dialogueTree[] genDialogueTreesFromString(string s)
    {

        string[] db = s.Split('`');
        dialogueBlock[] dbl = new dialogueBlock[s.Length - 1];
        dialogueTree[] dtt = new dialogueTree[100];
        checkData(s);





        for(int i = 0;i<s.Length-1; i++)
        {
            dbl[i] = new dialogueBlock();
        }
        int dtc = 0;

        for (int i = 1; i < db.Length; i++)
        {
            string[] nMark = db[i].Split('\n')[0].Split(' ');
            if (nMark.Length > 1)
            {
                dtt[dtc] = new dialogueTree();
                dtt[dtc].name = UF.slice(nMark[1],0,-2);
                dtt[dtc].head = dbl[i - 1];
                dtc++;
            }
            genDialogueBlockFromString(db[i],i-1,dbl);
        }

        dialogueTree[] res = new dialogueTree[dtc];
        for(int i = 0; i < dtc; i++)
        {
            res[i] = dtt[i];
            if (res[i].head.frontBlock != null)
            {
                res[i].head.frontBlock.nextBlocks = null;
                res[i].head.frontBlock = null;
            }
        }

        return res;
    }

    public static void genDialogueBlockFromString(string s,int n,dialogueBlock[] dbl)
    {
        string[] lines = s.Split('\n');
        InstructionReturn ir = InstructionReader.readInstruction(lines[1]);
        if(ir.name == null)
        {
            lines = UF.slice(lines, 1);
        }
        else if(ir.name != "m")
        {
            ir = new InstructionReturn();
            lines = UF.slice(lines, 2);
        }
        else
        {
            lines = UF.slice(lines, 2);
        }
        lines = UF.noVoidEnd(lines);


        genBlockFromIns(ir, lines, n, dbl);

        if (ir.hasParameter("doubleContent"))
        {
            genDoubleBlockFromIns(ir, lines, n, dbl);
        }
        else 
        {
            genSingleBlockFromIns(ir, lines, n, dbl);
        }
        

        if(ir.hasParameter("c") || ir.hasParameter("nc"))
        {
            genChooseBlockFromIns(ir, lines, n, dbl);
        }
        else
        {
            genNormalBlockFromIns(ir, lines, n, dbl);
        }

    }

    public static void genBlockFromIns(InstructionReturn ir, string[] lines, int n, dialogueBlock[] dbl)
    {
        if(ir.hasParameter("f") && !ir.hasParameter("v"))
        {
            ir.setParameter("v", ir.getParameter("f"));
        }


        setAr("sp", ir, n, dbl);
        setAr("f", ir, n, dbl);
        setAr("v", ir, n, dbl);
        dbl[n].endMessage = "" + (n+1);

    }

    public static void setAr(string ar, InstructionReturn ir, int n, dialogueBlock[] dbl)
    {
        if (ir.hasParameter(ar))
        {
            dbl[n].setArr(ar, ir.getParameter(ar));
        }

    }
    public static void genDoubleBlockFromIns(InstructionReturn ir, string[] lines, int n, dialogueBlock[] dbl)
    {
        int sp = lines.Length;
        for(int i = 0; i < lines.Length; i++)
        {
            if(lines[i] == "||")
            {
                sp = i;
                break;
            }
        }
        dbl[n].content = new string[2];
        dbl[n].content[0] = "";
        dbl[n].content[1] = "";
        for(int i = 0; i < sp; i++)
        {
            dbl[n].content[0] += lines[i];
            dbl[n].content[0] += "]";
        }

        for(int i = sp + 1; i < lines.Length; i++)
        {
            dbl[n].content[1] += lines[i];
            dbl[n].content[1] += "]";
        }



    }
    public static void genSingleBlockFromIns(InstructionReturn ir, string[] lines, int n, dialogueBlock[] dbl)
    {
        dbl[n].content = new string[1];
        dbl[n].content[0] = stringListToSting(lines);
    }

    public static string stringListToSting(string[] ss)
    {
        string res = "";
        foreach(string sss in ss)
        {
            res += sss;
            res += " ";
        }
        res = UF.slice(res, 0, -2);
        res.Replace("  ", " ");
        res.Replace("\r", "");
        string[] tem = res.Split(' ');
        int tn = 0;
        res = "";
        foreach(string sss in tem)
        {
            if(tn + sss.Length > linesNum)
            {
                res += "]" + sss+" ";
                tn = sss.Length + 1;
            }
            else if(tn + sss.Length == linesNum)
            {
                res += sss + "]";
                tn = 0;
            }
            else
            {
                res += sss + " ";
                tn += sss.Length + 1;
            }
        }


        return res;
    }
    public static void genNormalBlockFromIns(InstructionReturn ir,string[] lines, int n, dialogueBlock[] dbl)
    {
        if (ir.hasParameter("e"))
        {
            string en = ir.getParameter("e");
            if (en == "end")
            {
                dbl[n].nextBlocks = null;
            }
            else
            {
                dbl[n].nextBlocks = new dialogueBlock[1];
                dbl[n].nextBlocks[0] = dbl[UF.stringToInt(en) - 1];
                dbl[UF.stringToInt(en) - 1].frontBlock = dbl[n];
            }

        }
        else
        {
            dbl[n].nextBlocks = new dialogueBlock[1];
            dbl[n].nextBlocks[0] = dbl[n + 1];
            dbl[n + 1].frontBlock = dbl[n];
        }
    }

    public static void genChooseBlockFromIns(InstructionReturn ir, string[] lines, int n, dialogueBlock[] dbl)
    {
        if (ir.hasParameter("c"))
        {
            dbl[n].Choose = true;
        }
        else if (ir.hasParameter("nc"))
        {
            dbl[n].nagtiveChoose = ir.getParameter("nc");
        }

        if (ir.hasParameter("e"))
        {
            string[] en = ir.getParameter("e").Split(',');
            if (en[0] == "end")
            {
                dbl[n].nextBlocks = null;
            }
            else
            {
                dbl[n].nextBlocks = new dialogueBlock[en.Length];
                for (int i = 0; i < en.Length; i++)
                {
                    dbl[n].nextBlocks[i] = dbl[UF.stringToInt(en[i]) - 1];
                    dbl[UF.stringToInt(en[i]) - 1].frontBlock = dbl[n];
                }
            }
        }
        else
        {
            int tn = UF.stringToInt(ir.getParameter("cn"));
            dbl[n].nextBlocks = new dialogueBlock[tn];
            for(int i = 0; i < tn; i++)
            {
                dbl[n].nextBlocks[i] = dbl[n + 1];
                dbl[n + 1].frontBlock = dbl[n];
            }
        }

    }


}


public class dialogueBlock
{
    public dialogueBlock[] nextBlocks = null;
    public dialogueBlock frontBlock = null;
    public bool Choose = false;
    public string nagtiveChoose = null;
    public int ChooseNum = 1;
    public string[] content;
    public bool skip = true;
    public bool doubleContent = false;
    public string[] img;
    public string[] speed;
    public string[] voice;
    public string endMessage;
    public dialogueMeta[] meta = null;

    public dialogueBlock()
    {
        img = new string[1];
        img[0] = "none";
        speed = new string[1];
        speed[0] = "1";
        voice = new string[1];
        voice[0] = "none";
    }
    public void setArr(string ar,string con)
    {
        string[] tar;
        switch (ar)
        {
            case ("sp"):tar = speed;break;
            case ("f"):tar = img;break;
            case ("v"):tar = voice;break;
        }
        string[] spCom = con.Split(',');
        tar = new string[spCom.Length];
        for(int i = 0; i < spCom.Length; i++)
        {
            tar[i] = spCom[i];
        }
    }

}

public class dialogueMeta
{
    public string name;
    public int start;
    public int end;
}


public class dialogueTree
{
    public bool breakable = false;
    public string name;
    public dialogueBlock head;
    public dialogueBlock current;

    public dialogueBlock next(int n = 0)
    {
        if (current.nextBlocks != null)
        {
            current = current.nextBlocks[n];
            return current;
        }
        else
        {
            return null;
        }
    }
}