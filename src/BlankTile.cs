using System;
using System.Drawing;
using System.Windows.Forms;

public class BlankTile : Tile {
	
	public BlankTile (int startingIndex, PuzzlePanel puzzle) : base(startingIndex, null, puzzle) {}

	// this method does nothing in the blank tile
	protected override void InitializeImage() {}

	public override void HandleClick(Object sender, EventArgs args) {}
}