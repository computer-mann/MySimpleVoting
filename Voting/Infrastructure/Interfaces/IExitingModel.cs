using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voting.Models.ViewModels;

namespace Voting.Infrastructure.Interfaces
{
   public interface IExitingModel
    {
         void AddSelectedVotesPosts(SelectedVotesPostModel s);
        List<SelectedVotesPostModel> GetSelectedVotesPosts();

    }
}
