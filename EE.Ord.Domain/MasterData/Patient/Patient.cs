using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EE.Ord.Domain.MasterData.PatientFiles;
using EE.Ord.Infrastructure.EntityFramework;

namespace EE.Ord.Domain.MasterData
{
    public class Patient : EntityWithAuditing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PatientId { get; set; }

        [Required]
        public long InsuranceNumber { get; set; } // Versicherungsnummer

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public IEnumerable<SimplePatientNote> SimplePatientNotes { get; set; }
    }
}
