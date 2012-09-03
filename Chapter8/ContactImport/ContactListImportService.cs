using System;
using System.Collections.Generic;
using Improsys.ContactImporter;

namespace EFSchools.Englishtown.Community.Common.ContactImport
{

    public class ContactListImportService : IContactListImportService
    {
        IContactImporterFactory _contactImporterFactory;
        private static IContactListImportService _instance = null;

        internal ContactListImportService(IContactImporterFactory contactImporterFactory)
        {
            _contactImporterFactory = contactImporterFactory;
        }

        public static IContactListImportService Instance
        {
            get { return _instance ?? (_instance = new ContactListImportService(ContactImporterFactory.Instance)); }
        }

        public IList<EmailContact> CreateDefaultEmailContacts(int numberOfContacts)
        {
            var res = new List<EmailContact>();
            for (int i = 1; i <= numberOfContacts; i++)
            {
                EmailContact c = new EmailContact();
                c.Name = String.Format("Name {0}", i);
                c.Email = String.Format("user{0}@somemail.com", i);
                res.Add(c);
            }
            return res;
        }

        public ImportResult GetContacts(string username, string password, string emailDomain, out IList<EmailContact> contacts)
        {
            ImportResult result = ImportResult.NotSet;
            var importedContacts = new List<EmailContact>();

            contacts = null;
            try
            {
                using (var ci = _contactImporterFactory.CreateContactImporter(username, password, emailDomain))
                {
                    ci.login();

                    if (ci.logged_in)
                    {
                        ci.getcontacts();

                        for (int i = 0; i < ci.emailArray.Length; i++)
                        {
                            if (string.IsNullOrEmpty(ci.emailArray[i]))
                            {
                                continue;
                            }

                            EmailContact contact = new EmailContact();

                            contact.Email = ci.emailArray[i];
                            if (ci.nameArray.Length > i)
                            {
                                contact.Name = ci.nameArray[i];
                            }
                            importedContacts.Add(contact);
                        }
                        contacts = importedContacts;
                        result = ImportResult.Valid;
                    }
                    else
                    {
                        result = ImportResult.LoginFailed;
                    }
                }

            }
            catch (Exception ex)
            {
                result = ImportResult.Error;
            }
            return result;
        }
    }
}