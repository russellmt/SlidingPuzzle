using System;
using System.Windows.Forms;

public class ItemPanel : FlowLayoutPanel {

	private PuzzlePanel puzzlePanel;
	private AddThemeDialog addThemeDialog;
	private ChangeThemeDialog changeThemeDialog;
	
	public ItemPanel() : base() {
		puzzlePanel = new PuzzlePanel();
		addThemeDialog = new AddThemeDialog();
		changeThemeDialog = new ChangeThemeDialog();
		Build();
	}

	private void Build() {
		AutoSize = true;

		Button newGameButton = new Button();
		newGameButton.Text = Constants.NEW_GAME;
		newGameButton.Width = Constants.BUTTON_WIDTH;
		newGameButton.Click += HandleNewGame;
		Controls.Add(newGameButton);

		Button addThemeButton = new Button();
		addThemeButton.Text = Constants.ADD_THEME;
		addThemeButton.Width = Constants.BUTTON_WIDTH;
		addThemeButton.Click += HandleAddTheme;
		Controls.Add(addThemeButton);

		Button changeThemeButton = new Button();
		changeThemeButton.Text = Constants.CHANGE_THEME;
		changeThemeButton.Width = Constants.BUTTON_WIDTH;
		changeThemeButton.Click += HandleChangeTheme;
		Controls.Add(changeThemeButton);

		Controls.Add(puzzlePanel);
	}

	public void HandleNewGame(Object sender, EventArgs args) {
		puzzlePanel.DoNewGame();
	}

	public void HandleAddTheme(Object sender, EventArgs args) {
		addThemeDialog.ShowDialog();
	}

	public void HandleChangeTheme(Object sender, EventArgs args) {
		changeThemeDialog.ShowDialog();
		if (changeThemeDialog.ConsumeChange()) {
			puzzlePanel.GameInProgress = false;
			puzzlePanel.ResetTiles();
			Invalidate();
		}
	}

	public void RequestShowWin() {
		MessageBox.Show(Constants.WIN_MESSAGE, Constants.WIN_TITLE, MessageBoxButtons.OK);
	}
}