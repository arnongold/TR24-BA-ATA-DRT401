using LayeredPlugin.BusinessLogic;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayeredPlugin.PluginEndpoint
{
    public class Contact_PreCreate : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IPluginExecutionContext pluginExecutionContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationService organizationService = factory.CreateOrganizationService(pluginExecutionContext.UserId);

            Model.Contact TargetEntity = (Model.Contact)pluginExecutionContext.InputParameters["Target"];

            ContactBL bl = new ContactBL(organizationService);
            var result = bl.ValidateContactCreation(TargetEntity);
            if (result.HasError)
            {
                throw new InvalidPluginExecutionException(result.errorMessage);
            }
        }
    }
}
