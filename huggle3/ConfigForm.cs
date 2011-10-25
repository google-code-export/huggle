﻿//This is a source code or part of Huggle project
//
//This file contains code for
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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace huggle3
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        ///<summary>
        ///Localize
        ///</summary>
        private void Localize()
        {
            Core.History("Configuration.Localize()");
            this.Text = Languages.Get("config-title");
            this.bSave.Text = Languages.Get("ok");
            this.Cancel.Text = Languages.Get("cancel");
            this.groupBox1.Text = Languages.Get("config-general");
            this.groupBox2.Text = Languages.Get("config-interface");
            this.groupBox3.Text = Languages.Get("config-keyboard");
            this.groupBox4.Text = Languages.Get("config-editing");
            this.groupBox5.Text = Languages.Get("config-reverting");
            this.groupBox6.Text = Languages.Get("config-reporting");
            this.groupBox7.Text = Languages.Get("config-templates-id");
            this.groupBox8.Text = Languages.Get("config-editor");
            this.groupBox9.Text = Languages.Get("config-admin");
        }

        public void Tab(int key)
        {
            this.groupBox1.Visible = false;
            this.groupBox2.Visible = false;
            this.groupBox3.Visible = false;
            this.groupBox4.Visible = false;
            this.groupBox5.Visible = false;
            this.groupBox6.Visible = false;
            this.groupBox7.Visible = false;
            this.groupBox8.Visible = false;
            this.groupBox9.Visible = false;
            switch (key)
            {
                case 0:
                    this.groupBox1.Visible = true;
                    break;
                case 1:
                    this.groupBox2.Visible = true;
                    break;
                case 2:
                    this.groupBox3.Visible = true;
                    break;
                case 3:
                    this.groupBox4.Visible = true;
                    break;
                case 4:
                    this.groupBox5.Visible = true;
                    break;
                case 5:
                    this.groupBox6.Visible = true;
                    break;
                case 6:
                    this.groupBox7.Visible = true;
                    break;
                case 7:
                    this.groupBox8.Visible = true;
                    break;
                case 8:
                    this.groupBox9.Visible = true;
                    break;
            }
        }

        public void Config_Load()
        {
            //Load all config values
            Core.History("Configuration.Config_Load()");
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigForm_Load(object sender, EventArgs e)
        {
            Localize();
            Config_Load();
            listView1.Items.Add("general", Languages.Get("config-general"), 0);
            listView1.Items.Add("interface", Languages.Get("config-interface"), 0);
            listView1.Items.Add("keyboard", Languages.Get("config-keyboard"), 0);
            listView1.Items.Add("editing", Languages.Get("config-editing"), 0);
            listView1.Items.Add("reverting", Languages.Get("config-reverting"), 0);
            listView1.Items.Add("reporting", Languages.Get("config-reporting"), 0);
            listView1.Items.Add("templates", Languages.Get("config-templates-id"), 0);
            listView1.Items.Add("editor", Languages.Get("config-editor"), 0);
            listView1.Items.Add("admin", Languages.Get("config-admin"), 0);
            int dw = 560, dh = 320;
            groupBox1.Left = 200;
            groupBox1.Top = 25;
            groupBox3.Left = 200;
            groupBox3.Top = 25;
            groupBox2.Left = 200;
            groupBox2.Top = 25;
            groupBox4.Left = 200;
            groupBox4.Top = 25;
            groupBox5.Left = 200;
            groupBox5.Top = 25;
            groupBox6.Left = 200;
            groupBox6.Top = 25;
            groupBox7.Left = 200;
            groupBox7.Top = 25;
            groupBox8.Left = 200;
            groupBox8.Top = 25;
            groupBox9.Left = 200;
            groupBox9.Top = 25;
            groupBox1.Height = dh;
            groupBox1.Width = dw;
            groupBox2.Height = dh;
            groupBox2.Width = dw;
            groupBox3.Height = dh;
            groupBox3.Width = dw;
            groupBox4.Height = dh;
            groupBox4.Width = dw;
            groupBox5.Height = dh;
            groupBox5.Width = dw;
            groupBox6.Height = dh;
            groupBox6.Width = dw;
            groupBox7.Height = dh;
            groupBox7.Width = dw;
            groupBox8.Height = dh;
            groupBox8.Width = dw;
            groupBox9.Height = dh;
            groupBox9.Width = dw;

            Tab(0);
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            Core.History("Configuration.bSave_Click()");
            Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Core.History("listView1_SelectedIndexChanged()");
            try
            {
                //ta
                foreach (ListViewItem it in listView1.SelectedItems)
                {
                    Tab(it.Index);
                }
            }
            catch (Exception ex)
            {
                if (Config.devs)
                {
                    // throw for debug info
                    Core.ExceptionHandler(ex);
                }
            }
        }
    }
}
