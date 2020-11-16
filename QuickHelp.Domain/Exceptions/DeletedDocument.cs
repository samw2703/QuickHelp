using System;

namespace QuickHelp.Domain.Exceptions
{
	public class DeletedDocument : Exception
	{
		public DeletedDocument(string message)
			: base(message)
		{
		}
	}
}