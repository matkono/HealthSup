﻿using HealthSup.Domain.Entities;
using System.Threading.Tasks;

namespace HealthSup.Domain.Repositories
{
    public interface IMedicalAppointmentRepository
    {
        Task<MedicalAppointment> GetById
        (
            int id
        );
    }
}