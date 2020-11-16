using System;
using System.Data.SqlTypes;

namespace QuickHelp.Domain.Exceptions
{
	public class EmptyDocumentName : Exception
	{
		public EmptyDocumentName()
			: base("Cannot create a document with an empty name")
		{
		}
	}
}