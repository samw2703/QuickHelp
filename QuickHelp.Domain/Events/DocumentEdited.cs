using System;

namespace QuickHelp.Domain.Events
{
	public class DocumentEdited
	{
		public Guid Id { get; }
		public string NewBody { get; }

		public DocumentEdited(Guid id, string newBody)
		{
			Id = id;
			NewBody = newBody;
		}
	}
}