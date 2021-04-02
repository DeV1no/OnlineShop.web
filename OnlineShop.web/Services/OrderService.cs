using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DataLayer.Context;
using OnlineShop.web.Entities.Course;
using OnlineShop.web.Entities.Order;
using OnlineShop.web.Entities.Wallet;
using OnlineShop.web.Services.Interface;

namespace OnlineShop.web.Services
{
    public class OrderService : IOrderService
    {
        private OnlineShopeContext _context;
        private IUserService _userService;

        public OrderService(OnlineShopeContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public int AddOrder(string userName, int courseId)
        {
            int userId = _userService.GetUserIdByUserName(userName);
            Order order = _context.Orders.FirstOrDefault(o => o.UserId == userId && !o.IsFinaly);
            var course = _context.Courses.Find(courseId);
            if (order == null)
            {
                order = new Order()
                {
                    UserId = userId,
                    IsFinaly = false,
                    CreateDate = DateTime.Now,
                    OrderSum = course.CoursePrice,
                    OrderDetails = new List<OrderDetail>()
                    {
                        new OrderDetail()
                        {
                            CourseId = courseId,
                            Count = 1,
                            // OrderId = order.OrderId,
                            Price = course.CoursePrice
                        }
                    }
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            else
            {
                OrderDetail detail = _context.OrderDetails
                    .FirstOrDefault(d => d.OrderId == order.OrderId && d.CourseId == courseId);
                if (detail != null)
                {
                    detail.Count++;
                    _context.OrderDetails.Update(detail);
                }
                else
                {
                    detail = new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        Count = 1,
                        CourseId = courseId,
                        Price = course.CoursePrice
                    };
                    _context.OrderDetails.Add(detail);
                }

                _context.SaveChanges();
                UpdatePriceOrder(order.OrderId);
            }

            return order.OrderId;
        }

        public void UpdatePriceOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            order.OrderSum = _context.OrderDetails.Where(d => d.OrderId == orderId).Sum(d => d.Price);
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public Order GetOrderGorUserPanle(string userName, int orderId)
        {
            int userId = _userService.GetUserIdByUserName(userName);
            return _context.Orders.Include(o => o.OrderDetails)
                .ThenInclude(od => od.Course)
                .FirstOrDefault(o => o.UserId == userId && o.OrderId == orderId);
        }

        public bool FinallyOrder(string userName, int orderId)
        {
            int userId = _userService.GetUserIdByUserName(userName);
            var order = _context.Orders.Include(o => o.OrderDetails)
                .ThenInclude(od => od.Course)
                .FirstOrDefault(o => o.OrderId == orderId && o.UserId == userId);
            if (order == null || order.IsFinaly)
                return false;
            if (_userService.BalanceUserWallet(userName) >= order.OrderSum)
            {
                order.IsFinaly = true;
                _userService.AddWallet(new Wallet()
                {
                    Amount = order.OrderSum,
                    CreatedDate = DateTime.Now,
                    IsPay = true,
                    Description = " فاکتور شماره#" + order.OrderId,
                    UserId = userId,
                    TypeId = 2,
                });
                _context.Orders.Update(order);
                foreach (var item in order.OrderDetails)
                {
                    _context.UserCourses.Add(new UserCourse()
                    {
                        CourseId = item.CourseId,
                        UserId = userId
                    });
                }

                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public List<Order> GetUserOrders(string userName)
        {
            int userId = _userService.GetUserIdByUserName(userName);
            return _context.Orders.Where(o => o.UserId == userId).ToList();
        }
    }
}