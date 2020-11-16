using System;
using QuickHelp.Domain.Exceptions;
using ValueOf;

namespace QuickHelp.Domain.ValueObjects
{
	public class DocumentId : ValueOf<Guid, DocumentId>
	{
		public static DocumentId From()
			=> From(Guid.NewGuid());

		protected override void Validate()
		{
			if (Value == Guid.Empty)
				throw new EmptyDocumentId();
		}
	}
}