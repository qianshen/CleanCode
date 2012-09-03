using System;
using System.Collections.Generic;
using System.Text;
using Improsys.ContactImporter;

namespace EFSchools.Englishtown.Community.Common.ContactImport
{
    internal class ContactImporterFactory : IContactImporterFactory
    {
        static ContactImporterFactory _instance = null;

        private ContactImporterFactory()
        {
        }

        internal static ContactImporterFactory Instance
        {
            get
            {
                return _instance ?? (_instance = new ContactImporterFactory());
            }
        }

        public IContactImporter CreateContactImporter(string username, string password, string type)
        {
            var contactImporter = new ContactImporter(username, password, type);

            return new ContactImporterWrapper(contactImporter);
        }
    }
}
