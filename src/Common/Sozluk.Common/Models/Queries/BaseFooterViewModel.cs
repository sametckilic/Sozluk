using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sozluk.Common.Models.Queries;

public class BaseFooterRateViewModel
{
    public VoteType VoteType { get; set; }

}

public class BaseFooterFavoritedViewModel
{
    public bool IsFavorited { get; set; }
    public int FavoritedCount { get; set; }
}

public class BaseFooterRateFavoriteViewModel : BaseFooterFavoritedViewModel
{
    public VoteType VoteType { get; set; }
}