using MediatR;
using OrderManager.Core.Entities;
using OrderManager.Core.Repositories;

namespace OrderManager.Application.Commands.Orders.CreateOrderCommand
{
	public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
	{
		private readonly ICustomerRepository _customerRepository;
		private readonly IOrderRepository _orderRepository;
		private readonly IOrderItemRepository _orderItemRepository;

		public CreateOrderCommandHandler(ICustomerRepository customerRepository, IOrderRepository orderRepository, IOrderItemRepository orderItemRepository)
		{
			_customerRepository = customerRepository;
			_orderRepository = orderRepository;
			_orderItemRepository = orderItemRepository;
		}

		public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
		{
			var customer = await _customerRepository.GetByIdAsync(request.CustomerCode);
			if (customer == null)
			{
				customer = new Customer(null);
				await _customerRepository.InsertAsync(customer);
			}

			var order = await _orderRepository.GetByIdAsync(request.OrderCode);
			if (order == null)
			{
				order = new Order(customer.Id);
				await _orderRepository.InsertAsync(order);
			}

			foreach (var item in request.Items)
			{
				var orderItems = new OrderItem(item.Product, item.Quantity, item.Price, order.Id);
				await _orderItemRepository.InsertAsync(orderItems);
			}
		}
	}
}
