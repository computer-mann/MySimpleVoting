using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voting.Infrastructure.Interfaces;
using Voting.Models.ViewModels;

namespace Voting.Infrastructure
{
    public class ExitingModel:IExitingModel
    {
        List<SelectedVotesPostModel> postModels = new List<SelectedVotesPostModel>();
        public ExitingModel()
        {
            
        }
        public void AddSelectedVotesPosts(SelectedVotesPostModel s)
        {
            postModels.Add(s);
        }
       public List<SelectedVotesPostModel> GetSelectedVotesPosts()
        {
            return postModels;
        }
    }
}
