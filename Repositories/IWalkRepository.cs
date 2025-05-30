﻿using System.Runtime.InteropServices;
using nzWalksApi.Models.Domain;
using nzWalksApi.Models.DTO;

namespace nzWalksApi.Repositories
{
    public interface IWalkRepository
    {
        public Task<Walk> CreateAsync(Walk walk);
        public Task<List<Walk>> GetAllAsync(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool isAscending = true,
            int pageNumber = 1,
            int pageSize = 1000
        );
        public Task<Walk?> GetWalkByIdAsync(Guid id);

        public Task<Walk?> UpdateWalk(Walk walk, Guid Id);

        public Task<Walk?> DeleteWalkByIdAsync(Guid Id);
    }
}
