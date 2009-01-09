﻿#region Common Public License Copyright Notice
/**************************************************************************\
* PhotoToys Clone                                                          *
*                                                                          *
* Copyright © Brice Lambson. All rights reserved.                          *
*                                                                          *
* The use and distribution terms for this software are covered by the      *
* Common Public License 1.0 (http://opensource.org/licenses/cpl1.0.php)    *
* which can be found in the file CPL.txt at the root of this distribution. *
* By using this software in any fashion, you are agreeing to be bound by   *
* the terms of this license.                                               *
*                                                                          *
* You must not remove this notice, or any other, from this software.       *
\**************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PhotoToys
{
	public partial class PhotoResizeForm : Form
	{
		private bool isAdvanced;

		public int PhotoWidth
		{
			get
			{
				if (smallRadioButton.Checked)
				{
					return 640;
				}

				if (mediumRadioButton.Checked)
				{
					return 800;
				}

				if (largeRadioButton.Checked)
				{
					return 1024;
				}

				if (handheldRadioButton.Checked)
				{
					return 240;
				}

				return Convert.ToInt32(customWidthTextBox.Text, CultureInfo.InvariantCulture);
			}
		}

		public int PhotoHeight
		{
			get
			{
				if (smallRadioButton.Checked)
				{
					return 480;
				}

				if (mediumRadioButton.Checked)
				{
					return 600;
				}

				if (largeRadioButton.Checked)
				{
					return 768;
				}

				if (handheldRadioButton.Checked)
				{
					return 320;
				}

				return Convert.ToInt32(customHeightTextBox.Text, CultureInfo.InvariantCulture);
			}
		}

		public bool SmallerOnly
		{
			get { return smallerOnlyCheckBox.Checked; }
		}

		public string FileNameAppendage
		{
			get
			{
				if (resizeOriginalCheckBox.Checked)
				{
					return String.Empty;
				}

				if (smallRadioButton.Checked)
				{
					return " (Small)";
				}

				if (mediumRadioButton.Checked)
				{
					return " (Medium)";
				}

				if (largeRadioButton.Checked)
				{
					return " (Large)";
				}

				if (handheldRadioButton.Checked)
				{
					return " (WinCE)";
				}

				return " (Custom)";
			}
		}

		public PhotoResizeForm()
		{
			InitializeComponent();
		}

		private void customRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			customLabel1.Enabled = customRadioButton.Checked;
			customWidthTextBox.Enabled = customRadioButton.Checked;
			customLabel2.Enabled = customRadioButton.Checked;
			customHeightTextBox.Enabled = customRadioButton.Checked;
			customLabel3.Enabled = customRadioButton.Checked;
		}

		private void advancedButton_Click(object sender, EventArgs e)
		{
			isAdvanced = !isAdvanced;

			customRadioButton.Visible = isAdvanced;
			customLabel1.Visible = isAdvanced;
			customWidthTextBox.Visible = isAdvanced;
			customLabel2.Visible = isAdvanced;
			customHeightTextBox.Visible = isAdvanced;
			customLabel3.Visible = isAdvanced;
			smallerOnlyCheckBox.Visible = isAdvanced;
			resizeOriginalCheckBox.Visible = isAdvanced;

			if (isAdvanced)
			{
				this.Height = 299;
				advancedButton.Text = "<< &Basic";
			}
			else
			{
				if (customRadioButton.Checked)
				{
					smallRadioButton.Checked = true;
				}

				customWidthTextBox.Text = "1200";
				customHeightTextBox.Text = "1024";
				smallerOnlyCheckBox.Checked = false;
				resizeOriginalCheckBox.Checked = false;

				this.Height = 227;
				advancedButton.Text = "&Advanced >>";
			}
		}

		private void PhotoResizeForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			int dummy;

			if (DialogResult != DialogResult.Cancel && customRadioButton.Checked && !(Int32.TryParse(customWidthTextBox.Text, out dummy) && Int32.TryParse(customHeightTextBox.Text, out dummy)))
			{
				MessageBoxOptions options = 0;

				if (RightToLeft == RightToLeft.Yes)
				{
					options = MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading;
				}

				MessageBox.Show("The custom size dimmensions must be numbers.\r\nPlease check that those text fields are numbers and try again.", "Picture Resizer", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, options);

				e.Cancel = true;
			}
		}
	}
}
