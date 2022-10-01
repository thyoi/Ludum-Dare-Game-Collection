using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class TesterValueCoutroler
{
    static public void test1()
    {
        FloatCountroler t = new FloatCountroler(45.06f);
        FloatCountroler tt =  new FloatCountroler(2.1f);
        FloatCountroler ttt = new FloatCountroler(5.99f);
        tt.OperationIndex = ValueCountrolerManager.OprationName.num_add;
        ttt.OperationIndex = ValueCountrolerManager.OprationName.num_add;
        tt.AddFactorInCopy(ttt);
        ttt.OrderIndex = 2;
        ttt.DefaultValue = 8;
        tt.AddFactorInCopy(ttt);
        ttt.OrderIndex = 3;
        ttt.DefaultValue = 2;
        tt.AddFactorInCopy(ttt);
        ttt.OrderIndex = 5;
        ttt.DefaultValue = 4;
        tt.AddFactorInCopy(ttt);
        ttt.OrderIndex = 6;
        ttt.DefaultValue = 1;
        tt.AddFactorInCopy(ttt);
        ttt.OrderIndex = 7;
        ttt.DefaultValue = -6;
        tt.AddFactorInCopy(ttt);
        ttt.DefaultValue = 8;
        ttt.OperationIndex = ValueCountrolerManager.OprationName.any_ignore;
        tt.AddFactorInCopy(ttt);
        t.AddFactorInCopy(tt);

        
        UF.print(t);

        t.FactorWithIndex(0).FactorWithIndex(0).DefaultValue = 200;
        t.FactorWithIndex(0).FactorWithIndex(1).OperationIndex = ValueCountrolerManager.OprationName.any_overwrite;
        t.FactorWithIndex(0).DelFactorWithIndex(5);
        t.FactorWithIndex(0).DefaultValue = 100;

        UF.print(t);


    }

    static public void test2()
    {
        BoolCountroler t = new BoolCountroler(true);
        BoolCountroler tt = new BoolCountroler(true);
        BoolCountroler ttt = new BoolCountroler(true);
        tt.OperationIndex = ValueCountrolerManager.OprationName.bool_and;
        ttt.OperationIndex = ValueCountrolerManager.OprationName.bool_and;
        tt.AddFactorInCopy(ttt);
        ttt.OrderIndex = 2;
        tt.AddFactorInCopy(ttt);
        ttt.OrderIndex = 3;
        tt.AddFactorInCopy(ttt);
        t.AddFactorInCopy(tt);


        UF.print(t);

        t.FactorWithIndex(0).FactorWithIndex(1).DefaultValue = false;

        UF.print(t);


    }

    static public void test3()
    {
        Vector3Countroler t = new Vector3Countroler(new Vector3(45.6f,33,5));
        Vector3Countroler tt = new Vector3Countroler(new Vector3(2.1f, 3, 2));
        Vector3Countroler ttt = new Vector3Countroler(new Vector3(6, 6, 6));
        tt.OperationIndex = ValueCountrolerManager.OprationName.num_add;
        ttt.OperationIndex = ValueCountrolerManager.OprationName.num_add;
        tt.AddFactorInCopy(ttt);
        ttt.OrderIndex = 2;
        ttt.DefaultValue = new Vector3(7, 3, 5);
        tt.AddFactorInCopy(ttt);
        ttt.OrderIndex = 3;
        ttt.DefaultValue = new Vector3(4, 33, 2);
        tt.AddFactorInCopy(ttt);
        ttt.OrderIndex = 5;
        ttt.DefaultValue = new Vector3(45.6f, -80, 9);
        tt.AddFactorInCopy(ttt);
        ttt.OrderIndex = 6;
        ttt.DefaultValue = new Vector3(45.6f, 33, 5);
        tt.AddFactorInCopy(ttt);
        ttt.OrderIndex = 7;
        ttt.DefaultValue = new Vector3(45.6f, 33, 5);
        tt.AddFactorInCopy(ttt);
        ttt.DefaultValue = new Vector3(45.6f, 33, 5);
        ttt.OperationIndex = ValueCountrolerManager.OprationName.any_ignore;
        tt.AddFactorInCopy(ttt);
        t.AddFactorInCopy(tt);


        UF.print(t);

        t.FactorWithIndex(0).FactorWithIndex(0).DefaultValue = new Vector3(100, 103, 105);
        t.FactorWithIndex(0).FactorWithIndex(1).OperationIndex = ValueCountrolerManager.OprationName.any_overwrite;
        t.FactorWithIndex(0).DelFactorWithIndex(5);
        t.FactorWithIndex(0).DefaultValue = new Vector3(200, 203, 205);

        UF.print(t);
    }

    static public void test4()
    {
        Vector2Countroler t = new Vector2Countroler(new Vector3(45.6f, 33, 5));
        Vector2Countroler tt = new Vector2Countroler(new Vector3(2.1f, 3, 2));
        Vector2Countroler ttt = new Vector2Countroler(new Vector3(6, 6, 6));
        tt.OperationIndex = ValueCountrolerManager.OprationName.num_add;
        ttt.OperationIndex = ValueCountrolerManager.OprationName.num_add;
        tt.AddFactorInCopy(ttt);
        ttt.OrderIndex = 2;
        ttt.DefaultValue = new Vector3(7, 3, 5);
        tt.AddFactorInCopy(ttt);
        ttt.OrderIndex = 3;
        ttt.DefaultValue = new Vector3(4, 33, 2);
        tt.AddFactorInCopy(ttt);
        ttt.OrderIndex = 5;
        ttt.DefaultValue = new Vector3(45.6f, -80, 9);
        tt.AddFactorInCopy(ttt);
        ttt.OrderIndex = 6;
        ttt.DefaultValue = new Vector3(45.6f, 33, 5);
        tt.AddFactorInCopy(ttt);
        ttt.OrderIndex = 7;
        ttt.DefaultValue = new Vector3(45.6f, 33, 5);
        tt.AddFactorInCopy(ttt);
        ttt.DefaultValue = new Vector3(45.6f, 33, 5);
        ttt.OperationIndex = ValueCountrolerManager.OprationName.any_ignore;
        tt.AddFactorInCopy(ttt);
        t.AddFactorInCopy(tt);


        UF.print(t);

        t.FactorWithIndex(0).FactorWithIndex(0).DefaultValue = new Vector3(100, 103, 105);
        t.FactorWithIndex(0).FactorWithIndex(1).OperationIndex = ValueCountrolerManager.OprationName.any_overwrite;
        t.FactorWithIndex(0).DelFactorWithIndex(5);
        t.FactorWithIndex(0).DefaultValue = new Vector3(200, 203, 205);

        UF.print(t);
    }


}
