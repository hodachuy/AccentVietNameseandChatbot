using BotProject.Data.Infrastructure;
using BotProject.Data.Repositories;
using BotProject.Model.Models.LiveChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotProject.Service.Livechat
{
    public interface ICustomerService
    {
        // Customer
        Customer Create(Customer customer);
        IEnumerable<Customer> GetListCustomerByChannelGroupId(int channelGroupId);
        Customer GetById(string customerId);
        void Update(Customer customer);
        Customer Delete(Customer customer);

        // Device
        Device CreateDevice(Device device);


        void Save();
    }
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _customerRepository;
        private IDeviceRepository _deviceRepository;
        private IUnitOfWork _unitOfWork;
        public CustomerService(ICustomerRepository customerRepository,
                               IDeviceRepository deviceRepository,
                               IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _deviceRepository = deviceRepository;
        }

        public Device CreateDevice(Device device)
        {
            return _deviceRepository.Add(device);
        }

        public Customer Create(Customer customer)
        {
            return _customerRepository.Add(customer);
        }

        public Customer Delete(Customer customer)
        {
            return _customerRepository.Delete(customer);
        }

        public IEnumerable<Customer> GetListCustomerByChannelGroupId(int channelGroupId)
        {
            return _customerRepository.GetMulti(x => x.ChannelGroupID == channelGroupId);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Customer customer)
        {
            _customerRepository.Update(customer);
        }

        public Customer GetById(string customerId)
        {
            return _customerRepository.GetSingleByCondition(x => x.ID == customerId);
        }
    }
}
