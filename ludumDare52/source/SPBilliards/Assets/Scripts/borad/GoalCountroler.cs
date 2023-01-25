using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCountroler : MonoBehaviour
{
    public DialogManager DiaManager;
    public bool PointMode;
    public bool SPBallMode;
    public int ScroeCount;
    public AnimeCountroler ac;
    public SpriteRenderer border;
    public string FString;
    public string BString;
    public BallCounter bc;

    public void Bomm(float size)
    {
        ac.ScaleX.Init = size;
        ac.ScaleY.Init = size;
        ac.StartAnime();
    }
    public void InitPointCount(int Scroe)
    {
        PointMode = true;
        SPBallMode = false;
        ScroeCount = bc.GetNextScore();

        DiaManager.ShowContent("Get " + ScroeCount + " Point to continue");
    }

    public void GetPoint(int p,Color c)
    {
        if (PointMode)
        {
            Bomm(1.1f + p * 0.1f);
            border.color = c;
            particalManager.GlobalManager.BoomParticalBust(20 * p, transform.position, c, 1.4f + p * 0.2f, true);
            ScroeCount -= p;
            DiaManager.DefaultColor = c;
            DiaManager.ShowContent("Get " + ScroeCount + " Point to continue");
        }
    }

    public void InitSPBall(int c)
    {
        PointMode = false;
        SPBallMode = true;
        ScroeCount  = c;

        DiaManager.ShowContent("knock in " + ScroeCount + " Spcial Ball");
    }

    public void GetSPBall(int p, Color c)
    {
        if (SPBallMode)
        {
            Bomm(1.1f + p * 0.1f);
            border.color = c;
            particalManager.GlobalManager.BoomParticalBust(20 * p, transform.position, c, 1.4f + p * 0.2f, true);
            ScroeCount -= 1;
            DiaManager.DefaultColor = c;
            DiaManager.ShowContent("knock in " + ScroeCount + " Spcial Ball");
        }
    }

    public void SetSpell(string n,Color c)
    {
        PointMode = false;
        SPBallMode = false;
        DiaManager.DefaultColor = c;
        DiaManager.ShowContent(n);
    }

}
