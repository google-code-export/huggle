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
using System.Text;

namespace huggle3.Requests
{
    public static class request_read 
    {
        public class diff : request_core.Request
        {
            public Controls.SpecialBrowser browsertab;
            public int Request_Count;
            public int Preload_Count;
            public int MaxSimultaneousR = 20;
            public edit _Edit;
            public override void Process()
            {
                Request_Count--;

                if (Request_Count >= MaxSimultaneousR)
                {
                    ThreadDone();
                    return;
                }

                if (browsertab == null)
                {
                    browsertab = main._CurrentBrowser;
                }
                _Edit.DiffCacheState = edit.CacheState.Caching;
                string Old;
                Old = "prev";
                if (_Edit.Oldid != "-1")
                {
                    Old = _Edit.Oldid;
                }

                string QueryString;

                //QueryString = 
            }
        }
        public class blocklog : request_core.Request
        {
            public user User;
            public override void Process()
            {
            
            }
        }
    }
}
