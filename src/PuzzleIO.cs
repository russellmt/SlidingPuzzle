using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public sealed class PuzzleIO {

	public static string[] ReadAllThemes() {
		string themeDirectory = Constants.THEMES_PATH;
		string[] themes = Directory.GetDirectories(themeDirectory);

		int index = 0;
		foreach (string theme in themes) {
			themes[index] = Path.GetFileName(theme);
			index++;
		}
		return themes;
	}
	
	public static string ReadCurrentTheme() {
		string currentFileName = Constants.CURRENT_THEME_FILE;
		string currentTheme = Constants.DEFAULT_THEME;

		if (File.Exists(currentFileName)) {
			currentTheme = File.ReadAllText(currentFileName);
		}
		return currentTheme;
	}

	public static void WriteCurrentTheme(string theme) {
		string currentFileName = Constants.CURRENT_THEME_FILE;

		if (File.Exists(currentFileName)) {
			File.WriteAllText(currentFileName, theme);
		}
	}

	public static string[] ReadThemeImageNames() {
		return ReadThemeImageNames(ReadCurrentTheme());
	}

	public static string[] ReadThemeImageNames(string theme) {
		string imageDirectory = Constants.THEMES_PATH + theme;
		if (!Directory.Exists(imageDirectory)) {	//stops program from crashing if selected theme was deleted
			imageDirectory = Constants.THEMES_PATH + Constants.DEFAULT_THEME;
		}
		return Directory.GetFiles(imageDirectory);
	}

	public static void DeleteTheme(string theme) {
		string themeDirectory = Constants.THEMES_PATH + theme + Constants.SLASH;
		if (Directory.Exists(themeDirectory)) {
			Directory.Delete(themeDirectory, true);
		}
	}

	public static void WriteThemeImage(string theme, Image image, int index) {
		string themeDirectory = Constants.THEMES_PATH + theme + Constants.SLASH;
		Directory.CreateDirectory(themeDirectory);

		string indexString = index.ToString("D2");
		string imageFileName = themeDirectory + indexString + Constants.IMAGE_EXTENSION;
		image.Save(imageFileName, ImageFormat.Png);
	}
}