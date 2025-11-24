global using MediatR;

using System.Reflection;

namespace Store.Core.Business;

public static class BusinessLayer
{
    public static Assembly Assembly => typeof(BusinessLayer).Assembly;
}