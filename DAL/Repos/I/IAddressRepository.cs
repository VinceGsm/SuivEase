﻿using DAL.Models;

namespace DAL.I.Repos;

public interface IAddressRepository
{
    Task<Address> GetById(int id);

    Task<bool> IsInDb(int id);        

    Task Update(Address address);   

    Task<Address> Create (Address address);
}
