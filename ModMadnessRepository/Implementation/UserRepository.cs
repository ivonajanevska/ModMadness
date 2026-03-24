using Microsoft.EntityFrameworkCore;
using ModMadnessDomain.Identity;
using ModMadnessRepository.Interface;
using ModMadnessRepository.Data;
using ModMadnessDomain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModMadnessRepository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<MadnessUser> entities;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<MadnessUser>();
        }
        public MadnessUser? GetUserById(string id)
        {
            return entities.FirstOrDefault(user => user.Id == id);
        }
    }
}
