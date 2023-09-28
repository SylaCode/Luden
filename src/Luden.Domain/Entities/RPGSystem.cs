﻿using Luden.Domain.Core.Models;

namespace Luden.Domain.Entities
{
    public class RpgSystem : BaseEntity, IAuditableEntity, ISoftDeleteEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Config { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
    }
}