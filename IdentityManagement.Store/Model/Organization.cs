﻿using System;

namespace Affecto.IdentityManagement.Store.Model
{
    public class Organization
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDisabled { get; set; }
    }
}