﻿
using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.Dtos;
using System.Windows.Input;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name Is Required");
        RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("CustomerId Is Required");
        RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("Order Items Should Not Be Empty");
    }
}