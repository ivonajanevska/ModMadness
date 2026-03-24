using ModMadnessDomain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModMadnessRepository.Interface
{
    public interface IUserRepository
    {
        MadnessUser? GetUserById(string id);
    }
}
