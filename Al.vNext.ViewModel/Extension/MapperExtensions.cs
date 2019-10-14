//-----------------------------------------------------------------------------------
// <copyright file="MapperExtensions.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Al.vNext.Core.Entity;
using Al.vNext.Core.Extension;
using Al.vNext.Core.ViewModel;
using Al.vNext.ViewModel;

namespace Al.vNext.ViewModel
{
    public static class MapperExtensions
    {
        public static TModel ToModel<TModel>(this BaseInfo model) where TModel : class, IEntitySet
        {
            TModel t = Mapping.Default<BaseInfo, TModel>(model);
            if (model != null && t != null)
            {
                t.SetEntityPrincipal(model.User);
            }

            return t;
        }

        public static TModel ToModel<TModel>(this MongoBaseInfo model) where TModel : class, IEntitySet
        {
            TModel t = Mapping.Default<MongoBaseInfo, TModel>(model);
            if (model != null && t != null)
            {
                t.SetEntityPrincipal(model.User);
            }

            return t;
        }

        public static TViewModel ToViewModel<TViewModel>(this IEntitySet entity) where TViewModel : class
        {
            TViewModel t = Mapping.Default<IEntitySet, TViewModel>(entity);
            return t;
        }

        public static IList<TViewModel> ToViewModels<TModel, TViewModel>(this IList<TModel> models) where TModel : IEntitySet
        {
            IList<TViewModel> t = Mapping.Default<IList<TModel>, IList<TViewModel>>(models);
            return t;
        }

        public static IList<TModel> ToModels<TViewModel, TModel>(this IList<TViewModel> viewModels) where TViewModel : IViewModel
        {
            IList<TModel> t = Mapping.Default<IList<TViewModel>, IList<TModel>>(viewModels);
            foreach (var item in t)
            {
                if (t.IsNotNullOrEmpty())
                {
                    t.SetEntityPrincipal(viewModels.First().User);
                }
            }

            return t;
        }
    }
}