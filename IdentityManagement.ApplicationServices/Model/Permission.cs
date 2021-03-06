﻿using System;
using Affecto.IdentityManagement.Interfaces.Model;

namespace Affecto.IdentityManagement.ApplicationServices.Model
{
    internal class Permission : IPermission
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}