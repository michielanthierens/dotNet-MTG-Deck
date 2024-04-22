using Howest.MagicCards.DAL.DBContext;
using Howest.MagicCards.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howest.MagicCards.DAL.Repositories;

public class SqlCardRepository : ICardRepository
{
    private readonly MtgContext _db;

    public SqlCardRepository(MtgContext myMtgContext)
    {
        _db = myMtgContext;
    }

    public IQueryable<Card> getAllCards()
    {
        IQueryable<Card> AllCards = _db.Cards.Select(c => c);        
        return AllCards;
    }
}
