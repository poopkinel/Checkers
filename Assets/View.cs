using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View : MonoBehaviour
{
	static float queenScale = 1.2f;

	static Color player1PawnColor = Color.red;
	static Color player1QueenColor = new Color(1f, 0.2f, 0.45f, 1f);
	static Color player2PawnColor = Color.blue;
	static Color player2QueenColor = Color.cyan;
	static Color RegularCellColor = Color.white;
	static Color MarkedCellColor = Color.grey;

	public static void CreateBoard() {
		
	}
	
	public static void RemovePieceOn3DBoard(int row, int col)
	{
		Debug.Log("Inside RemovePieceOn3DBoard( " + "row=" + row.ToString() + " col = " + col.ToString());
		GameObject pieceToDestroy = GameObject.Find("Cell " + row + " " + col).transform.GetChild(0).gameObject;
		Debug.Log("pieceToDestroy: " + pieceToDestroy.ToString());
		//Destroy(pieceToDestroy);
		DestroyImmediate(pieceToDestroy, true);
	}
	
	public static void MovePieceOn3DBoard(int playerNumber, bool isQueen, int rowFrom, int colFrom, int rowTo, int colTo) {
		PlacePieceOn3DBoard(playerNumber, isQueen, rowTo, colTo);
		RemovePieceOn3DBoard(rowFrom, colFrom);
	}
	
	public static void PlacePieceOn3DBoard (int playerNumber, bool isQueen, int row, int col){
		GameObject cellToPlay = GameObject.Find("Cell " + row + " " + col );
		GameObject mesh;
		
		if (playerNumber == 1) {
			if (!isQueen)
			{
				mesh = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				var meshRenderer = mesh.GetComponent<Renderer>();
				meshRenderer.material.SetColor("_Color", player1PawnColor);
				mesh.layer = 2; // let raycast go through
			}
			else 
			{
				mesh = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				var meshRenderer = mesh.GetComponent<Renderer>();
				meshRenderer.material.SetColor("_Color", player1QueenColor);
				mesh.layer = 2;
			}
		}
		else {
			if (!isQueen)
			{
				mesh = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				var meshRenderer = mesh.GetComponent<Renderer>();
				meshRenderer.material.SetColor("_Color", player2PawnColor);
				mesh.layer = 2; // let raycast go through
			}
			else {
				mesh = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				var meshRenderer = mesh.GetComponent<Renderer>();
				meshRenderer.material.SetColor("_Color", player2QueenColor);
				mesh.layer = 2; // let raycast go through
			}
		}
		mesh.transform.position = cellToPlay.transform.position;
		mesh.transform.SetParent(cellToPlay.transform);
	}
	
	public static void DisplayTie() {
		Text textObject = GameObject.Find("End Game Text").GetComponent<Text>();
		textObject.text = "It's a Tie!";
		GameObject button = GameObject.Find("Button");
		DestroyImmediate(button, true);
	}

	public static void DisplayWin(char winner)
	{
		// set text to the winner
		Text textObject = GameObject.Find("End Game Text").GetComponent<Text>();
		if (winner == 'X')
        {
			textObject.text = "Winner:\nPlayer 1 (Red)!";
		}
		else
        {
			textObject.text = "Winner:\nPlayer 2 (Blue)!";
		}

		// destroy button
		GameObject button = GameObject.Find("Button");
		DestroyImmediate(button, true);
	}

	public static GameObject GetMeshOfCell(int row, int col){
		return GameObject.Find("Cell " + row + " " + col);
	}
	
	public static void ScaleUpMeshToQueen(GameObject mesh){
		var x = mesh.transform.localScale.x;
		var y = mesh.transform.localScale.y;
		var z = mesh.transform.localScale.z;
		mesh.transform.localScale = new Vector3(queenScale*x, queenScale*y, queenScale*z);
	}

	public static void ChangeColorOfQueen(GameObject mesh, int playerNumber)
    {
		Debug.Log("Inside ChangeColorOfQueen(mesh.name=" + (mesh.name).ToString());
		var meshChild = mesh.transform.GetChild(0).gameObject;
		Debug.Log("mesh.GetChild(0): " + (meshChild));
		Renderer rend = meshChild.GetComponent<Renderer>();
		if (playerNumber == 2)
		{
			rend.material.SetColor("_Color", player2QueenColor);
		}
		else
        {
			rend.material.SetColor("_Color", player1QueenColor);
		}
	}

	public static void ChangeButtonColor(int playerNumber)
    {
		GameObject button = GameObject.Find("Button");
		if (playerNumber == 1)
        {
			button.GetComponent<Image>().color = player1PawnColor;
        }
		else
        {
			button.GetComponent<Image>().color = player2PawnColor;
		}
    }

	public static void SetButtonStartText()
    {
		Text textObject = GameObject.Find("Text").GetComponent<Text>();
		textObject.text = "End Turn";
    }

	public static void UpdateTieCountdownDisplay(int tieCountdown)
    {
		GameObject tieCounter = GameObject.Find("Tie Counter");
		Text textObj = tieCounter.GetComponent<Text>();
		textObj.text = tieCountdown.ToString();
	}

	public static void MarkCellForPossibleMove(int i, int j)
    {
		GameObject cellToMark = GameObject.Find("Cell " + i + " " + j);
		Renderer rend = cellToMark.GetComponent<Renderer>();
		rend.material.SetColor("_Color", MarkedCellColor);
	}

	public static void UnmarkCellForPossibleMove(int i, int j)
    {
		GameObject cellToMark = GameObject.Find("Cell " + i + " " + j);
		Renderer rend = cellToMark.GetComponent<Renderer>();
		rend.material.SetColor("_Color", RegularCellColor);
	}
}