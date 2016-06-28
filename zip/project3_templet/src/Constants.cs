using System;

public sealed class Constants {

	//directory titles
	public static readonly string SLASH = "\\";
	public static readonly string RESOURCE_PATH = "res" + SLASH;
	public static readonly string CURRENT_THEME_FILE = RESOURCE_PATH + "current_theme";
	public static readonly string THEMES_PATH = RESOURCE_PATH + "themes" + SLASH;
	public static readonly string DEFAULT_THEME = "default";

	public static readonly string IMAGE_EXTENSION = ".png";

	//common button text
	public static readonly string OK = "OK";
	public static readonly string CANCEL = "Cancel";

	//form titles
	public static readonly string PUZZLE_TITLE = "Sliding Puzzle";
	public static readonly string NEW_GAME = "New Game";
	public static readonly string CHANGE_THEME = "Change Theme";
	public static readonly string ADD_THEME = "Add Theme";
	public static readonly string WIN_TITLE = "Winner";

	//other labels
	public static readonly string WIN_MESSAGE = "Congratulations! You won...";
	public static readonly string THEME_TITLE = "Theme title";

	//font families
	public static readonly string WIN_FONT_FAMILY = "Arial";

	//magic numbers
	public static readonly int BUTTON_WIDTH = 100;
	public static readonly int RANDOM_MOVE_THRESHOLD = 1000;

	public static readonly int WIN_DIALOG_WIDTH = 300;
	public static readonly int WIN_DIALOG_HEIGHT = 100;
	public static readonly int WIN_FONT_SIZE = 24;

	public static readonly int[] PUZZLE_SIZES = new int[4] {3, 4, 5, 6};
}