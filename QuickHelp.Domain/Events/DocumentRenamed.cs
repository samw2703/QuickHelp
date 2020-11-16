using System;

namespace QuickHelp.Domain.Events
{
	public class DocumentRenamed
	{
		public Guid Id { get; }
		public string NewName { get; }

		public DocumentRenamed( Guid id, string newName)
		{
			Id = id;
			NewName = newName;
		}
	}
}