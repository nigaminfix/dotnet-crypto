using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBitcoin.DataEncoders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ArkEcosystem.Crypto.Tests.Deserialisers
{
    [TestClass]
    public class DelegateRegistrationTest
    {
        [TestMethod]
        public void Should_Deserialise_The_Transaction()
        {
            var fixture = File.ReadAllText("../../../fixtures/delegate_registration.json");
            var transaction = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(fixture);
            var actual = new Deserialiser(transaction["serialized"]).Deserialise();

            Assert.AreEqual((UInt64)transaction["amount"], actual.Amount);
            Assert.AreEqual((UInt64)transaction["fee"], actual.Fee);
            Assert.AreEqual((string)transaction["asset"]["delegate"]["username"], actual.Asset["delegate"]["username"]);
            Assert.AreEqual(transaction["id"], actual.Id);
            Assert.AreEqual(transaction["network"], actual.Network);
            Assert.AreEqual(transaction["senderPublicKey"], actual.SenderPublicKey);
            Assert.AreEqual(transaction["signature"], actual.Signature);
            Assert.AreEqual(transaction["timestamp"], actual.Timestamp);
            Assert.AreEqual(transaction["type"], actual.Type);
            Assert.AreEqual(transaction["version"], actual.Version);

            Assert.AreEqual(transaction["serialized"], Encoders.Hex.EncodeData(new Serialiser(actual).Serialise()));
        }
    }
}
