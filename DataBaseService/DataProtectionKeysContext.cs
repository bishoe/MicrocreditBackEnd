﻿using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseService
{
    public class DataProtectionKeysContext : DbContext, IDataProtectionKeyContext
    {
        public DataProtectionKeysContext(DbContextOptions<DataProtectionKeysContext> options) : base(options) { }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    }
}

