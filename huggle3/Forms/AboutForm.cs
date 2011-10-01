﻿//This is a source code or part of Huggle project
//
//This file contains code for
//last modified by Addshore

//Copyright (C) 2011 Huggle team
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace huggle3.Forms
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        public bool Localize()
        {
            // localize form
            Core.History("About.Localize()");
            this.Text = Languages.Get("about-form");
            return true;
        }


        private void AboutForm_Load(object sender, EventArgs e)
        {
            Core.History("AboutForm_Load()");
            Localize();
        }
    }
}
