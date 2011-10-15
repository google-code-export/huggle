﻿//This is a source code or part of Huggle project
//
//This file contains code for browser object
//last modified by Petrb

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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace huggle3.Controls
{
    public partial class SpecialBrowser
    {
        public List<Core.HistoryItem> History;

        public void Browser_Navigating(Object b, WebBrowserNavigatingEventArgs x)
        {
            Core.History("Navigating()");
            if (x.Url.ToString() == "about:blank")
            {
                return;
            }

        }

        public void AddToHistory(Core.HistoryItem i)
        {
            
        }

        public SpecialBrowser()
        {
            InitializeComponent();
        }
    }
}
