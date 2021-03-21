using Ilia.Api.Controllers;
using Ilia.Api.Test.Helper;
using Ilia.Business;
using Ilia.CrossCutting;
using Ilia.CrossCutting.Exceptions;
using Ilia.CrossCutting.Filter;
using Ilia.CrossCutting.Interop.Dto;
using Ilia.CrossCutting.Interop.Dto.Customer;
using Ilia.CrossCutting.Interop.ViewModel;
using Ilia.Data.Context;
using Ilia.Data.Repository;
using Ilia.Data.RepositoryReadOnly;
using Ilia.Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ilia.Api.Test
{
    public class CustomerControllerTest
    {
        private readonly CustomerController _customerController;

        public CustomerControllerTest()
        {
            EnvironmentProperties.ConnectionString = string.Empty;
            TestHelper.PrepareDatabase<DataContext>();
            var context = new DataContext();
            var repositoryReadOnly = new CustomerRepositoryReadOnly(context);
            var repository = new CustomerRepository(context);
            var service = new CustomerService(repositoryReadOnly, repository, TestHelper.GetMapper());
            _customerController = new CustomerController(service, TestHelper.GetMapper());
        }

        [Fact]
        public async Task BadRequest_When_Called_Post_Without_Email()
        {
            var model = new CustomerInsertViewModel
            {
                Name = "Customer 1",
                Email = "",
                Address = new List<CustomerAddressInsertViewModel>
                {
                    new CustomerAddressInsertViewModel
                    {
                        City = "AnyCity",
                        State = "AnyState",
                        Country = "AnyCountry",
                        Street = "AnyStreet",
                        ZipCode = "00000-000",
                        Number = 000
                    }
                },
                PhoneContact = new List<CustomerPhoneContactInsertViewModel>
                {
                    new CustomerPhoneContactInsertViewModel
                    {
                        Number = "999999999",
                        PhoneType = EnumPhoneType.Landline
                    }
                }
            };
            _customerController.ModelState.AddModelError("Email", "required");
            var result = await _customerController.Post(model);

            Assert.True(result is BadRequestObjectResult);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.True(badRequestResult.Value is ErrorResponseDto);

            var resultModel = (ErrorResponseDto)badRequestResult.Value;
            Assert.Single(resultModel.Errors);
            Assert.Contains(resultModel.Errors, a => a.Field == "Email");
            Assert.Contains(resultModel.Errors, a => a.Messages.All(all => all.ToLower().Contains("required")));
        }

        [Fact]
        public async Task BadRequest_When_Called_Post_With_Invalid_Email()
        {
            var model = new CustomerInsertViewModel
            {
                Name = "Customer 1",
                Email = "invalidemailtest",
                Address = new List<CustomerAddressInsertViewModel>
                {
                    new CustomerAddressInsertViewModel
                    {
                        City = "AnyCity",
                        State = "AnyState",
                        Country = "AnyCountry",
                        Street = "AnyStreet",
                        ZipCode = "00000-000",
                        Number = 000
                    }
                },
                PhoneContact = new List<CustomerPhoneContactInsertViewModel>
                {
                    new CustomerPhoneContactInsertViewModel
                    {
                        Number = "999999999",
                        PhoneType = EnumPhoneType.Landline
                    }
                }
            };
            _customerController.ModelState.AddModelError("Email", "Invalid Email");
            var result = await _customerController.Post(model);

            Assert.True(result is BadRequestObjectResult);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.True(badRequestResult.Value is ErrorResponseDto);

            var resultModel = (ErrorResponseDto)badRequestResult.Value;
            Assert.Single(resultModel.Errors);
            Assert.Contains(resultModel.Errors, a => a.Field == "Email");
        }

        [Fact]
        public async Task BadRequest_When_Called_Post_With_Email_Already_Registered()
        {
            await When_Called_Post_Should_Be_Created();

            var model = new CustomerInsertViewModel
            {
                Name = "Customer 1",
                Email = "customer@hotmail.com",
                Address = new List<CustomerAddressInsertViewModel>
                {
                    new CustomerAddressInsertViewModel
                    {
                        City = "AnyCity",
                        State = "AnyState",
                        Country = "AnyCountry",
                        Street = "AnyStreet",
                        ZipCode = "00000-000",
                        Number = 000
                    }
                },
                PhoneContact = new List<CustomerPhoneContactInsertViewModel>
                {
                    new CustomerPhoneContactInsertViewModel
                    {
                        Number = "999999999",
                        PhoneType = EnumPhoneType.Landline
                    }
                }
            };
            await Assert.ThrowsAsync<EntityValidationException>(async () => await _customerController.Post(model));
        }

        [Fact]
        public async Task BadRequest_When_Called_Post_Without_Name()
        {
            var model = new CustomerInsertViewModel
            {
                Name = "",
                Email = "email@email.com",
                Address = new List<CustomerAddressInsertViewModel>
                {
                    new CustomerAddressInsertViewModel
                    {
                        City = "AnyCity",
                        State = "AnyState",
                        Country = "AnyCountry",
                        Street = "AnyStreet",
                        ZipCode = "00000-000",
                        Number = 000
                    }
                },
                PhoneContact = new List<CustomerPhoneContactInsertViewModel>
                {
                    new CustomerPhoneContactInsertViewModel
                    {
                        Number = "999999999",
                        PhoneType = EnumPhoneType.Landline
                    }
                }
            };
            _customerController.ModelState.AddModelError("Name", "required");
            var result = await _customerController.Post(model);

            Assert.True(result is BadRequestObjectResult);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.True(badRequestResult.Value is ErrorResponseDto);

            var resultModel = (ErrorResponseDto)badRequestResult.Value;
            Assert.Single(resultModel.Errors);
            Assert.Contains(resultModel.Errors, a => a.Field == "Name");
            Assert.Contains(resultModel.Errors, a => a.Messages.All(all => all.ToLower().Contains("required")));
        }

        [Fact]
        public async Task BadRequest_When_Called_Post_Without_PhoneNumber()
        {
            var model = new CustomerInsertViewModel
            {
                Name = "Customer01",
                Email = "email@email.com",
                Address = new List<CustomerAddressInsertViewModel>
                {
                    new CustomerAddressInsertViewModel
                    {
                        City = "AnyCity",
                        State = "AnyState",
                        Country = "AnyCountry",
                        Street = "AnyStreet",
                        ZipCode = "00000-000",
                        Number = 000
                    }
                }
            };
            _customerController.ModelState.AddModelError("PhoneNumber", "required");
            var result = await _customerController.Post(model);

            Assert.True(result is BadRequestObjectResult);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.True(badRequestResult.Value is ErrorResponseDto);

            var resultModel = (ErrorResponseDto)badRequestResult.Value;
            Assert.Single(resultModel.Errors);
            Assert.Contains(resultModel.Errors, a => a.Field == "PhoneNumber");
            Assert.Contains(resultModel.Errors, a => a.Messages.All(all => all.ToLower().Contains("required")));
        }

        [Fact]
        public async Task BadRequest_When_Called_Post_Without_Address()
        {
            var model = new CustomerInsertViewModel
            {
                Name = "Customer01",
                Email = "email@email.com",
                PhoneContact = new List<CustomerPhoneContactInsertViewModel>
                {
                    new CustomerPhoneContactInsertViewModel
                    {
                        Number = "999999999",
                        PhoneType = EnumPhoneType.Landline
                    }
                }
            };
            _customerController.ModelState.AddModelError("Address", "required");
            var result = await _customerController.Post(model);

            Assert.True(result is BadRequestObjectResult);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.True(badRequestResult.Value is ErrorResponseDto);

            var resultModel = (ErrorResponseDto)badRequestResult.Value;
            Assert.Single(resultModel.Errors);
            Assert.Contains(resultModel.Errors, a => a.Field == "Address");
            Assert.Contains(resultModel.Errors, a => a.Messages.All(all => all.ToLower().Contains("required")));
        }

        [Fact]
        public async Task<Guid> When_Called_Post_Should_Be_Created()
        {
            await Should_Delete_All_Registers();

            var model = new CustomerInsertViewModel
            {
                Name = "Customer 1",
                Email = "customer@hotmail.com",
                Address = new List<CustomerAddressInsertViewModel>
                {
                    new CustomerAddressInsertViewModel
                    {
                        City = "AnyCity",
                        State = "AnyState",
                        Country = "AnyCountry",
                        Street = "AnyStreet",
                        ZipCode = "00000-000",
                        Number = 000
                    }
                },
                PhoneContact = new List<CustomerPhoneContactInsertViewModel>
                {
                    new CustomerPhoneContactInsertViewModel
                    {
                        Number = "999999999",
                        PhoneType = EnumPhoneType.Landline
                    }
                }
            };

            var result = await _customerController.Post(model);
            var response = (ObjectResult)result;

            Assert.True(response.Value is Guid);
            Assert.True(response.StatusCode.Equals(201));
            return (Guid)response.Value;
        }

        [Fact]
        public async Task Ok_WhenCalled_Get_With_PageControl_City_Filter()
        {
            await Should_Delete_All_Registers();
            await When_Called_Post_Should_Be_Created();

            var filter = new CustomerFilter
            {
                City = "AnyCity"
            };

            var result = await _customerController.Get(filter);

            var okResult = (ObjectResult)result;
            Assert.True(okResult.Value is ResponseDto<CustomerDto>);

            var resultModel = (ResponseDto<CustomerDto>)okResult.Value;

            Assert.True(resultModel.Data.Count > 0);
            resultModel.Data.ForEach(f =>
            {
                Assert.Contains(f.Address, w => w.City.Equals("AnyCity"));
            });
        }

        [Fact]
        public async Task Ok_WhenCalled_Get_With_PageControl_State_Filter()
        {
            await Should_Delete_All_Registers();
            await When_Called_Post_Should_Be_Created();

            var filter = new CustomerFilter
            {
                State = "AnyState"
            };

            var result = await _customerController.Get(filter);

            var okResult = (ObjectResult)result;
            Assert.True(okResult.Value is ResponseDto<CustomerDto>);

            var resultModel = (ResponseDto<CustomerDto>)okResult.Value;

            Assert.True(resultModel.Data.Count > 0);
            resultModel.Data.ForEach(f =>
            {
                Assert.Contains(f.Address, w => w.State.Equals("AnyState"));
            });
        }

        [Fact]
        public async Task Ok_WhenCalled_Get_With_PageControl_Country_Filter()
        {
            await Should_Delete_All_Registers();
            await When_Called_Post_Should_Be_Created();

            var filter = new CustomerFilter
            {
                Country = "AnyCountry"
            };

            var result = await _customerController.Get(filter);

            var okResult = (ObjectResult)result;
            Assert.True(okResult.Value is ResponseDto<CustomerDto>);

            var resultModel = (ResponseDto<CustomerDto>)okResult.Value;

            Assert.True(resultModel.Data.Count > 0);
            resultModel.Data.ForEach(f =>
            {
                Assert.Contains(f.Address, w => w.Country.Equals("AnyCountry"));
            });
        }

        [Fact]
        public async Task Ok_WhenCalled_Get_With_PageControl_ZipCode_Filter()
        {
            await Should_Delete_All_Registers();
            await When_Called_Post_Should_Be_Created();
            var filter = new CustomerFilter
            {
                ZipCode = "00000-000"
            };

            var result = await _customerController.Get(filter);

            var okResult = (ObjectResult)result;
            Assert.True(okResult.Value is ResponseDto<CustomerDto>);

            var resultModel = (ResponseDto<CustomerDto>)okResult.Value;

            Assert.True(resultModel.Data.Count > 0);
            resultModel.Data.ForEach(f =>
            {
                Assert.Contains(f.Address, w => w.ZipCode.Equals("00000-000"));
            });
        }

        [Fact]
        public async Task Ok_WhenCalled_Get_With_PageControl_Street_Filter()
        {
            await Should_Delete_All_Registers();
            await When_Called_Post_Should_Be_Created();
            var filter = new CustomerFilter
            {
                Street = "AnyStreet"
            };

            var result = await _customerController.Get(filter);

            var okResult = (ObjectResult)result;
            Assert.True(okResult.Value is ResponseDto<CustomerDto>);

            var resultModel = (ResponseDto<CustomerDto>)okResult.Value;

            Assert.True(resultModel.Data.Count > 0);
            resultModel.Data.ForEach(f =>
            {
                Assert.Contains(f.Address, w => w.Street.Equals("AnyStreet"));
            });
        }

        [Fact]
        public async Task NotFound_WhenCalled_Try_Get_Customer_With_Wrong_Id()
        {
            await Assert.ThrowsAsync<NotFoundException>(async () => await _customerController.Find(Guid.NewGuid()));
        }

        [Fact]
        public async Task BadRequest_When_Called_Put_Without_Email()
        {
            await Should_Delete_All_Registers();
            var model = new CustomerUpdateViewModel
            {
                Id = await When_Called_Post_Should_Be_Created(),
                Name = "Customer 2",
                Version = 1
            };

            _customerController.ModelState.AddModelError("Email", "required");
            var result = await _customerController.Put(model);

            Assert.True(result is BadRequestObjectResult);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.True(badRequestResult.Value is ErrorResponseDto);

            var resultModel = (ErrorResponseDto)badRequestResult.Value;
            Assert.Single(resultModel.Errors);
            Assert.Contains(resultModel.Errors, a => a.Field == "Email");
            Assert.Contains(resultModel.Errors, a => a.Messages.All(all => all.ToLower().Contains("required")));
        }

        [Fact]
        public async Task Ok_When_Called_Put()
        {
            await Should_Delete_All_Registers();
            var model = new CustomerUpdateViewModel
            {
                Id = await When_Called_Post_Should_Be_Created(),
                Email = "customeremail@email.com",
                Name = "Customer 2",
                Version = 1
            };

            var result = await _customerController.Put(model);

            var response = (ObjectResult)result;
            Assert.True(response.Value != null);
            Assert.True(response.StatusCode.Equals(200));
        }

        [Fact]
        public async Task Should_Delete_All_Registers()
        {
            var list = await GetCustomers();
            list.ForEach(async item => await _customerController.Remove(item.Id));
            var newlist = await GetCustomers();
            Assert.True(!newlist.Any());
        }

        [Fact]
        public async Task Should_Add_New_Phone_Number()
        {
            await Should_Delete_All_Registers();

            var model = new PhoneContactInsertViewModel
            {
                CustomerId = await When_Called_Post_Should_Be_Created(),
                Number = "4499999999",
                PhoneType = EnumPhoneType.MobilePhone
            };

            await _customerController.PostPhoneContact(model);
            var result = await _customerController.Find(model.CustomerId);

            var response = (ObjectResult)result;
            Assert.True(response.Value != null);
            var customer = (CustomerDto)response.Value;
            Assert.True(customer.PhoneContact.Count == 2);
            Assert.True(response.StatusCode.Equals(200));
        }

        [Fact]
        public async Task Should_Add_New_Address()
        {
            await Should_Delete_All_Registers();

            var model = new AddressInsertViewModel
            {
                CustomerId = await When_Called_Post_Should_Be_Created(),
                City = "AnotherCity",
                Country = "AnotherCountry",
                Number = 321,
                State = "AnotherState",
                Street = "AnotherStreet",
                ZipCode = "00000-888"
            };

            await _customerController.PostAddress(model);
            var result = await _customerController.Find(model.CustomerId);

            var response = (ObjectResult)result;
            Assert.True(response.Value != null);
            var customer = (CustomerDto)response.Value;
            Assert.True(customer.Address.Count == 2);
            Assert.True(response.StatusCode.Equals(200));
        }

        private async Task<List<CustomerDto>> GetCustomers()
        {
            var filter = new CustomerFilter
            {
                Page = 1,
                PerPage = 100
            };

            var result = await _customerController.Get(filter);

            var response = TestHelper.ConvertFromActionResult<ResponseDto<CustomerDto>>(result);
            return response.Data.ToList();
        }
    }
}