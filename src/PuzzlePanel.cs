using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

//this panel contains the actual puzzle tiles

public class PuzzlePanel : Panel {

	private string theme;
	private int dimension;
	private bool gameInProgress;
	private Tile removedTile;
	private Tile blankTile;
	
	public PuzzlePanel () : base() {
		Build();
	}

	public string Theme {
		get { return theme; }
		set { theme = value; }
	}

	public int Dimension {
		get { return dimension; }
	}

	public bool GameInProgress {
		get { return gameInProgress; }
		set { gameInProgress = value; }
	}

	public bool GameComplete {
		get {
			bool isComplete = false;
			if (GameInProgress) {
				isComplete = true;
				foreach (Control item in Controls) {
					Tile tile = (Tile) item;
					if (tile.CurrentIndex != tile.StartingIndex) {
						isComplete = false;
						break;
					}
				}
			}
			return isComplete;
		}
	}

	public Tile BlankTile {
		get { return blankTile; }
	}

	private void Build() {
		ResetTiles();
	}

	public void ResetTiles() {
		removedTile = null;
		blankTile = null;
		Controls.Clear();

		theme = PuzzleIO.ReadCurrentTheme();
		string[] imageNames = PuzzleIO.ReadThemeImageNames(theme);
		dimension = (int) Math.Sqrt(imageNames.Length);
		Tile currentTile;

		int index = 0;
		foreach (string imageName in imageNames) {
			currentTile = new Tile(index, imageName, this);
			currentTile.Location = GetTileStartingLocation(currentTile);
			Controls.Add(currentTile);

			if (index == 0) {
				Width = currentTile.Width * dimension;
				Height = currentTile.Height * dimension;
			}
			index++;
		}
		Invalidate();
	}

	public void DoNewGame() {
		//reset tiles if game is in progress
		if (GameInProgress) {
			ResetTiles();
		}

		GameInProgress = true;
		RemoveRandomTile();
		ShuffleTiles();
	}

	private void RemoveRandomTile() {
		int index = new Random().Next(Controls.Count);
		removedTile = RemoveTileAt(index);
		blankTile = new BlankTile(index, this);
		blankTile.Location = removedTile.Location;
		InsertTileAt(blankTile, index);
	}

	private void ShuffleTiles() {
		Random ran = new Random();
		int numMoves = ran.Next(Constants.RANDOM_MOVE_THRESHOLD);
		List<Tile> moveChoices = new List<Tile>();

		for (int i = 0; i < numMoves; i++) {
			int blankIdx = BlankTile.CurrentIndex;
			if (blankIdx - 1 >= 0) {
				moveChoices.Add(GetTileAt(blankIdx - 1));
			}
			if (blankIdx - Dimension >= 0) {
				moveChoices.Add(GetTileAt(blankIdx - Dimension));
			}
			if (blankIdx + 1 < Controls.Count) {
				moveChoices.Add(GetTileAt(blankIdx + 1));
			}
			if (blankIdx + Dimension < Controls.Count) {
				moveChoices.Add(GetTileAt(blankIdx + Dimension));
			}
			int choice = ran.Next(moveChoices.Count);
			SwapTiles(BlankTile, moveChoices[choice]);
			moveChoices.Clear();
		}
	}

	public void RequestMove(Tile tile) {
		bool isLegalMove = true;
		if (IsBlankRow(tile)) {
			DoMoveHorizontal(tile);
		} else if (IsBlankColumn(tile)) {
			DoMoveVertical(tile);
		} else {
			isLegalMove = false;
		}

		if (isLegalMove) {
			if (GameComplete) {
				HandleGameWon();
			}
		}
	}

	private void DoMoveHorizontal(Tile tile) {
		DoMove(tile, GetMoveDirection(tile) * 1);
	}

	private void DoMoveVertical(Tile tile) {
		DoMove(tile, GetMoveDirection(tile) * Dimension);
	}

	private int GetMoveDirection(Tile tile) {
		int sign;
		if (BlankTile.CurrentIndex < tile.CurrentIndex) {
			sign = 1;
		} else {
			sign = -1;
		}
		return sign;
	}

	private void DoMove(Tile tile, int offset) {
		Tile other = null;
		do {
			int blankIndex = BlankTile.CurrentIndex;
			other = GetTileAt(blankIndex + offset);
			SwapTiles(BlankTile, other);
		} while (other.StartingIndex != tile.StartingIndex);
	}

	private void HandleGameWon() {
		Console.WriteLine("finished");
		GameInProgress = false;

		ResetTiles();
		((ItemPanel) Parent).RequestShowWin();
	}

	public Tile GetTileAt(int index) {
		return (Tile) Controls[index];
	}

	public Point GetTileStartingLocation(Tile tile) {
		int index = tile.StartingIndex;
		return new Point((index % dimension) * tile.Width, (index / dimension) * tile.Height);
	}

	public Tile ReplaceTileAt(Tile tile, int index) {
		Tile oldTile = GetTileAt(index);
		InsertTileAt(tile, index);
		return oldTile;
	}

	public void InsertTileAt(Tile tile, int index) {
		Controls.Add(tile);
		Controls.SetChildIndex(tile, index);
	}

	public Tile RemoveTileAt(int index) {
		Tile tile = GetTileAt(index);
		Controls.RemoveAt(index);
		return tile;
	}

	public void SwapTiles(Tile first, Tile second) {
		int firstIndex = first.CurrentIndex;
		int secondIndex = second.CurrentIndex;

		Point firstLocation = first.Location;
		Point secondLocation = second.Location;

		ReplaceTileAt(second, firstIndex);
		ReplaceTileAt(first, secondIndex);
		first.Location = secondLocation;
		second.Location = firstLocation;
	}

	private bool IsBlankRow(Tile tile) {
		bool isBlankRow = false;
		if (BlankTile != null) {
			isBlankRow = BlankTile.Row == tile.Row;
		}
		return isBlankRow;
	}

	private bool IsBlankColumn(Tile tile) {
		bool isBlankCol = false;
		if (BlankTile != null) {
			isBlankCol = BlankTile.Column == tile.Column;
		}
		return isBlankCol;
	}
}