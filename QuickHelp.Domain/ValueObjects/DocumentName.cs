using System;
using QuickHelp.Domain.Exceptions;
using ValueOf;

namespace QuickHelp.Domain.ValueObjects
{
	public class DocumentName : ValueOf<string, DocumentName>
	{
		protected override void Validate()
		{
			if (string.IsNullOrEmpty(Value))
				throw new EmptyDocumentName();
		}
	}
}