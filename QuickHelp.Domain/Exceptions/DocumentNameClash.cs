using System;
using QuickHelp.Domain.ValueObjects;

namespace QuickHelp.Domain.Exceptions
{
	public class DocumentNameClash : Exception
	{
		public DocumentNameClash(DocumentName name)
			: base($"Document with name {name.Value} already exists")
		{
		}
	}
}