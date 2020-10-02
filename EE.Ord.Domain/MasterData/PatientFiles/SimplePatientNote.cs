using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EE.Ord.Infrastructure.EntityFramework;

namespace EE.Ord.Domain.MasterData.PatientFiles
{
    public class SimplePatientNote : EntityWithAuditing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SimplePatientNoteId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
