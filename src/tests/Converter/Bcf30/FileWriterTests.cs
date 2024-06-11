using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using BcfToolkit.Builder.Bcf30;
using BcfToolkit.Converter;
using BcfToolkit.Converter.Bcf30;
using BcfToolkit.Model;
using BcfToolkit.Utils;
using BcfToolkit.Model.Bcf30;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Tests.Converter.Bcf30;
[TestFixture]
public class FileWriterTests {
  [Test]
  [Category("BCF v3.0")]
  public async Task WriteBcfWithStreamTest() {
    var builder = new BcfBuilder();

    var bcf =
      builder.WithDefaults()
      .Build();

    var stream = await FileWriter.WriteBcfToStream(bcf);

    stream.Position = 0;

    var bcfResultBuilder = new BcfBuilder();

    var bcfResult =
      await bcfResultBuilder
      .BuildFromStream(stream);

    Assert.That(bcf.Markups.FirstOrDefault()?.Topic.Title, Is.EqualTo(bcfResult.Markups.FirstOrDefault()?.Topic.Title));

  }

}