using System;
using System.Collections.Generic;
using System.Text;
using Improsys.ContactImporter;

namespace EFSchools.Englishtown.Community.Common.ContactImport
{
    public class ContactImporterWrapper : IContactImporter
    {
        ContactImporter _importer;

        internal ContactImporterWrapper(ContactImporter importer)
        {
            _importer = importer;
        }

        public string[] emailArray
        {
            get
            {
                return _importer.emailArray;
            }
        }

        public bool logged_in
        {
            get
            {
                return _importer.logged_in;
            }
        }

        public string[] nameArray
        {
            get
            {
                return _importer.nameArray;
            }
        }

        public void getcontacts()
        {
            _importer.getcontacts();
        }

        public void login()
        {
            _importer.login();
        }

        public void Dispose()
        {
            _importer.Dispose();
        }
    }
}
