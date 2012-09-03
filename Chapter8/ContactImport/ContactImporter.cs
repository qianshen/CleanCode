using System;

namespace Improsys.ContactImporter
{
	public class ContactImporter
	{
		public ContactImporter (
			string username,
			string password,
			string type
			)
		{
		}

		public virtual string[] emailArray
		{
			get;
			set;
		}

		public virtual bool logged_in
		{
			get;
			set;
		}

		public virtual string[] nameArray
		{
			get;
			set;
		}

		public virtual void getcontacts()
		{
		}

		public virtual void login()
		{
		}

		public virtual void Dispose()
		{
		}
	}
}

