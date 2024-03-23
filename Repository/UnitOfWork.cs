﻿using Microsoft.EntityFrameworkCore.Storage;
using OrderService.Contracts;
using OrderService.Entities;

namespace OrderService.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private IUserRepository _user;
        private IOrderRepository _order;
        private IOrderDetailsRepository _orderDetails;
        private ICartRepository _cartRepository;
        private ICartCustomizationRepository _cartCustomizationRepository;


        private IDbContextTransaction _transaction;

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_context);
                }
                return _user;
            }
        }
        public IOrderRepository Order
        {
            get
            {
                if (_order == null)
                {
                    _order = new OrdersRepository(_context);
                }
                return _order;
            }
        }

        public IOrderDetailsRepository OrderDetails
        {
            get
            {
                if (_orderDetails == null)
                {
                    _orderDetails = new OrderDetailsRepository(_context);
                }
                return _orderDetails;
            }
        }

        public ICartRepository Cart
        {
            get
            {
                if (_cartRepository == null)
                {
                    _cartRepository = new CartRepository(_context);
                }
                return _cartRepository;
            }
        }

        public ICartCustomizationRepository CartCustomization
        {
            get
            {
                if (_cartCustomizationRepository == null)
                {
                    _cartCustomizationRepository = new CartCustomizationRepository(_context);
                }
                return _cartCustomizationRepository;
            }
        }

        // public ICartRepository Cart => throw new NotImplementedException();


        public UnitOfWork(ApplicationContext context)
        {
            _context = context;

        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _transaction?.Commit();
        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
            }
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
