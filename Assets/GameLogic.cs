using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLogic
{
	static int boardLength = 8;
	static char[,] board = new char[boardLength, boardLength];

	public static char player1Queen = 'X';
	public static char player1Pawn = 'x';
	public static char player2Queen = 'O';
	public static char player2Pawn = 'o';
	static char[] player1Pieces = new char[] { player1Pawn, player1Queen };
	static char[] player2Pieces = new char[] { player2Pawn, player2Queen };
	static char[] playersPieces = { player1Pawn, player1Queen, player2Pawn, player2Queen };

	public static char currentPlayerPawn;
	public static char currentPlayerQueen;

	public static bool isWaitingForTurn;

	public static bool thisTurnIsEatingMove;
	public static bool lastTurnIsEatingMove;
	public static bool isFirstMove;

	public static char winner;
	public static int preMovePiecesCount;
	public static int noChangeCounter = 0;
	public static int tieCountdown = 10;
	public static bool isTie;
	public static bool gameOver;

	View view;

	public static void FillBoard() {
		for (int i = 0; i < boardLength; i++) {
			for (int j = 0; j < boardLength; j++) {
				if ((i + j) % 2 == 0) {
					bool CheckQueen = false;
					//Debug.Log("i, j " + i.ToString() + " " + j.ToString());
					if (i <= 2) {
						board[i, j] = player1Pawn;
						int playerNumber = 1;
						View.PlacePieceOn3DBoard(playerNumber, CheckQueen, i, j);
					}
					else if (i >= 5) {
						board[i, j] = player2Pawn;
						int playerNumber = 2;
						View.PlacePieceOn3DBoard(playerNumber, CheckQueen, i, j);
					}
					else
					{
						board[i, j] = 'W';
					}
				}
			}
		}
	}

	public static void StartGame() {
		View.CreateBoard();
		FillBoard();

		currentPlayerPawn = player1Pawn;
		currentPlayerQueen = player1Queen;

		thisTurnIsEatingMove = false;
		lastTurnIsEatingMove = false;

		winner = '\0';
		isTie = false;
		gameOver = false;
		noChangeCounter = 0; // for tie countdown
		tieCountdown = 10;
	}


	static bool isOpponentPiece(char p) {
		if (currentPlayerPawn == player1Pawn) {
			return player2Pieces.Contains(p);
		}
		else {
			return player1Pieces.Contains(p);
		}
	}


	static int CountConsecutiveOpponentsInPath(int rowFrom, int colFrom, int rowTo, int colTo) {
		//Debug.Log("Inside CountConsecutive..");
		int count = 0;
		if (rowFrom < rowTo) {
			for (int i = 1; rowFrom + i < rowTo; i++) {
				if (colFrom < colTo) {
					if (isOpponentPiece(rowFrom + i, colFrom + i))
					{
						count++;
					}
					else count = 0;
				}
				else if (colFrom > colTo) {
					if (isOpponentPiece(rowFrom + i, colFrom - i))
					{
						count++;
					}
					else count = 0;
				}
			}
		}
		else if (rowTo < rowFrom) {
			//Debug.Log("rowTo < rowFrom");
			for (int i = 1; rowFrom - i > rowTo; i++) {
				if (colFrom < colTo) {
					//Debug.Log("colFrom < colTo");
					if (isOpponentPiece(rowFrom - i, colFrom + i))
					{
						count++;
						//Debug.Log("count++ count = "+count.ToString());
					}
					else count = 0;
				}
				else if (colFrom > colTo) {
					if (isOpponentPiece(rowFrom - i, colFrom - i))
					{
						count++;
					}
					else count = 0;
				}
			}
		}
		return count;
	}

	static bool CheckIfAlly(int row, int col) {
		if (board[row, col] == currentPlayerPawn || board[row, col] == currentPlayerQueen)
			return true;
		else
			return false;
	}

	static bool IsAllyOnDiagonal(int rowFrom, int colFrom, int rowTo, int colTo) {
		if (rowFrom < rowTo) {
			for (int i = 1; rowFrom + i < rowTo; i++) {
				if (colFrom < colTo) {
					if (CheckIfAlly(rowFrom + i, colFrom + i)) {
						return true;
					}
				}
				else if (colFrom > colTo) {
					if (CheckIfAlly(rowFrom + i, colFrom - i)) {
						return true;
					}
				}
			}
		}
		else if (rowFrom > rowTo) {
			for (int i = 1; rowFrom - i > rowTo; i++) {
				if (colFrom < colTo) {
					if (CheckIfAlly(rowFrom - i, colFrom + i)) {
						return true;
					}
				}
				else if (colFrom > colTo) {
					if (CheckIfAlly(rowFrom - i, colFrom - i)) {
						return true;
					}
				}
			}
		}
		return false;
	}

	static bool CheckQueenPathClear(int rowFrom, int colFrom, int rowTo, int colTo) {
		return (CountConsecutiveOpponentsInPath(rowFrom, colFrom, rowTo, colTo) < 2 &&
				!IsAllyOnDiagonal(rowFrom, colFrom, rowTo, colTo));
	}

	static bool isOpponentPiece(int row, int col)
	{
		//Debug.Log("Inside isOpponentPiece("+row.ToString() + " " + col.ToString());
		if (board[row, col] != currentPlayerPawn &&
			board[row, col] != currentPlayerQueen &&
			board[row, col] != 'W')
		{
			return true;
		}
		return false;

	}

	static int CountAscendingRowsAndCols(int rowFrom, int colFrom, int rowTo, int colTo)
	{
		int count = 0;
		for (int i = 0; rowFrom + i < rowTo; i++)
		{
			//Debug.Log("Inside AscRowsAndCols: rowFrom = "+rowFrom.ToString()+" i = "+i.ToString()+" colFrom = "+colFrom.ToString()
			//			+ " rowTo: " + rowTo.ToString() + " colTo: " + colTo.ToString());
			if (isOpponentPiece(rowFrom + i, colFrom + i))
			{
				count++;
			}
		}
		return count;
	}

	// Diagonal Traverser
	static int CountDescendingRowsAscendingCols(int rowFrom, int colFrom, int rowTo, int colTo)
	{
		int count = 0;
		for (int i = 0; rowFrom - i > rowTo; i++)
		{
			if (isOpponentPiece(rowFrom - i, colFrom + i))
			{
				count++;
			}
		}
		return count;
	}

	static int CountAscendingRowsDescendingCols(int rowFrom, int colFrom, int rowTo, int colTo)
	{
		int count = 0;
		for (int i = 0; rowFrom + i < rowTo; i++)
		{
			//Debug.Log("(rowFrom <= colFrom)");
			if (isOpponentPiece(rowFrom + i, colFrom - i))
			{
				count++;
			}
		}
		return count;
	} 

	static int CountOpponentsOnAscendingRows(int rowFrom, int colFrom, int rowTo, int colTo)
	{
		int count = 0;
		if (colFrom < colTo)
		{
			count += CountAscendingRowsAndCols(rowFrom, colFrom, rowTo, colTo);
		}
		else if (colFrom > colTo)
		{
			count += CountAscendingRowsDescendingCols(rowFrom, colFrom, rowTo, colTo);
		}
		return count;
	}

	static int CountDescedingRowsAndCols(int rowFrom, int colFrom, int rowTo, int colTo)
    {
		int count = 0;
		for (int i = 0; rowFrom - i > rowTo; i++)
		{
			//Debug.Log("rowFrom: " + rowFrom.ToString() + " colFrom: " + colFrom.ToString() + " rowTo: " + rowTo.ToString() + " colTo: " + colTo.ToString());
			//Debug.Log("i = " + i.ToString());
			if (isOpponentPiece(rowFrom - i, colFrom - i))
			{
				count++;
			}
		}
		return count;
	}

	static int CountOpponentsOnDescendingRows(int rowFrom, int colFrom, int rowTo, int colTo)
	{
		int count = 0;
		if (colFrom < colTo)
		{
			count += CountDescendingRowsAscendingCols(rowFrom, colFrom, rowTo, colTo);
		}
		else if (colFrom > colTo)
		{
			count += CountDescedingRowsAndCols(rowFrom, colFrom, rowTo, colTo);
		}
		return count;
	}

	static int CountOpponentsOnDiagonal(int rowFrom, int colFrom, int rowTo, int colTo) {
		Debug.Log("Inside CountOpponentsOnDiagonal(" + rowFrom.ToString() + "," + colFrom.ToString() + "," + rowTo.ToString() + "," + colTo.ToString());
		int count = 0;
		if (rowFrom < rowTo) {
			//Debug.Log("(rowFrom > rowTo)");
			count += CountOpponentsOnAscendingRows(rowFrom, colFrom, rowTo, colTo);
		}
		else if (rowFrom > rowTo) {
			//Debug.Log("(rowFrom < rowTo)");
			count += CountOpponentsOnDescendingRows(rowFrom, colFrom, rowTo, colTo);
		}
		return count;
	}
	

	static bool IsCurrentPlayerPiece(int row, int col)
	{
		return board[row, col] == currentPlayerPawn || board[row, col] == currentPlayerQueen;
	}
	
	static bool IsCellEmpty(int row, int col)
	{
		return board[row, col] == 'W';
	}

	static bool CheckAdjecentCell(int rowFrom, int colFrom, int rowTo, int colTo, char piece)
	{
		if (piece == player1Pawn)
		{
			return rowFrom + 1 == rowTo && (Math.Abs(colFrom - colTo) == 1);
		}
		else if (piece == player2Pawn)
		{
			return rowFrom - 1 == rowTo && (Math.Abs(colFrom - colTo) == 1);
		}
		return false;
	}
	
	static bool CheckQueen(int row, int col)
	{
		return board[row, col] == player1Queen || board[row, col] == player2Queen;
	}
	
	static bool CheckQueenEatingMove(int rowFrom, int colFrom, int rowTo, int colTo)
    {
		if (CheckQueen(rowFrom, colFrom))
		{
			return CountOpponentsOnDiagonal(rowFrom, colFrom, rowTo, colTo) > 0 && 
				CountConsecutiveOpponentsInPath(rowFrom, colFrom, rowTo, colTo) < 2;
		}
		return false;
    }
	
	static bool IsValidQueenMove(int rowFrom, int colFrom, int rowTo, int colTo)
	{
		return CheckQueenPathClear(rowFrom, colFrom, rowTo, colTo);
	}

	static bool CheckPawnEatingMove(int rowFrom, int colFrom, int rowTo, int colTo)
	{
		Debug.Log("Inside CheckPawnEatingMove("+ rowFrom.ToString() + " " + colFrom.ToString() + " " + rowTo.ToString() + " " + colTo.ToString());
		char piece = board[rowFrom, colFrom];

		bool isTwoColsAway = (Math.Abs(colFrom - colTo) == 2f);
		bool oneOpponentInPath = (CountOpponentsOnDiagonal(rowFrom, colFrom, rowTo, colTo) == 1f);

		if (piece == player1Pawn)
		{
			bool isTwoRowsForwards = (rowFrom + 2 == rowTo);
			return isTwoRowsForwards && isTwoColsAway && oneOpponentInPath;
		}
		else if (piece == player2Pawn)
		{
			//Debug.Log("piece == player2Pawn");
			bool isTwoRowsBack = (rowFrom - 2 == rowTo);
			return isTwoRowsBack && isTwoColsAway && oneOpponentInPath;
		}
		else
		{
			Debug.Log("EatingPawn at false");
			return false;
		}
	}

	static bool IsSquareDiagonal(int rowFrom, int colFrom, int rowTo, int colTo)
    {
		if ((rowFrom - colFrom == rowTo - colTo) || (rowFrom + colFrom == rowTo + colTo))
        {
			//Debug.Log("Inside IsSquareDiagonal = true (" + rowFrom.ToString() + " " + colFrom.ToString() + " " + rowTo.ToString() + " " + colTo.ToString());
			return true;
        }
		else
        {
			//Debug.Log("Inside IsSquareDiagonal = false (" + rowFrom.ToString() + " " + colFrom.ToString() + " " + rowTo.ToString() + " " + colTo.ToString());
			//Debug.Log(rowFrom + rowTo == colFrom + colTo);
			return false;
        }
    }

	static bool CheckEatingMove(int rowFrom, int colFrom, int rowTo, int colTo)
    {
		return CheckQueenEatingMove(rowFrom, colFrom, rowTo, colTo) || 
			CheckPawnEatingMove(rowFrom, colFrom, rowTo, colTo);
    }

	static bool CheckBoardConditions(int rowFrom, int colFrom, int rowTo, int colTo)
    {
		//Debug.Log("Inside CheckBoardCondition(" + rowFrom.ToString() + " " + colFrom.ToString() + " " + rowTo.ToString() + " " + colTo.ToString());
		if (!IsCurrentPlayerPiece(rowFrom, colFrom))
		{
			//Debug.Log("Not current player piece");
			return false;
		}
		if (!IsCellEmpty(rowTo, colTo))
		{
			//Debug.Log("Cell isn't empty");
			return false;
		}
		//Debug.Log("before IsSquareDiagonal(" + rowFrom.ToString() + " " + colFrom.ToString() + " " + rowTo.ToString() + " " + colTo.ToString());
		if (!IsSquareDiagonal(rowFrom, colFrom, rowTo, colTo))
		{
			//Debug.Log("IsSquareDiagonal(" + rowFrom.ToString() + " " + colFrom.ToString() + " " + rowTo.ToString() + " " + colTo.ToString());
			return false;
		}
		return true;
	}

	static bool CheckNonEatingMoveConditions(int rowFrom, int colFrom, int rowTo, int colTo)
    {
		if (CheckAdjecentCell(rowFrom, colFrom, rowTo, colTo, board[rowFrom, colFrom]))
		{
			Debug.Log("Inside CheckAdjecentCell(" + rowFrom.ToString() + " " + colFrom.ToString() + " " + rowTo.ToString() + " " + colTo.ToString());
			return true;
		}

		if (CheckQueen(rowFrom, colFrom))
        {
			return (!IsAllyOnDiagonal(rowFrom, colFrom, rowTo, colTo)) &&
				(CountOpponentsOnDiagonal(rowFrom, colFrom, rowTo, colTo) == 0);
		}
		return false;
	}

	static bool CheckEatingMoveConditions(int rowFrom, int colFrom, int rowTo, int colTo)
    {
		//Debug.Log("CheckEatingMoveConditions(" + rowFrom.ToString() + " " + colFrom.ToString() + " " + rowTo.ToString() + " " + colTo.ToString());
		if (CheckPawnEatingMove(rowFrom, colFrom, rowTo, colTo))
		{
			return true;
		}
		else if (CheckQueenEatingMove(rowFrom, colFrom, rowTo, colTo))
        {
			return true;
        }
		return false;
	}

	static bool CheckValidMove(int rowFrom, int colFrom, int rowTo, int colTo) {
		//Debug.Log("Inside CheckValidMove("+rowFrom.ToString() + " " + colFrom.ToString() + " " + rowTo.ToString() + " " + colTo.ToString());
		//Debug.Log("board[rowTo, colTo]]: " + board[rowTo, colTo].ToString());
		
		//Debug.Log("before CheckBoardCondition(" + rowFrom.ToString() + " " + colFrom.ToString() + " " + rowTo.ToString() + " " + colTo.ToString());
		if (!CheckBoardConditions(rowFrom, colFrom, rowTo, colTo))
        {
			//Debug.Log("In CheckBoardCondition(" + rowFrom.ToString() + " " + colFrom.ToString() + " " + rowTo.ToString() + " " + colTo.ToString());
			return false;
        }
		Debug.Log("before NonEatingMoveConditions(" + rowFrom.ToString() + " " + colFrom.ToString() + " " + rowTo.ToString() + " " + colTo.ToString());
		if (CheckNonEatingMoveConditions(rowFrom, colFrom, rowTo, colTo))
		{
			return true;
		}

		if (CheckEatingMoveConditions(rowFrom, colFrom, rowTo, colTo))
        {
			return true;
        }
		return false;
	}


	public static void EndTurn() {
		View.SetButtonStartText();
		if (!isWaitingForTurn)
		{
			if (currentPlayerPawn == player1Pawn)
			{
				currentPlayerPawn = player2Pawn;
				currentPlayerQueen = player2Queen;
				View.ChangeButtonColor(2);
			}
			else
			{
				currentPlayerPawn = player1Pawn;
				currentPlayerQueen = player1Queen;
				View.ChangeButtonColor(1);
			}

			noChangeCounter = UpdateNoChangeCounter(preMovePiecesCount, noChangeCounter);
			//Debug.Log("noChangeCounter: " + noChangeCounter.ToString());
			Debug.Log("before CheckWinner()");
			winner = CheckWinner();
			Debug.Log("winner = " + winner.ToString());
			if (winner != '\0')
			{
				View.DisplayWin(winner);
			}
			
			else
			{
				isTie = CheckTie();
				Debug.Log("isTie = true");

				if (isTie)
				{
					View.DisplayTie();
				}
				else
				{
					UpdateTieCountdown();

					Debug.Log("current player = " + currentPlayerPawn.ToString());

					Debug.Log("isTie: " + isTie.ToString());
					isWaitingForTurn = true;

					isFirstMove = true;
					lastTurnIsEatingMove = false;
					thisTurnIsEatingMove = false;
				}
			}
		}
	}


	static int CountPieces(char playerPawn, char playerQueen) {
		int count = 0;
		for (int i = 0; i < boardLength; i++) {
			for (int j = 0; j < boardLength; j++) {
				if (board[i, j] == playerQueen || board[i, j] == playerPawn) {
					count++;
				}
			}
		}
		return count;
	}

	static int UpdateNoChangeCounter(int preMovePiecesCount, int noChangeCounter) {
		int postMovePiecesCount = CountPieces(player1Pawn, player1Queen) +
									CountPieces(player2Pawn, player2Queen);
		if (preMovePiecesCount == postMovePiecesCount) {
			noChangeCounter++;
		}
		else {
			noChangeCounter = 0;
		}
		return noChangeCounter;
	}


	static void CheckAndRemoveOpponentPiece(int row, int col) {
		char pieceToCheck = board[row, col];
		//Debug.Log("Piece to check and remove: " + (board[row,col]).ToString());
		if (pieceToCheck != currentPlayerPawn && pieceToCheck != currentPlayerQueen && pieceToCheck != 'W')
		{
			board[row, col] = 'W';
			View.RemovePieceOn3DBoard(row, col);
		}
	}
	
	static void PerformPawnEating(int rowFrom, int colFrom, int rowTo, int colTo)
	{
		var rowPawn = (rowFrom + rowTo) / 2;
		var colPawn = (colFrom + colTo) / 2;
		board[rowPawn, colPawn] = 'W';
		//Debug.Log("before View.RemovePieceOn3DBoard(rowPawn, colPawn): rowPawn" + rowPawn.ToString() + " colPawn: " + colPawn.ToString());
		View.RemovePieceOn3DBoard(rowPawn, colPawn);
	}

	static void PeformQueenEating(int rowFrom, int colFrom, int rowTo, int colTo)
    {
		//Debug.Log("Inside PefromQueenEating");
		if (rowFrom < rowTo)
		{
			for (int i = 1; rowFrom + i < rowTo; i++)
			{
				if (colFrom < colTo)
				{
					CheckAndRemoveOpponentPiece(rowFrom + i, colFrom + i);
				}
				else if (colFrom > colTo)
				{
					CheckAndRemoveOpponentPiece(rowFrom + i, colFrom - i);
				}
			}
		}
		else if (rowFrom > rowTo)
		{
			for (int i = 1; rowFrom - i > rowTo; i++)
			{
				if (colFrom < colTo)
				{
					CheckAndRemoveOpponentPiece(rowFrom - i, colFrom + i);
				}
				else
				{
					if (colFrom > colTo)
					{
						CheckAndRemoveOpponentPiece(rowFrom - i, colFrom - i);
					}
				}
			}
		}
	}

	static void PerformEating(int rowFrom, int colFrom, int rowTo, int colTo) {
		//Debug.Log("before if (board[rowFrom, colFrom] == currentPlayerPawn) " + (board[rowFrom, colFrom] == currentPlayerPawn).ToString());
		//Debug.Log("Inside PeformEating");
		char piecePerformingEating = board[rowFrom, colFrom];
		if (piecePerformingEating == currentPlayerPawn)
		{
			PerformPawnEating(rowFrom, colFrom, rowTo, colTo);
		}
		else if (piecePerformingEating == currentPlayerQueen) 
		{
			PeformQueenEating(rowFrom, colFrom, rowTo, colTo);
		}
	}
	
	static void DoTurn(int rowFrom, int colFrom, int rowTo, int colTo) {
		Debug.Log("Inside DoTurn(" + rowFrom.ToString() + " " + colFrom.ToString() + " " + rowTo.ToString() + " " + colTo.ToString());
		isWaitingForTurn = false;
		char fromPiece = board[rowFrom, colFrom];
		board[rowTo, colTo] = fromPiece;
		bool isOpponentOnDiagonal = CountOpponentsOnDiagonal(rowFrom, colFrom, rowTo, colTo) >= 1;
		//Debug.Log("isOpponentOnDiagonal: " + isOpponentOnDiagonal.ToString());
		if (isOpponentOnDiagonal)
		{
			//Debug.Log("before PerformEating(" + rowFrom.ToString() + " " + colFrom.ToString() + " " + rowTo.ToString() + " " + colTo.ToString());
			PerformEating(rowFrom, colFrom, rowTo, colTo);
		}
		board[rowFrom, colFrom] = 'W';

		int playerNumber;
		bool CheckQueen = false;
		char[] player1Pieces = {player1Pawn, player1Queen};
		char[] player2Pieces = {player2Pawn, player2Queen};

		if (player1Pieces.Contains(fromPiece)){
			playerNumber = 1;
			if (fromPiece == player1Queen) {
				CheckQueen = true;
			}
		}
		else {
			playerNumber = 2;
			if (fromPiece == player2Queen) {
				CheckQueen = true;
			}
		}
		
		View.MovePieceOn3DBoard(playerNumber, CheckQueen, rowFrom, colFrom, rowTo, colTo);
	}
	

	static char CheckWinner() {
		//Debug.Log("Inside CheckWinner");
		char[][] players = new char[][]{
			new char[] {player1Pawn, player1Queen}, 
			new char[] {player2Pawn, player2Queen}
		};
		for (int p=0; p<players.Length; p++) {
			if (CountPieces(players[p][0], players[p][1]) == 0) {
				char otherPlayerQueen = players[((p+1)%2)][1];
				return otherPlayerQueen;
			} 
		}
		return '\0';
	}
	
	static void UpdateTieCountdown() {
		bool isFewPiecesLeft = CountPieces(player1Pawn, player1Queen) + CountPieces(player2Pawn, player2Queen) < 6;
		bool noChangeInLastFewTurns = noChangeCounter >= 5;
		Debug.Log("isFewPiecesLeft: " + isFewPiecesLeft);
		Debug.Log("noChangeInLastFewTurns " + noChangeInLastFewTurns);
		if (isFewPiecesLeft	&& noChangeInLastFewTurns){
			tieCountdown --;
		}
		else {
			tieCountdown = 10;
		}
		View.UpdateTieCountdownDisplay(tieCountdown);
	}
	
	static bool CheckTie() {
		Debug.Log("Inside CheckTie");
		if (tieCountdown == 0) {
			return true;
		}
		for (int rowFrom=0; rowFrom<boardLength; rowFrom++) {
			for (int colFrom=0; colFrom<boardLength; colFrom++) {
				if (board[rowFrom, colFrom] == currentPlayerPawn || 
					board[rowFrom, colFrom] == currentPlayerQueen) {
					for (int rowTo=0; rowTo<boardLength; rowTo++) {
						for (int colTo=0; colTo<boardLength; colTo++) {
							if (CheckValidMove(rowFrom, colFrom, rowTo, colTo)) {
								Debug.Log("at CheckMove(" + rowFrom.ToString() + " " + colFrom.ToString() + " " + rowTo.ToString() + " " + colTo.ToString());
								return false; 
							}
						}
					}
				}	
			}
		}
		return true;
	}
	
	static void DoQueening(int row, int col){
		board[row, col] = currentPlayerQueen;
		var mesh = View.GetMeshOfCell(row, col);
		var playerNumber = 2;
		if (currentPlayerQueen == player1Queen)
        {
			playerNumber = 1;
        }
		View.ChangeColorOfQueen(mesh, playerNumber);
	}
	
	static void CalcMoveConsequences(int rowFrom, int colFrom, int rowTo, int colTo) {
		var currentPiece = board[rowTo, colTo];
		if (currentPiece == player1Pawn)
		{
			if (rowTo == boardLength - 1)
			{
				DoQueening(rowTo, colTo);
			}
		}
		else
        {
			if (rowTo == 0)
            {
				DoQueening(rowTo, colTo);
            }
        }
	}
	
	public static void MakeMove(int rowFrom, int colFrom, int rowTo, int colTo) {
		bool isValidMove = CheckValidMove(rowFrom, colFrom, rowTo, colTo);
		Debug.Log("Check Valid Move:" + isValidMove.ToString());
		if (CheckEatingMove(rowFrom, colFrom, rowTo, colTo))
		{
			thisTurnIsEatingMove = true;
		}
		if (isValidMove && (isFirstMove || (thisTurnIsEatingMove && lastTurnIsEatingMove))) {
			preMovePiecesCount = CountPieces(player1Pawn, player1Queen) + 
									 CountPieces(player2Pawn, player2Queen);

			DoTurn(rowFrom, colFrom, rowTo, colTo);

			isFirstMove = false;
			if (thisTurnIsEatingMove)
            {
				lastTurnIsEatingMove = true;
				thisTurnIsEatingMove = false;
            }
			CalcMoveConsequences(rowFrom, colFrom, rowTo, colTo);
		}
	}

	public static void ShowPossibleMoves(int rowFrom, int colFrom)
    {
		// go over all the moves and see which are valid
		for (int i=0; i<boardLength; i++)
        {
			for (int j=0; j<boardLength; j++)
            {
				if (CheckValidMove(rowFrom, colFrom, i, j))
                {
					View.MarkCellForPossibleMove(i, j);
                }
            }
        }
    }

	public static void UnshowPossibleMoves(int rowFrom, int colFrom)
	{
		// go over all the moves and see which are valid
		for (int i = 0; i < boardLength; i++)
		{
			for (int j = 0; j < boardLength; j++)
			{
				View.UnmarkCellForPossibleMove(i, j);
			}
		}
	}
}