﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeLicker.Properties;
using CodeLicker.ViewModels;

namespace CodeLicker.ViewModels
{
    public class WelcomeActivityVM : ActivityVM
    {
        #region Constants
        readonly string ActivityName = Resources.Welcome;
        #endregion
        public WelcomeActivityVM()
        {
            TabName = ActivityName;
        }
    }
}
