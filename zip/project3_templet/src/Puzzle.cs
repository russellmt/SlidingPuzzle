using System;
using System.Windows.Forms;

//top level control for puzzle

public class Puzzle : Form {
	
	[STAThread]
	public static void Main() {
		Puzzle puzzle = new Puzzle();
		puzzle.Build();
		Application.EnableVisualStyles();
		Application.Run(puzzle);
	}

	public void Build() {
		ItemPanel panel = new ItemPanel();

		Text = Constants.PUZZLE_TITLE;
		AutoSize = true;
		FormBorderStyle = FormBorderStyle.FixedSingle;
		Controls.Add(panel);
	}
}