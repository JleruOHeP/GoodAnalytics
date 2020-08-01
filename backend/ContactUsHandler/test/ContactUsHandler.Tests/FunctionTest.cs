using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using ContactUsHandler;

namespace ContactUsHandler.Tests
{
    public class FunctionTest
    {
        //Need to write some tests, but this function is simple call to captcha service and
        //save to dynamodb

        //all the tests will be full mock and verify that mocks are called
        //which is stupid

        //leaving this file for future usage - if there will be some logic added
    }
}
