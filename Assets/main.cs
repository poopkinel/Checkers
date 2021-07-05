using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class main : MonoBehaviour
{	
	void Start()
    {
		//Debug.Log("Inside main.Start");
		//Test();

    }

	public static void TestPlayer2Win()
    {
		Test();
		GameLogic.MakeMove(1, 7, 2, 6);
		GameLogic.EndTurn();
		GameLogic.MakeMove(5, 7, 3, 5);
		GameLogic.EndTurn();
		GameLogic.MakeMove(0, 0, 1, 1);
		GameLogic.EndTurn();
		GameLogic.MakeMove(3, 5, 1, 7);
		GameLogic.EndTurn();
		GameLogic.MakeMove(0, 6, 1, 5);
		GameLogic.EndTurn();
		GameLogic.MakeMove(1, 7, 0, 6);
		GameLogic.EndTurn();
		GameLogic.MakeMove(1, 1, 2, 2);
		GameLogic.EndTurn();
		GameLogic.MakeMove(0, 6, 3, 3);
		GameLogic.MakeMove(3, 3, 1, 1);
	}

	public static void Test()
    {
		//GameLogic.EndTurn()
		GameLogic.EndTurn();
		GameLogic.MakeMove(5, 3, 4, 4);
		GameLogic.EndTurn();
		GameLogic.MakeMove(2, 2, 3, 3);
		GameLogic.EndTurn();
		GameLogic.MakeMove(4, 4, 2, 2);
		GameLogic.EndTurn();
		GameLogic.MakeMove(1, 1, 3, 3);
		GameLogic.EndTurn();
		GameLogic.MakeMove(5, 5, 4, 4);
		GameLogic.EndTurn();
		GameLogic.MakeMove(3, 3, 5, 5);
		GameLogic.EndTurn();
		GameLogic.MakeMove(6, 6, 4, 4);
		GameLogic.EndTurn();
		GameLogic.MakeMove(2, 4, 3, 5);
		GameLogic.EndTurn();
		GameLogic.MakeMove(5, 7, 4, 6);
		GameLogic.EndTurn();
		GameLogic.MakeMove(3, 5, 5, 3);
		GameLogic.EndTurn();
		GameLogic.MakeMove(6, 2, 4, 4);
		GameLogic.EndTurn();
		GameLogic.MakeMove(2, 6, 3, 5);
		GameLogic.EndTurn();
		GameLogic.MakeMove(4, 6, 2, 4);
		GameLogic.EndTurn();
		GameLogic.MakeMove(1, 5, 3, 3);
		GameLogic.MakeMove(3, 3, 5, 5);
		GameLogic.EndTurn();
		GameLogic.MakeMove(6, 4, 4, 6);
		GameLogic.EndTurn();
		GameLogic.MakeMove(1, 3, 2, 4);
		GameLogic.EndTurn();
		GameLogic.MakeMove(4, 6, 3, 5);
		GameLogic.EndTurn();
		GameLogic.MakeMove(2, 4, 4, 6);
		GameLogic.EndTurn();
		GameLogic.MakeMove(5, 1, 4, 2);
		GameLogic.EndTurn();
		GameLogic.MakeMove(2, 0, 3, 1);
		GameLogic.EndTurn();
		GameLogic.MakeMove(4, 2, 2, 0);
		GameLogic.EndTurn();
		GameLogic.MakeMove(0, 2, 1, 1);
		GameLogic.EndTurn();
		GameLogic.MakeMove(2, 0, 0, 2);
		GameLogic.EndTurn();
		GameLogic.MakeMove(0, 4, 1, 3);
		GameLogic.EndTurn();
		GameLogic.MakeMove(0, 2, 5, 7);
		GameLogic.EndTurn();
		
	}

	public static void TestPlayer1Old()
    {
		GameLogic.EndTurn();
		GameLogic.MakeMove(5, 3, 4, 2);
		GameLogic.MakeMove(4, 2, 3, 1);
		GameLogic.MakeMove(6, 4, 5, 3);
		GameLogic.MakeMove(5, 3, 4, 2);
		GameLogic.MakeMove(7, 5, 6, 4);
		GameLogic.EndTurn();
		GameLogic.MakeMove(2, 4, 3, 3);
		GameLogic.MakeMove(3, 3, 4, 4);
		GameLogic.MakeMove(4, 4, 5, 3);
		GameLogic.MakeMove(5, 3, 7, 5);
	}

	public static void TestPlayer2Old()
    {
		GameLogic.MakeMove(2, 4, 3, 3);
		GameLogic.MakeMove(3, 3, 4, 2);
		GameLogic.MakeMove(1, 5, 2, 4);
		GameLogic.MakeMove(2, 4, 3, 3);
		GameLogic.EndTurn();
		GameLogic.MakeMove(5, 5, 4, 4);
		GameLogic.MakeMove(4, 4, 3, 5);
		GameLogic.MakeMove(3, 5, 2, 4);
		GameLogic.EndTurn();
		GameLogic.MakeMove(0, 6, 1, 5);
		GameLogic.EndTurn();
		GameLogic.MakeMove(2, 4, 0, 6);

	}
	public static void Test3Old()
    {
		TestPlayer2Old();
		GameLogic.EndTurn();
		GameLogic.MakeMove(3, 3, 4, 4);
		GameLogic.EndTurn();
		GameLogic.MakeMove(0, 6, 3, 3);
		//GameLogic.MakeMove(3, 3, 5, 5);
	}
	public static void Test4Old()
    {
		Test3Old();
		GameLogic.EndTurn();
		GameLogic.MakeMove(2, 6, 3, 5);
		GameLogic.MakeMove(3, 5, 4, 6);
		GameLogic.MakeMove(4, 6, 5, 5);
		GameLogic.MakeMove(1, 7, 2, 6);
		GameLogic.EndTurn();
		GameLogic.MakeMove(5, 7, 4, 6);
		GameLogic.MakeMove(4, 6, 3, 5);
		GameLogic.MakeMove(3, 5, 1, 7);
		GameLogic.MakeMove(5, 3, 3, 5);
		GameLogic.MakeMove(6, 4, 4, 6);
		GameLogic.MakeMove(3, 3, 5, 5);
		GameLogic.EndTurn();
		GameLogic.MakeMove(2, 2, 3, 3);
		GameLogic.MakeMove(3, 3, 4, 4);
		GameLogic.MakeMove(1, 1, 2, 2);
		GameLogic.MakeMove(1, 3, 2, 4);
	}

	public static void Test5WinOld()
    {
		Test4Old();
		GameLogic.MakeMove(0, 4, 1, 5);
		GameLogic.MakeMove(1, 5, 2, 6);
		GameLogic.EndTurn();
		GameLogic.MakeMove(5, 5, 3, 3);
		GameLogic.MakeMove(3, 3, 1, 5);
		GameLogic.MakeMove(1, 5, 3, 7);
		GameLogic.MakeMove(5, 1, 3, 3);
		GameLogic.MakeMove(3, 3, 1, 1);
		GameLogic.EndTurn();
		GameLogic.MakeMove(0, 0, 2, 2);
		GameLogic.MakeMove(2, 2, 3, 3);
		GameLogic.MakeMove(3, 3, 4, 4);
		GameLogic.MakeMove(4, 4, 5, 5);
		GameLogic.MakeMove(0, 2, 1, 3);
		GameLogic.MakeMove(2, 0, 3, 1);
		GameLogic.EndTurn();
		GameLogic.MakeMove(3, 7, 0, 4);
		GameLogic.MakeMove(0, 4, 2, 2);
		GameLogic.MakeMove(2, 2, 4, 0);

	}

	public static void TestTieOld()
    {
		//GameLogic.MakeMove(2, 0, 3, 1);
		//GameLogic.MakeMove(3, 1, 4, 0);
		for (int j = 2; j >= 0; j--)
		{
			for (int i = 0; i < 7; i++)
			{
				GameLogic.MakeMove(j, i, j+1, i + 1);
				GameLogic.MakeMove(j+1, i + 1, j+2, i);
			}
		}
	}

	public static void TestTieCountdownOld()
    {
		Test5WinOld();
		GameLogic.MakeMove(7, 3, 6, 4);
		GameLogic.EndTurn();
		GameLogic.MakeMove(5, 5, 7, 3);
		GameLogic.MakeMove(7, 3, 3, 7);
		GameLogic.MakeMove(3, 7, 2, 6);
		GameLogic.EndTurn();
		GameLogic.MakeMove(6, 2, 5, 3);
		GameLogic.EndTurn();
		GameLogic.MakeMove(2, 6, 4, 4);
		GameLogic.MakeMove(4, 4, 6, 2);
		GameLogic.EndTurn();
		GameLogic.MakeMove(6, 0, 5, 1);
		GameLogic.MakeMove(5, 1, 4, 2);
		GameLogic.MakeMove(4, 2, 3, 3);
		GameLogic.MakeMove(7, 5, 6, 4);
		GameLogic.MakeMove(6, 4, 5, 3);
		GameLogic.MakeMove(6, 6, 5, 5);
		GameLogic.EndTurn();
		GameLogic.MakeMove(6, 2, 4, 4);
		GameLogic.MakeMove(4, 4, 2, 2);
		GameLogic.MakeMove(2, 2, 6, 6);
		GameLogic.MakeMove(6, 6, 5, 5);
		GameLogic.EndTurn();
		GameLogic.MakeMove(7, 7, 6, 6);
		GameLogic.EndTurn();
		GameLogic.MakeMove(5, 5, 7, 7);
		GameLogic.MakeMove(7, 7, 4, 4);
		GameLogic.EndTurn();
		GameLogic.MakeMove(7, 1, 6, 2);
		GameLogic.EndTurn();
		GameLogic.MakeMove(4, 4, 7, 1);
	}

}