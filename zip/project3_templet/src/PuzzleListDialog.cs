using System;
using System.Windows.Forms;

public abstract class PuzzleListDialog : Form {
	
	protected ListBox list;
	protected bool changeAccepted;

	public PuzzleListDialog() : base() {
		Build();
	}

	private void Build() {
		list = new ListBox();
		list.Width = 200;
		list.Height = 250;

		Button okButton = new Button();
		okButton.Text = Constants.OK;
		okButton.Click += HandleOK;

		Button cancelButton = new Button();
		cancelButton.Text = Constants.CANCEL;
		cancelButton.Click += HandleCancel;

		FlowLayoutPanel panel = new FlowLayoutPanel();
		panel.Width = 250;
		panel.Height = 300;
		panel.Controls.Add(list);

		TextBox titleField = ConstructTitleField();
		if (titleField != null) {
			panel.Controls.Add(titleField);
		}

		panel.Controls.Add(okButton);
		panel.Controls.Add(cancelButton);

		Text = GetTitleText();
		AutoSize = true;
		FormBorderStyle = FormBorderStyle.FixedSingle;
		Controls.Add(panel);
	}

	protected virtual TextBox ConstructTitleField() {
		return null;
	}

	protected abstract string GetTitleText();

	public void HandleOK(Object sender, EventArgs args) {
		if (DoApplyChanges()) {
			changeAccepted = true;
			Hide();
		}
	}

	protected abstract bool DoApplyChanges();

	public void HandleCancel(Object sender, EventArgs args) {
		Hide();
	}

	public bool ConsumeChange() {
		bool isChanged = changeAccepted;
		changeAccepted = false;
		return isChanged;
	}

	protected override void OnShown(EventArgs args) {
		base.OnShown(args);
		RefreshListedItems();
	}

	private void RefreshListedItems() {
		list.BeginUpdate();
		list.Items.Clear();
		DoRefreshList();
		list.EndUpdate();
	}

	protected abstract void DoRefreshList();
}