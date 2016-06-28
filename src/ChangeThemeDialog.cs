using System;
using System.Windows.Forms;

public class ChangeThemeDialog : PuzzleListDialog {

	protected override string GetTitleText() {
		return Constants.CHANGE_THEME;
	}

	protected override bool DoApplyChanges() {
		PuzzleIO.WriteCurrentTheme((string) list.SelectedItem);
		return true;
	}

	protected override void DoRefreshList() {
		string currentTheme = PuzzleIO.ReadCurrentTheme();
		string[] themes = PuzzleIO.ReadAllThemes();

		int index = 0;
		foreach (string theme in themes) {
			list.Items.Add(theme);
			if (theme.Equals(currentTheme)) {
				list.SetSelected(index, true);
			}
			index++;
		}
	}
}