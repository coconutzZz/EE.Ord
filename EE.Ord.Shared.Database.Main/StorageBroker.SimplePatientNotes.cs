using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EE.Ord.Domain.MasterData;
using EE.Ord.Domain.MasterData.PatientFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EE.Ord.Shared.Database.Main
{
    partial class StorageBroker
    {
        public DbSet<SimplePatientNote> SimplePatientNotes { get; set; }
        public async ValueTask<SimplePatientNote> InsertSimplePatientNoteAsync(SimplePatientNote simplePatientNote)
        {
            EntityEntry<SimplePatientNote> simplePatientNoteEntityEntry = await SimplePatientNotes.AddAsync(simplePatientNote);
            await SaveChangesAsync();

            return simplePatientNoteEntityEntry.Entity;
        }

        public async ValueTask<SimplePatientNote> SelectSimplePatientNoteByPatientIdAsync(Guid patientId)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await SimplePatientNotes.FirstOrDefaultAsync(x => x.Patient.PatientId == patientId);

        }

        public async ValueTask<SimplePatientNote> UpdateSimplePatientNoteAsync(SimplePatientNote simplePatientNote)
        {
            EntityEntry<SimplePatientNote> simplePatientNoteEntityEntry = SimplePatientNotes.Update(simplePatientNote);
            await SaveChangesAsync();

            return simplePatientNoteEntityEntry.Entity;
        }

        public async ValueTask<SimplePatientNote> DeleteSimplePatientNoteAsync(SimplePatientNote simplePatientNote)
        {
            EntityEntry<SimplePatientNote> simplePatientNoteEntityEntry = SimplePatientNotes.Remove(simplePatientNote);
            await this.SaveChangesAsync();

            return simplePatientNoteEntityEntry.Entity;
        }
    }
}
