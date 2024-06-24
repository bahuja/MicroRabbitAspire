
using MicroRabbit.Transfer.Domain.Interfaces;
using MicroRabbit.Transfer.Domain.Models;
using MicroRabbit.TransferData.Context;
using System.Collections.Generic;
using System.Linq;

namespace MicroRabbit.Transfer.Data.Repository
{

    /**
     * Transfer Repository class
     * 
     * @author D. P. Edwards
     * @license MIT
     * @version 1.0
     */ 
    public class TransferRepository : ITransferRepository
    {
        private readonly TransferDbContext _context;

        public TransferRepository(TransferDbContext context)
        {
            _context = context;
        }

        public void Add(TransferLog transferLog)
        {
            _context.TransferLogs.Add(transferLog);
            _context.SaveChanges();
        }

        public IEnumerable<TransferLog> GetTransferLogs()
        {
            var transferLogs= _context.TransferLogs;
            var list = transferLogs.ToList();
            return transferLogs;
        }
    }
}
