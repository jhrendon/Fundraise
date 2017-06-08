﻿using System;
using System.Collections.Generic;
using System.Linq;
using Fundraise.Core.Entities;

namespace Fundraise.Core.Services
{
    public class CampaignRepository : ICampaignRepository
    {
        private FundraiseContext _context;

        public CampaignRepository(FundraiseContext context)
        {
            _context = context;
        }

        public IEnumerable<Campaign> FindByName(string name)
        {
            return _context.Campaigns.Where(x => x.Name == name).OrderBy(c => c.Name).ToList();
        }

        public bool Exists(string name)
        {
            return _context.Campaigns.Where(x => x.Name == name).First() != null;
        }

        public IEnumerable<Campaign> GetAll()
        {
            return _context.Campaigns.OrderBy(c => c.Name).ToList();
        }

        public Campaign FindById(Guid id)
        {
            return _context.Campaigns.Find(id);
        }

        public void Close(Guid id)
        {
            var campaign = this.FindById(id);

            if (campaign == null)
                throw new Exception("Campaign not found");

            campaign.IsActive = false;
            _context.Update(campaign);
            _context.SaveChanges();
        }
    }
}