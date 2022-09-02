using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapIniter
{
    public static map[] mapInit()
    {
        map[] res = new map[1];
        res[0] = new map(3,3);
        token[] players = genPlayer();
        res[0].addToken(players[0],1,0);
        res[0].addToken(players[1],2,0);
        res[0].addToken(genMonsterSlime(), 0, 2);
        return res;
    }

    public static token[] genPlayer()
    {
        token[] res = new token[2];
        token t1 = new token();
        t1.Type = 0;
        t1.hp = 10;
        t1.att = 3;
        t1.def = 1;
        t1.player = true;
        t1.curXP = 0;
        t1.maxXP = 10;
        t1.level = 1;
        token t2 = new token();
        t2.Type = 1;
        t2.hp = 13;
        t2.att = 3;
        t2.def = 2;
        t2.player = true;
        t1.curXP = 0;
        t1.maxXP = 100;
        t1.level = 3;
        res[0] = t1;
        res[1] = t2;

        return res;

    }

    public static token genMonsterSlime()
    {
        token res = new token();
        res.Type = 2;
        res.hp = 5;
        res.attactType = 0;
        res.def = 0;
        res.speed = 0;
        res.player = false;
        return res;
    }
}
public class token
{
    public int Type;
    public int x;
    public int y;
    public int hp;
    public int attactType;
    public int att;
    public int def;
    public int speed;
    public bool player;
    public int level;
    public int curXP;
    public int maxXP;
    public bool reverse = false;
    public bool counter = false;
    public map map;

    public int moveTo(int x1, int y1)
    {
        return map.MoveToken(x, y, x1, y1);
    }




}