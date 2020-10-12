using System;
using System.Collections.Generic;
using System.Text;
using EE.Ord.Domain.MasterData.PatientFiles;
using Microsoft.EntityFrameworkCore;

namespace EE.Ord.Shared.Database.Main
{
    partial class StorageBroker
    {
        public DbSet<SimplePatientNote> SimplePatientNotes { get; set; }
    }
}
