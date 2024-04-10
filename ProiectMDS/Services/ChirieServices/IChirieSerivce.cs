﻿using ProiectMDS.Models.DTOs;

namespace ProiectMDS.Services
{
    public interface IChirieService
    {
        Task AddChirie(ChirieDTO chirieDTO);
        Task DeleteChirie(int id);
    }
}