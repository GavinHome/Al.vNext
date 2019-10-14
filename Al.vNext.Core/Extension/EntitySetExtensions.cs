//-----------------------------------------------------------------------------------
// <copyright file="EntitySetExtensions.cs" company="Al.vNext">
//     Copyright Al.vNext. All rights reserved.
// </copyright>
// <author>??</author>
// <date>2019/10/14 11:12:51</date>
// <description></description>
//-----------------------------------------------------------------------------------

using System;
using Al.vNext.Core.Const;
using Al.vNext.Core.Entity;
using Al.vNext.Core.Enum;
using Al.vNext.Core.Extension;

namespace Al.vNext.Core.Extension
{
    public static class EntitySetExtensions
    {
        public static void SetEntityPrincipal(this object value, System.Security.Claims.ClaimsPrincipal user)
        {            
            var code = user.FindFirst(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier);
            var organId = user.FindFirst(c => c.Type == JwtClaimNamesConst.Org);
            if (value is EntitySet t)
            {
                var isNew = t.IsNullOrEmpty();
                if (value is EntitySetWithCreate esc)
                {
                    if (isNew)
                    {
                        esc.CreateAt = DateTime.Now;
                        esc.CreateBy = code?.Value;
                        esc.DataStatus = DataStatusEnum.Valid;
                    }

                    if (value is EntitySetWithCreateAndUpdate escu)
                    {
                        escu.UpdateAt = DateTime.Now;
                        escu.UpdateBy = code?.Value;
                    }

                    if (value is EntitySetWithAllStatus ess)
                    {
                        if (isNew)
                        {
                            Guid? organizationId = null;
                            if (!string.IsNullOrEmpty(organId?.Value) && Guid.TryParse(organId?.Value, out Guid tempId))
                            {
                                organizationId = tempId;
                            }
                            
                            ess.CreateByOrganizationId = organizationId;
                        }

                        if (ess.ApprovalStatus == ApproveStatusEnum.Auditing)
                        {
                            ess.SubmitAt = DateTime.Now;
                        }
                    }
                }
            }
        }
    }
}
