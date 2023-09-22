﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Kalio.Domain
{
    public abstract class UpdateCommand
    {
        [Required]
        public string Id { get; set; }

        [MaxLength(512)]
        public string? Description { get; set; }

        [MaxLength(512)]
        public string? Comments { get; set; }

        public bool IsActive { get; set; } = true;

        //public string? CreatedBy { get; set; }
        [MaxLength(128)]
        public string CreatedByUserId { get; set; }
        public DateTimeOffset? DateCreated { get; set; } = DateTime.Now;



        //public string? UpdatedBy { get; set; }
        [MaxLength(128)]
        public string UpdatedByUserId { get; set; }
        public DateTimeOffset? DateUpdated { get; set; } = DateTime.Now;



        //public string? DeletedBy { get; set; }
        [MaxLength(128)]
        public string DeletedByUserId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTimeOffset? DateDeleted { get; set; }

        //[ConcurrencyCheck]
        public Guid RowVersion { get; set; }

        //[Timestamp]
        //public byte[] RowVersion { get; set; }

        [MaxLength(512)]
        [Browsable(false)]
        public string? FullText { get; set; }

        [MaxLength(256)]
        //public List<string> Tags { get; set; }
        public string? Tags { get; set; }

        [MaxLength(256)]
        public string? Caption { get; set; } //computed column

    }
}
