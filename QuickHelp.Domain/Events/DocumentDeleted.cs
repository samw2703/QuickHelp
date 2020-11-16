using System;

namespace QuickHelp.Domain.Events
{
	public class DocumentDeleted
	{
		public Guid DocumentId { get; }

		public DocumentDeleted(Guid documentId)
		{
			DocumentId = documentId;
		}
	}
}