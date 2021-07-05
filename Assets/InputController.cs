using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
	//[SerializeField] GameLogic gl;
	int rowFrom, colFrom, rowTo, colTo;
	public Button button;
	public Camera camera; // pair to MainCamera game object

	bool isFirstClick = true;

	void Start()
    {
		//Debug.Log("in Start() input controller");
		GameLogic.StartGame();
       
		button.GetComponent<Button>().onClick.AddListener(GameLogic.EndTurn);

		// Tests
		//main.TestTieOld();
		//main.TestTieCountdownOld();
		//main.Test();
	}

	void GetInput() {
		
		if (Input.GetMouseButtonUp(0) && isFirstClick)
		{
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				string[] coordinatesFrom = hit.collider.gameObject.name.Split(' ');
				Debug.Log("coordinatesFrom " + coordinatesFrom[0] + coordinatesFrom[1]);
				rowFrom = int.Parse(coordinatesFrom[1]);
				colFrom = int.Parse(coordinatesFrom[2]);
				isFirstClick = false;
				GameLogic.ShowPossibleMoves(rowFrom, colFrom);
			}
		}
		else if (Input.GetMouseButtonUp(0) && !isFirstClick) {
			Ray ray2 = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit2;
			if (Physics.Raycast(ray2, out hit2))
			{
				string[] coordinatesTo = hit2.collider.gameObject.name.Split(' ');
				Debug.Log("coordinatesTo " + coordinatesTo[0] + coordinatesTo[1]);
				rowTo = int.Parse(coordinatesTo[1]);
				colTo = int.Parse(coordinatesTo[2]);

				GameLogic.UnshowPossibleMoves(rowFrom, colFrom);
				GameLogic.MakeMove(rowFrom, colFrom, rowTo, colTo);
				isFirstClick = true;
			}
		}
	}
	
	void Update(){
		if (!GameLogic.gameOver){
			GetInput();
		}
		if (Input.GetKeyDown(KeyCode.Space))
        {
			GameLogic.EndTurn();
        }
	}
	
}
