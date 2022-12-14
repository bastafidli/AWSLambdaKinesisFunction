using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

using Amazon;
using Amazon.Lambda.Core;
using Amazon.Lambda.KinesisEvents;
using Amazon.Lambda.TestUtilities;

using AWSLambdaKinesisFunction;

namespace AWSLambdaKinesisFunction.Tests
{
    public class FunctionTest
    {
  
        [Fact]
        public void TestFunction()
        {
            KinesisEvent evnt = new KinesisEvent
            {
                Records = new List<KinesisEvent.KinesisEventRecord>
                {
                    new KinesisEvent.KinesisEventRecord
                    {
                        AwsRegion = "us-west-2",
                        Kinesis = new KinesisEvent.Record
                        {
                            ApproximateArrivalTimestamp = DateTime.Now,
                            Data = new MemoryStream(Encoding.UTF8.GetBytes("Hello World Kinesis Record"))
                        }
                    }
                }
            };


            var context = new TestLambdaContext();
            var function = new Function();

            function.FunctionHandler(evnt, context);

            var testLogger = context.Logger as TestLambdaLogger;
			Assert.Contains("Stream processing complete", testLogger.Buffer.ToString());
        }  
    }
}
