﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.IService;
using ZSZ.Service.Entities;

namespace ZSZ.Service
{
    public class AdminLogService : IAdminLogService
    {
        public void AddNew(long adminUserId, string message)
        {
            using (ZSZDbContext ctx = new ZSZDbContext())
            {
                ctx.AdminUserLogs
                    .Add(new AdminLogEntity() { AdminUserId=adminUserId,Message=message});
                ctx.SaveChanges();
            }
        }
    }
}
