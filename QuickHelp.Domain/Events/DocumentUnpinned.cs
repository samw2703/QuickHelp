using System;

namespace QuickHelp.Domain.Events
{
	public class DocumentUnpinned
	{
		public Guid Id { get; }

		public DocumentUnpinned(Guid id)
		{
			Id = id;
		}
	}
}