using DataModel;
using LayeredPlugin.DataAccess;
using LayeredPlugin.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayeredPlugin.BusinessLogic
{
    public class ContactBL
    {
        private IOrganizationService passThroughOnlyOrganizationService;

        public ContactBL(IOrganizationService inputOrganizationService)
        {
            passThroughOnlyOrganizationService = inputOrganizationService;
        }

        public ValidationResult ValidateContactCreation(Contact inputContact)
        {
            if (!MaximumContactPerAccountIsValid(inputContact))
            {
                return new ValidationResult()
                {
                    HasError = true,
                    errorMessage = "Can't add more than 5 contacts per Account"
                };
            }
            return new ValidationResult();
        }

        private bool MaximumContactPerAccountIsValid(Contact inputContact)
        {
            var dal = new ContactDAL(passThroughOnlyOrganizationService);
            int totalContact = dal.GetNumberOfContactsInAccount(inputContact.AccountId.Id);
            return totalContact < 5;
        }
    }
}
