﻿//
// Elements.cs: defines the various components of our view
//
// Author:
//   Miguel de Icaza (miguel@gnome.org)
//
// Copyright 2010, Novell, Inc.
//
// Code licensed under the MIT X11 license
//
// TODO: StyledStringElement: merge with multi-line?
// TODO: StyledStringElement: add image scaling features?
// TODO: StyledStringElement: add sizing based on image size?
// TODO: Move image rendering to StyledImageElement, reason to do this: the image loader would only be imported in this case, linked out otherwise
//
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.Drawing;
using MonoTouch.Foundation;
using MonoMobile.Dialog.Utilities;
using MonoTouch.CoreAnimation;

namespace MonoMobile.Dialog
{
	public partial class RadioElement : StringElement
	{

		public override UITableViewCell GetCell(UITableView tv)
		{
			var cell = base.GetCell(tv);
			var root = (RootElement)Parent.Parent;

			if (!(root.group is RadioGroup))
				throw new Exception("The RootElement's Group is null or is not a RadioGroup");

			bool selected = RadioIdx == ((RadioGroup)(root.group)).Selected;
			cell.Accessory = selected ? UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None;

			return cell;
		}

		public override void Selected(DialogViewController dvc, UITableView tableView, NSIndexPath indexPath)
		{
			RootElement root = (RootElement)Parent.Parent;
			if (RadioIdx != root.RadioSelected)
			{
				UITableViewCell cell;
				var selectedIndex = root.PathForRadio(root.RadioSelected);
				if (selectedIndex != null)
				{
					cell = tableView.CellAt(selectedIndex);
					if (cell != null)
						cell.Accessory = UITableViewCellAccessory.None;
				}
				cell = tableView.CellAt(indexPath);
				if (cell != null)
					cell.Accessory = UITableViewCellAccessory.Checkmark;
				root.RadioSelected = RadioIdx;
			}

			base.Selected(dvc, tableView, indexPath);
		}
	}

}