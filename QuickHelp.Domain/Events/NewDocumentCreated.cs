using System;

namespace QuickHelp.Domain.Events
{
	public class NewDocumentCreated
	{
		public Guid Id { get; }
		public string Name { get; }
		public string Body { get; }

		public NewDocumentCreated(Guid id, string name, string body)
		{
			Id = id;
			Name = name;
			Body = body;
		}
	}
}
