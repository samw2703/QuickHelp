using System;

namespace QuickHelp.Domain.Events
{
	public class DocumentPinned
	{
		public Guid Id { get; }

		public DocumentPinned(Guid id)
		{
			Id = id;
		}
	}
}