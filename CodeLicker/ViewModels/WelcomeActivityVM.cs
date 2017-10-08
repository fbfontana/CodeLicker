using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeLicker.ViewModels;

namespace CodeLicker.ViewModels
{
    public class WelcomeActivityVM : ActivityVM
    {
        #region Constants
        const string ActivityName = "WelcomeActivity";
        #endregion
        public WelcomeActivityVM()
        {
            TabName = nameof(ActivityName);
        }
    }
}
