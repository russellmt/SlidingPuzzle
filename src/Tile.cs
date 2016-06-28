using System;
using System.Drawing;
using System.Windows.Forms;

public class Tile : PictureBox {

	protected int startingIndex;
	protected string imageName;
	protected PuzzlePanel puzzle;
	
	public Tile (int startingIndex, string imageName, PuzzlePanel puzzle) : base() {
		this.startingIndex = startingIndex;
		this.imageName = imageName;
		this.puzzle = puzzle;
		Build();
	}

	public int StartingIndex {
		get { return startingIndex; }
	}

	public int CurrentIndex {
		get { return puzzle.Controls.IndexOf(this); }
	}

	public int Row {
		get { return CurrentIndex / puzzle.Dimension; }
	}

	public int Column {
		get { return CurrentIndex % puzzle.Dimension; }
	}

	public string ImageName {
		get { return imageName; }
	}

	public PuzzlePanel Puzzle {
		get { return puzzle; }
	}

	protected void Build() {
		InitializeImage();
		Click += HandleClick;
	}

	protected virtual void InitializeImage() {
		Image = Image.FromFile(imageName);
		Width = Image.Width;
		Height = Image.Height;
	}

	public virtual void HandleClick(Object sender, EventArgs args) {
		puzzle.RequestMove(this);
	}

	
}