//-----------------------------------------------------------------------------------
// <copyright file="MenusExtensions.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Al.vNext.Core.ViewModel;
using Al.vNext.ViewModel.Enum;

namespace Al.vNext.ViewModel
{
    public static class MenusExtensions
    {
        public static IList<string> GetMenus<TViewModel>(this TViewModel model, Func<TViewModel, IList<MenuTypeEnum>> meduIdListGenerator) where TViewModel : IViewModel
        {
            return meduIdListGenerator(model).Select(x => x.ToString()).ToList();
        }

        public static TViewModel SetMenus<TViewModel>(this TViewModel model, IList<string> menus) where TViewModel : IViewModel
        {
            if (model != null)
            {
                model.Menus = menus;
            }

            return model;
        }

        public static TViewModel SetMenus<TViewModel>(this TViewModel model, Func<TViewModel, IList<MenuTypeEnum>> meduIdListGenerator) where TViewModel : IViewModel
        {
            if (model != null)
            {
                model.Menus = model.GetMenus(meduIdListGenerator);
            }

            return model;
        }
    }
}