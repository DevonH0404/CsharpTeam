using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp
{
	internal class Member
	{
		// Private variables
		private string _name;
		private int _memberId;
		private List<Book> _checkedOutBooks;
		
		// Public variables
		public string Name { get { return _name; } set { _name = value; } }
		public int MemberId { get { return _memberId; } set { _memberId = value; } }
		public List<Book> CheckedOutBooks { get { return _checkedOutBooks; } set { _checkedOutBooks = value; } }

		// Constructor
		public Member (string name, int memberId)
		{
			// Invalid input handling
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Name cannot be empty.", nameof(name));
			if (string.IsNullOrWhiteSpace(memberId.ToString()))
				throw new ArgumentException("MemberId cannot be empty", nameof(memberId));
			
			// Assigning constructor values to public variables
			Name = name;
			MemberId = memberId;
		}

		// Methods
		public void CheckOut(Book book) 
		{
			// Invalid checkout handling
			if (!book.IsAvailable)
				throw new InvalidOperationException($"No available copies of '{book.Title}' to check out.");
			if (CheckedOutBooks.Count > 5)
				throw new InvalidOperationException($"Member '{Name}' has checked out the max amount of books");

			// Add book to List
			CheckedOutBooks.Add(book);
		}
	}
}
