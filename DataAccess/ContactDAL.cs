using LayeredPlugin.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayeredPlugin.DataAccess
{
    public class ContactDAL
    {
        private ServiceContext context;

        public ContactDAL(IOrganizationService organizationService)
        {
            context = new ServiceContext(organizationService);
        }

        public int GetNumberOfContactsInAccount(Guid accountId)
        {
            int numberOfContacts;
            numberOfContacts = context.ContactSet.Count(c => c.AccountId.Id == accountId);
            return numberOfContacts;
        }
    }
}
