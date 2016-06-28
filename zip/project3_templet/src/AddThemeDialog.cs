using System;
using System.Drawing;
using System.Windows.Forms;

public class AddThemeDialog : PuzzleListDialog {

	private TextBox titleField;
	private OpenFileDialog imageSelectDialog;

	public AddThemeDialog() : base() {
		imageSelectDialog = ConstructImageSelectDialog();
	}

	private OpenFileDialog ConstructImageSelectDialog() {
		OpenFileDialog dialog = new OpenFileDialog();
		dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG";
		dialog.FilterIndex = 0;
		dialog.RestoreDirectory = true;
		return dialog;
	}

	protected override TextBox ConstructTitleField() {
		titleField = new TextBox();
		titleField.Text = Constants.THEME_TITLE;
		titleField.Width = 200;
		return titleField;
	}

	protected override string GetTitleText() {
		return Constants.ADD_THEME;
	}

	protected override bool DoApplyChanges() {
		bool isApplying = (imageSelectDialog.ShowDialog() == DialogResult.OK);
		if (isApplying) {
			Image sourceImage = Image.FromFile(imageSelectDialog.FileName);
			CreateThemeImages(sourceImage);
		}
		return isApplying;
	}

	private void CreateThemeImages(Image sourceImage) {
		int dimension = Constants.PUZZLE_SIZES[list.SelectedIndex];
		int fragmentWidth = (int) ((double) sourceImage.Width / dimension);
		int fragmentHeight = (int) ((double) sourceImage.Height / dimension);
		int numImages = dimension * dimension;

		string theme = titleField.Text;
		PuzzleIO.DeleteTheme(theme);

		Graphics g;
		Bitmap bitmap;
		for (int i = 0; i < numImages; i++) {
			int horIndex = i % dimension;
			int vertIndex = i / dimension;

			bitmap = new Bitmap(fragmentWidth, fragmentHeight);
			g = Graphics.FromImage(bitmap);
        	g.DrawImage(sourceImage,
        		new Rectangle(0, 0, fragmentWidth, fragmentHeight),
        		new Rectangle(horIndex * fragmentWidth, vertIndex * fragmentHeight, fragmentWidth, fragmentHeight),
        		GraphicsUnit.Pixel);
        	g.Dispose();

        	PuzzleIO.WriteThemeImage(theme, bitmap, i);
		}
	}

	protected override void DoRefreshList() {
		int[] sizes = Constants.PUZZLE_SIZES;
		foreach (int size in sizes) {
			string sizeString = size + " x " + size;
			list.Items.Add(sizeString);
		}
		list.SetSelected(0, true);
	}
}