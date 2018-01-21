using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleFunctionPlugin
{
    public class Contact_PreCreate : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IPluginExecutionContext pluginExecutionContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationService organizationService = factory.CreateOrganizationService(pluginExecutionContext.UserId);

            Model.ServiceContext context = new Model.ServiceContext(organizationService);

            Model.Contact TargetEntity = (Model.Contact)pluginExecutionContext.InputParameters["Target"];
            var totalContacts = context.ContactSet.Count(c => c.AccountId.Id == TargetEntity.AccountId.Id);
            if (totalContacts >= 5)
            {
                throw new InvalidPluginExecutionException("Can't add more than 5 contacts to an account");
            }

        }
    }
}
