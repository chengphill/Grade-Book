using System;
using Xunit;

namespace GradeBook.Tests
{

    public delegate string WriteLogDelegate(string logMessage);

    public class TypeTests
    {
        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log;

            log = ReturnMessage;

            var result = log("Hello");
            Assert.Equal("Hello", result);
        }
        string ReturnMessage(string message)
        {
            return message;
        }

        [Fact]
        public void StringssBehaveLikeValueTypes()
        {
            //strings are immutable
            string name = "Phil";
            var upper = MakeUppercase(name);

            Assert.Equal("Phil", name);
            Assert.Equal("PHIL", upper);
        }

        private string MakeUppercase(string parameter)
        {
            return parameter.ToUpper();
        }

        [Fact]
        public void ValueTypeAlsoPassByValue()
        {
            var x = GetInt();
            Assert.Equal(3, x);

            SetInt(ref x);
            Assert.Equal(42, x);
        }

        private void SetInt(ref int z)
        {
            z = 42;
        }

        private int GetInt()
        {
            return 3;
        }

        [Fact]
        public void CSharpCanPassByRef()
        {
            var book1 = GetBook("Book 1");
            //GetBookSetNameByRef(ref book1, "New Name"); this reference "ref" or "out" **
            GetBookSetNameByRef(out book1, "New Name"); //if using out, must have output ex. init object

            //values are equal
            Assert.Equal("New Name", book1.Name);

        }
        private void GetBookSetNameByRef(out InMemoryBook book, string name)//**must be followed up here
        {
            book = new InMemoryBook(name);
        }

        [Fact]
        public void CSharpIsPassByValue()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(book1, "New Name");

            //values are equal
            Assert.Equal("Book 1", book1.Name);

        }
        private void GetBookSetName(InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }

        [Fact]
        public void CanSetNameFromReference()
        {
            var book1 = GetBook("Book 1");
            SetName(book1, "New Name");

            //values are equal
            Assert.Equal("New Name", book1.Name);

        }

        private void SetName(InMemoryBook book, string name)
        {
            book.Name = name;
        }

        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            //values are equal
            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);

            //different object pointers
            Assert.NotSame(book1, book2);
        }

        [Fact]
        public void TwoVarsCanReferenceSameObject()
        {
            var book1 = GetBook("Book 1");
            var book2 = book1;

            //points to same object
            Assert.Same(book1, book2);

            //what Assert.Same is doing
            Assert.True(Object.ReferenceEquals(book1, book2));
        }

        InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }
    }
}
