using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Auction.Buddy.Web.Common.Validation;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Xunit;

namespace Auction.Buddy.Unit.Tests.Common.Validation
{
    public class ValidationExceptionFilterTests
    {
        private readonly ValidationExceptionFilter _filter;

        public ValidationExceptionFilterTests()
        {
            _filter = new ValidationExceptionFilter();
        }

        [Fact]
        public async Task GivenValidationExceptionWhenHandledThenBadRequest()
        {
            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("Prop", "Bad Data")
            };

            var context = CreateExceptionContext(new ValidationException(failures));
            await _filter.OnExceptionAsync(context);

            context.ExceptionHandled.Should().BeTrue();
            context.Result.Should().BeOfType<BadRequestObjectResult>();
            context.Result.As<BadRequestObjectResult>().Value.Should().Be(failures);
        }

        [Fact]
        public async Task GivenADifferentExceptionWhenHandledThenExceptionIsNotHandled()
        {
            var context = CreateExceptionContext(new ArgumentNullException());
            await _filter.OnExceptionAsync(context);

            context.ExceptionHandled.Should().BeFalse();
        }

        private static ExceptionContext CreateExceptionContext(Exception exception)
        {
            var actionContext = new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor());
            return new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = exception
            };
        }
    }
}