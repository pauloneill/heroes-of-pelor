﻿using Heroes_of_Pelor.Application.Common.Exceptions;
using Heroes_of_Pelor.Application.TodoItems.Commands.CreateTodoItem;
using Heroes_of_Pelor.Application.TodoItems.Commands.DeleteTodoItem;
using Heroes_of_Pelor.Application.TodoLists.Commands.CreateTodoList;
using Heroes_of_Pelor.Domain.Entities;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Heroes_of_Pelor.Application.IntegrationTests.TodoItems.Commands
{
    using static Testing;

    public class DeleteTodoItemTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTodoItemId()
        {
            var command = new DeleteTodoItemCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteTodoItem()
        {
            var listId = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            });

            var itemId = await SendAsync(new CreateTodoItemCommand
            {
                ListId = listId,
                Title = "New Item"
            });

            await SendAsync(new DeleteTodoItemCommand
            {
                Id = itemId
            });

            var item = await FindAsync<TodoItem>(itemId);

            item.Should().BeNull();
        }
    }
}
