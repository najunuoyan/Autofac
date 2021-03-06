﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extras.Attributed;
using Autofac.Extras.Tests.Attributed.ScenarioTypes;
using Autofac.Integration.Mef;
using NUnit.Framework;

namespace Autofac.Extras.Tests.Attributed
{
    [TestFixture]
    public class WeakTypedAttributeScenarioTestFixture
    {
        [Test]
        public void validate_wireup_of_generic_attributes_to_strongly_typed_metadata_on_resolve()
        {
            // arrange
            var builder = new ContainerBuilder();
            builder.RegisterMetadataRegistrationSources();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .As<IWeakTypedScenario>()
                .WithAttributedMetadata();

            // act
            var items = builder.Build().Resolve<IEnumerable<Lazy<IWeakTypedScenario, IWeakTypedScenarioMetadata>>>();

            // assert
            Assert.That(items.Count(), Is.EqualTo(1));
            Assert.That(items.Where(p => p.Metadata.Name == "Hello").Count(), Is.EqualTo(1));
        }
    }
}
